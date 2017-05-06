using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace HorceRace
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_EndRequest()
        {
            string url = Request.RawUrl.ToString();
            string[] stArray = url.Split('/');
            if (!(stArray.Length > 1 && stArray[1] == "Article" && stArray[2] == "NoAccess"))
            {
                if (Request.Browser.Browser.ToString() == "IE" || Request.Browser.Browser.ToString() == "InternetExplorer")
                {
                    Response.Clear();
                    Response.RedirectToRoute(new { controller = "Article", action = "NoAccess" });
                }
            }
        }
    }
}