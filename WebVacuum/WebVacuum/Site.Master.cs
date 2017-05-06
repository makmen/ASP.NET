using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebVacuum.Classes;

namespace WebVacuum
{
    public partial class Site : System.Web.UI.MasterPage
    {

        protected string GetUrl
        {
            get { return GlobalVariables.UrlHost; }
        }

        protected string GetGreeting
        {
            get
            {
                return "Добрый день, " + Session["name"] + " "
                        + Session["middlename"] + " "
                        + Session["lastname"];
            }
        }

        protected string GetGroupString
        {
            get
            {
                return "Вы вошли, как " + Session["groupTitle"].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] != null)
            {
                holderSessionEmpty.Visible = false;
                holderSessionEnter.Visible = true;
            }
            // Menu
            if (Session["login"] != null && Session["group"].ToString() == "1") // for admin
            {
                holderAddNews.Visible = true;
                holderAddAdmin.Visible = true;
            }
            if (Session["login"] != null) // for all
            {
                holderMyProfile.Visible = true;
            }
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            if (tbLogin.Text != "" && tbPassword.Text != "")
            {
                DbLink.Account findAccount = new DbLink.Account() { Login = tbLogin.Text, Password = tbPassword.Text };
                if (GlobalVariables.Link.Authorization(findAccount))
                {
                    Session["login"] = findAccount.Login;
                    Session["name"] = findAccount.Name;
                    Session["middlename"] = findAccount.MiddleName;
                    Session["lastname"] = findAccount.LastName;
                    Session["group"] = findAccount.group.Id;
                    Session["groupTitle"] = findAccount.group.Title;
                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    loginError.Visible = true;
                }
            }
            tbLogin.Text = tbPassword.Text = "";
        }
    }
}