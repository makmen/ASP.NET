using ConnectionDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebVTT.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/
        public static Connect Link = Connect.GetInstance();

        public ActionResult Index() // all news
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
            List<News> listNews = Link.GetNews(ViewBag.Limit, page);
            foreach (News news in listNews)
            {
                if (news.Content.Length > 400)
                {
                    news.Content = news.Content.Substring(0, 400);
                }
            }
            pagging();
            ViewBag.ListNews = listNews;

            return View();
        }
        private void pagging()
        {
            ViewBag.NumTotalNews = Link.GetTotalNews();
            ViewBag.NumPages = (int)Math.Ceiling((double)ViewBag.NumTotalNews / (double)ViewBag.Limit);
        }

        public ActionResult Add() // add news
        {
            if (HttpContext.Session["login"] == null)
            {
                return Redirect("~");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Add(News news) // add news
        {
            if (ModelState.IsValid)
            {
                news.Created = DateTime.Now;
                if (Link.AddNews(news, Session["login"].ToString()))
                {
                    ViewBag.Success = "Данные сохранены";
                }
                else
                {
                    ViewBag.Error = "Ошибка записи в базу данных";
                }
            }

            return View();
        }

        public ActionResult Edit() // edit if you created news you can change it
        {
            News news = null;
            string strId = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(strId))
            {
                return Redirect("~");
            }
            int id;
            Int32.TryParse(strId, out id);
            if (id <= 0)
            {
                return Redirect("~");
            }
            if (Session["login"] != null) // can he edit news?
            {
                Account currentAccount = new Account();
                currentAccount.Login = Session["login"].ToString();
                Link.GetUser(currentAccount);
                news = Link.GetOneNews(id);
                if (news == null ||
                    currentAccount.Id != news.account.Id)
                {
                    return Redirect("~");
                }
                news = Link.GetOneNews(id);
            }
            else
            {
                return Redirect("~");
            }

            return View("Add", news);
        }

        [HttpPost]
        public ActionResult Edit(News news) // edit if you created news you can change it
        {
            if (ModelState.IsValid)
            {
                if (Link.EditNews(news, Session["login"].ToString()))
                {
                    ViewBag.Success = "Данные сохранены";
                }
                else
                {
                    ViewBag.Error = "Ошибка записи в базу данных";
                }
            }

            return View("Add");
        }

        public ActionResult Print()
        {
            News OneNews = null;
            string strId = RouteData.Values["id"].ToString();
            if (!string.IsNullOrEmpty(strId))
            {
                int id;
                Int32.TryParse(strId, out id);
                if (id > 0)
                {

                }
                OneNews = Link.GetOneNews(id);
            }
            ViewBag.OneNews = OneNews;

            return View();
        }

        public ActionResult ViewOne() // view one news
        {
            string strId = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(strId))
            {
                return Redirect("~");
            }
            int id;
            Int32.TryParse(strId, out id);       
            if (id <= 0)
            {
                return Redirect("~");
            }
            News OneNews = Link.GetOneNews(id);
            if (OneNews == null)
            {
                return Redirect("~");
            }
            if (Session["login"] != null) // can he edit news?
            {
                Account currentAccount = new Account();
                currentAccount.Login = Session["login"].ToString();
                Link.GetUser(currentAccount);
                if (currentAccount.Id == OneNews.account.Id)
                {
                    ViewBag.CanEdit = true;
                }
            }
            ViewBag.OneNews = OneNews;

            return View();
        }

    }
}
