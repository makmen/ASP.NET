using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebVacuum.Classes;

namespace WebVacuum
{
    public partial class Default : System.Web.UI.Page
    {
        protected string GetUrl
        {
            get { return GlobalVariables.UrlHost; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string session = Request.QueryString["log"];
            if (session != null && session == "out")
            {
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }
    }
}