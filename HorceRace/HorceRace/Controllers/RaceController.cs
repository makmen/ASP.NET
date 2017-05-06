using Connection;
using Connection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace HorceRace.Controllers
{
    public class RaceController : Controller
    {
        //
        // GET: /Race/
        public static EntityRepository Link = EntityRepository.GetInstance();
        private Random rand = new Random();

        public ActionResult Index()
        {
            ViewBag.Limit = 5;
            string str = Request.QueryString["p"];
            int page = 1;
            if (!string.IsNullOrEmpty(str))
            {
                Int32.TryParse(str, out page);
                if (page == 0)
                {
                    page = 1;
                }
            }
            ViewBag.Page = page;
            ViewBag.ListRaces = Link.Races.GetRacesLimit(ViewBag.Limit, page);
            pagging();

            return View();
        }

        private void pagging()
        {
            ViewBag.NumTotalNews = Link.Races.GetTotalNews();
            ViewBag.NumPages = (int)Math.Ceiling((double)ViewBag.NumTotalNews / (double)ViewBag.Limit);
        }

        public ActionResult Add()
        {
            if (HttpContext.Session["login"] == null
                || HttpContext.Session["groupid"].ToString() != "1")
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Add(Race race)
        {
            if (ModelState.IsValid)
            {
                bool success = false;
                // Save
                race.Created = DateTime.Now;
                race.Status = "wait";
                try
                {
                    int id;
                    Int32.TryParse(Session["loginid"].ToString(), out id);
                    Account account = Link.Accounts.Get(id);
                    race.account = account;
                    if (race.account == null)
                    {
                        throw new Exception();
                    }
                    Link.Races.Create(race);
                    Link.Save();
                    success = true;
                }
                catch
                {
                    // логирование
                }
                ViewShowMessage(success, "Данные сохранены", "Ошибка записи в базу данных");
            }

            return View();
        }

        public ActionResult Edit()
        {
            if (HttpContext.Session["login"] == null || 
                HttpContext.Session["groupid"].ToString() != "1")
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }
            int id = GetId();
            if (id <= 0)
            {
                return Redirect("~/Race");
            }
            Race race = Link.Races.Get(id);
            if (race == null)
            {
                return Redirect("~/Race");
            }
            if (Session["loginid"].ToString() != race.account.Id.ToString() ||
                race.Status != "wait")
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }

            return View("Add", race);
        }

        [HttpPost]
        public ActionResult Edit(Race race)
        {
            if (HttpContext.Session["login"] == null ||
                HttpContext.Session["groupid"].ToString() != "1")
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }
            int id = GetId();
            if (id <= 0)
            {
                return Redirect("~/Race");
            }
            if (ModelState.IsValid)
            {
                bool success = false;
                Race raceNew = Link.Races.Get(id);
                if (Session["loginid"].ToString() != raceNew.account.Id.ToString() ||
                    raceNew.Status != "wait")
                {
                    ViewBag.noAccess = "1";
                    return View("~/Views/Article/NoAccess.cshtml");
                }
                raceNew.Title = Request.Form["title"].ToString();
                raceNew.Description = Request.Form["description"].ToString();
                raceNew.Distance = Int32.Parse(Request.Form["distance"]);
                try
                {
                    Link.Races.Update(raceNew);
                    Link.Save();
                    success = true;
                }
                catch
                {
                    // логирование
                }
                ViewShowMessage(success, "Данные сохранены", "Ошибка записи в базу данных");
            }

            return View("Add", race);
        }

        public ActionResult Viewone()
        {
            int id = GetId();
            if (id <= 0)
            {
                return Redirect("~/Race");
            }
            // просматривать могут все пользователи
            // а также гости но только когда Race находится в statuse wait или closed
            Race race = Link.Races.Get(id);
            if (race == null)
            {
                return Redirect("~/Race");
            }
            if ( HttpContext.Session["login"] == null && race.Status == "run")
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }
            if (Session["login"] != null) // can he edit race?
            {
                if (Session["loginid"].ToString() == race.account.Id.ToString() && race.Status == "wait")
                {
                    ViewBag.CanEdit = true;
                }
            }
            ViewBag.Onerace = race;
            List<Participant> participants = null;
            if (race.Status == "wait")
            {
                participants = Link.Participants.GetParticipants(id);
            }
            else
            {
                participants = Link.Participants.GetParticipantsOrder(id);
            }

            ViewBag.Allparticipants = participants;

            return View();
        }

        public ActionResult Start()
        {
            if (HttpContext.Session["login"] == null ||
                HttpContext.Session["groupid"].ToString() != "1")
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }
            int id = GetId();
            if (id <= 0)
            {
                return Redirect("~/Race");
            }
            Race race = Link.Races.Get(id);
            if (race == null || 
                race.Status != "wait" ||
                Session["loginid"].ToString() != race.account.Id.ToString())
            {
                return Redirect("~/Race");
            }
            List<Participant> participants = Link.Participants.GetParticipants(id);
            if (participants.Count < 5)
            {
                ViewBag.noAccess = "Должно быть как минимум 5 участников";
                return View("~/Views/Article/NoAccess.cshtml");
            }

            race.Status = "run";
            Link.Races.Update(race);
            Link.Save();

            ParameterizedThreadStart threadStart = new ParameterizedThreadStart(StartRunning);
            Thread mainThread = new Thread(threadStart);
            mainThread.IsBackground = true;
            mainThread.Start((object)race);
            Thread.Sleep(500);

            return Redirect("~/Race/Viewone/" + id);
        }

        public void StartRunning(object obj)
        {
            Race race = obj as Race;
            List<Participant> listParticipants = Link.Participants.GetParticipants(race.Id);
            bool end = true;
            while (end)
            {
                foreach (Participant item in listParticipants)
                {
                    item.Run += rand.Next(40, 50);
                    if (item.Run >= race.Distance)
                    {
                        end = false;
                    }
                    Link.Participants.Update(item);
                    Link.Save();
                }
                Thread.Sleep(10000);
            }
            race.Status = "Closed";
            Link.Races.Update(race);
            Link.Save();
        }


        private int GetId()
        {
            int res = 0;
            string strId = RouteData.Values["id"].ToString();
            if (!string.IsNullOrEmpty(strId))
            {
                Int32.TryParse(strId, out res);
            }

            return res;
        }

        private void ViewShowMessage(bool success, string good, string bad)
        {
            if (success)
            {
                ViewBag.Success = good;
            }
            else
            {
                ViewBag.Error = bad;
            }
        }
    }
}
