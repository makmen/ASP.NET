using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebVacuum.Classes;

namespace WebVacuum.article
{
    public partial class Technology : System.Web.UI.Page
    {
        protected string GetUrl
        {
            get { return GlobalVariables.UrlHost; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}