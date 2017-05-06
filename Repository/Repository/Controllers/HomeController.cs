using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repository.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public EFUnitOfWork Link = new EFUnitOfWork();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = Link.Books.GetAll();
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;

            return View();
        }

        [HttpGet]
        public ActionResult Buy(int id, int tem = 10)
        {
            string controller = RouteData.Values["controller"].ToString();
            string action = RouteData.Values["action"].ToString();
            string id1 = RouteData.Values["id"].ToString();
            ViewBag.BookId = id;
            int a = tem;
            return View();
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            Link.Purchases.Create(purchase);
            // сохраняем в бд все изменения
            Link.Save();
            
            return "Спасибо," + purchase.Person + ", за покупку!";
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

    }
}
