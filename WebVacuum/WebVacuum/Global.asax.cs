﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WebVacuum.Classes;

namespace WebVacuum
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            GlobalVariables.UrlHost = "http://" + Request.Url.Authority;
            GlobalVariables.UrlStop = GlobalVariables.UrlHost + "/Stop.aspx";
            GlobalVariables.Link = DbLink.Connect.GetInstance(); 
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();


        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}