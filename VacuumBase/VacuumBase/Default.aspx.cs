using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VacuumBase.Classes;

namespace VacuumBase
{
    public partial class Default : System.Web.UI.Page
    {
        protected string GetUrl
        {
            get { return GlobalVariables.UrlHost; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = Request.QueryString["log"];
            if (str != null && str == "out")
            {
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }
    }
}