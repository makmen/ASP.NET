using HorceRace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HorceRace.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult Contacts()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contacts(ContactsModel contactsModel)
        {
            if (ModelState.IsValid)
            {
                // send email
                bool send = true;
                if (send)
                {
                    contactsModel.Name = null;
                    contactsModel.Message = null;
                    contactsModel.Email = null;
                    ViewBag.Success = "Письмо отправлено";
                }
                else
                {
                    ViewBag.Error = "Ошибка отправки";
                }
            }

            return View();
        }

        public ActionResult NoAccess()
        {
            ViewBag.noAccess = "Для работы с сайтом необходимо использовать любой браузер кроме Internet Explorer";

            return View();
        }
    }
}
