using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VacuumBase.Classes;

namespace VacuumBase.Account
{
    public partial class ChangePass : System.Web.UI.Page
    {
        public string OkMessage { get; set; } 

        private Dictionary<string, string> errs;

        public Dictionary<string, string> Errs
        {
            get { return errs; }
            set { errs = value; }
        }
        private DbLink.Account account;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ok"] != null)
            {
                OkMessage = Session["ok"].ToString();
                Session["ok"] = null;
            }
            errs = new Dictionary<string, string>();
            if (Session["login"] == null)
            {
                Response.Redirect(GlobalVariables.UrlHost);
            }
            account = new DbLink.Account();
            account.Login = Session["login"].ToString();
            GlobalVariables.Link.GetUser(account);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckUser() == 0) // ошибок не было
            {
                account.Password = btnPassword.Text;
                if (GlobalVariables.Link.EditUser(account))
                {
                    Session["ok"] = "Пароль изменен";
                    Response.Redirect(Request.Url.ToString());
                }
            }
        }

        protected void GetError(string str)
        {
            Response.Write(errs[str]);
        }

        private int CheckUser()
        {
            if (!ErrorControls.NotEmptyTextBox(btnOldPassword))
            {
                errs.Add(btnOldPassword.ID, "Поле не должно быть пустым");
            }
            else if (btnOldPassword.Text != account.Password)
            {
                errs.Add(btnOldPassword.ID, "Не верный пароль");
            }
            if (!ErrorControls.NotEmptyTextBox(btnPassword))
            {
                errs.Add(btnPassword.ID, "Поле не должно быть пустым");
            }
            else if (btnPassword.Text != btnRePassword.Text)
            {
                errs.Add(btnPassword.ID, "Пароли должны совпадать");
                errs.Add(btnRePassword.ID, "Пароли должны совпадать");
            }

            return ErrorControls.GetCount();
        }
    }
}