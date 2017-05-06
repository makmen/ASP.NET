using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RouteCalc.Controllers
{
    public class CalcController : Controller
    {
        //
        // GET: /Calc/

        public ActionResult Index()
        {
            return Add();
        }

        public ActionResult Add()
        {
            if (RouteData.Values["first"] != null &&
                RouteData.Values["second"] != null)
            {
                ViewData["first"] = RouteData.Values["first"].ToString();
                ViewData["second"] = RouteData.Values["second"].ToString();
                int a, b;
                if (Int32.TryParse(ViewData["first"].ToString(), out a) &&
                    Int32.TryParse(ViewData["second"].ToString(), out b))
                {
                    ViewData["result"] = a + b; 
                }
            }

            return View("Add");
        }

        public ActionResult Sub()
        {
            if (RouteData.Values["first"] != null &&
                RouteData.Values["second"] != null)
            {
                ViewData["first"] = RouteData.Values["first"].ToString();
                ViewData["second"] = RouteData.Values["second"].ToString();
                int a, b;
                if (Int32.TryParse(ViewData["first"].ToString(), out a) &&
                    Int32.TryParse(ViewData["second"].ToString(), out b))
                {
                    ViewData["result"] = a - b;
                }
            }

            return View();
        }

        public ActionResult Mul()
        {
            if (RouteData.Values["first"] != null &&
                RouteData.Values["second"] != null)
            {
                ViewData["first"] = RouteData.Values["first"].ToString();
                ViewData["second"] = RouteData.Values["second"].ToString();
                int a, b;
                if (Int32.TryParse(ViewData["first"].ToString(), out a) &&
                    Int32.TryParse(ViewData["second"].ToString(), out b))
                {
                    ViewData["result"] = a * b;
                }
            }

            return View();
        }

        public ActionResult Div()
        {
            if (RouteData.Values["first"] != null &&
                RouteData.Values["second"] != null)
            {
                ViewData["first"] = RouteData.Values["first"].ToString();
                ViewData["second"] = RouteData.Values["second"].ToString();
                int a, b;
                if (Int32.TryParse(ViewData["first"].ToString(), out a) &&
                    Int32.TryParse(ViewData["second"].ToString(), out b))
                {
                    ViewData["result"] = (double)a / b;
                }
            }

            return View();
        }
    }
}
