using ConnectionDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebVTT.Models;

namespace WebVTT.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public static Connect Link = Connect.GetInstance();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Technology()
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

    }
}
