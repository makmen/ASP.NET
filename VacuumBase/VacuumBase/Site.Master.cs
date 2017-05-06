using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VacuumBase.Classes;

namespace VacuumBase
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected string GetUrl
        {
            get { return GlobalVariables.UrlHost; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GlobalVariables.UrlHost = "http://" + Request.Url.Authority;
            GlobalVariables.FullPath = Server.MapPath("~");
            GlobalVariables.Link = new DbLink.Connect();
            GlobalVariables.UrlStop = GlobalVariables.UrlHost + "/Ban.aspx";
        }

        protected void FormGreeting()
        {
            Response.Write("Добрый день, " + Session["name"] + " "
                + Session["middlename"] + " "
                + Session["lastname"]);
        }

        protected void FormGroup()
        {
            Response.Write("Вы вошли, как " + Session["groupTitle"]);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbLogin.Text != "" && tbPassword.Text != "")
            {
                // проверка к базе
                DbLink.Account findAccount = new DbLink.Account() { Login = tbLogin.Text, Password = tbPassword.Text };
                DbLink.Group findGroup = new DbLink.Group();
                if (GlobalVariables.Link.Authorization(findAccount, findGroup))
                {
                    Session["login"] = findAccount.Login;
                    Session["name"] = findAccount.Name;
                    Session["middlename"] = findAccount.MiddleName;
                    Session["lastname"] = findAccount.LastName;
                    Session["group"] = findGroup.Id;
                    Session["groupTitle"] = findGroup.Title;
                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    errlogin.Style.Clear();
                }
            }
            else
            {
                errlogin.Style.Clear();
            }
            tbLogin.Text = tbPassword.Text = "";
        }
    }
}