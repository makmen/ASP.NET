using Connection;
using Connection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HorceRace.Controllers
{
    public class ParticipantController : Controller
    {
        //
        // GET: /Participant/
        public static EntityRepository Link = EntityRepository.GetInstance();
        private Random rand = new Random();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            if (HttpContext.Session["login"] == null
                || HttpContext.Session["groupid"].ToString() != "1")
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

            if (Session["loginid"].ToString() != race.account.Id.ToString() 
                || race.Status != "wait")
            {
                return Redirect("~/Race/Viewone/" + id);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Add(Participant participant)
        {
            if (HttpContext.Session["login"] == null
                || HttpContext.Session["groupid"].ToString() != "1")
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

            if (Session["loginid"].ToString() != race.account.Id.ToString()
                || race.Status != "wait")
            {
                return Redirect("~/Race/Viewone/" + id);
            }
            if (ModelState.IsValid)
            {
                bool success = false;
                try
                {
                    participant.race = race;
                    participant.Num = (rand.Next(0, 100000)).ToString();
                    participant.Run = 0;
                    Link.Participants.Create(participant);
                    Link.Save();
                    success = true;
                    return Redirect("~/Race/Viewone/" + id);
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
            if (HttpContext.Session["login"] == null
                || HttpContext.Session["groupid"].ToString() != "1")
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }
            int id = GetId();
            if (id <= 0)
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }
            Participant participant = Link.Participants.Get(id);
            if (participant == null)
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }
            Race race = Link.Races.Get(participant.race.Id);
            if (Session["loginid"].ToString() != race.account.Id.ToString()
                || race.Status != "wait")
            {
                return Redirect("~/Race/Viewone/" + race.Id);
            }

            return View("Add", participant);
        }

        [HttpPost]
        public ActionResult Edit(Participant participant)
        {
            if (HttpContext.Session["login"] == null
                || HttpContext.Session["groupid"].ToString() != "1")
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }
            int id = GetId();
            if (id <= 0)
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }
            if (ModelState.IsValid)
            {
                Participant participantEdit = Link.Participants.Get(id);
                if (participantEdit == null)
                {
                    ViewBag.noAccess = "1";
                    return View("~/Views/Article/NoAccess.cshtml");
                }
                Race race = Link.Races.Get(participantEdit.race.Id);
                if (Session["loginid"].ToString() != race.account.Id.ToString()
                    || race.Status != "wait")
                {
                    return Redirect("~/Race/Viewone/" + race.Id);
                }
                participantEdit.Jokey = Request.Form["Jokey"].ToString();
                participantEdit.Trener = Request.Form["Trener"].ToString();
                participantEdit.Horse = Request.Form["Horse"].ToString();
                bool success = false;
                try
                {
                    Link.Participants.Update(participantEdit);
                    Link.Save();
                    success = true;
                    return Redirect("~/Race/Viewone/" + race.Id);
                }
                catch
                {
                    // логирование
                }
                ViewShowMessage(success, "Данные сохранены", "Ошибка записи в базу данных");
            }

            return View("Add", participant);
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
