using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebVacuum.Classes;

namespace WebVacuum.account
{
    public partial class ChangePass : System.Web.UI.Page
    {
        private Dictionary<PlaceHolder, string> errs;

        public Dictionary<PlaceHolder, string> Errs
        {
            get { return errs; }
            set { errs = value; }
        }
        private DbLink.Account account;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ok"] != null)
            {
                SuccessText.Visible = true;
                showSuccessText.Text = Session["ok"].ToString();
                Session["ok"] = null;
            }
            if (Session["login"] == null)
            {
                Response.Redirect(GlobalVariables.UrlHost);
            }
            errs = new Dictionary<PlaceHolder, string>();
            account = new DbLink.Account();
            account.Login = Session["login"].ToString();
            GlobalVariables.Link.GetUser(account);
            ErrorOldPass.Visible = false;
            ErrorNewPass.Visible = false;
            ErrorReNewPass.Visible = false;
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
            else
            {
                foreach (var item in Errs)
                {
                    item.Key.Visible = true;
                    Literal control = (Literal)item.Key.FindControl("show" + item.Key.ID);
                    control.Text = item.Value;
                }
            }
        }
        private int CheckUser()
        {
            if (!ErrorControls.NotEmptyTextBox(btnOldPassword))
            {
                errs.Add(ErrorOldPass, "Поле не должно быть пустым");
            }
            else if (btnOldPassword.Text != account.Password)
            {
                ErrorControls.CountErrors(false);
                errs.Add(ErrorOldPass, "Не верный пароль");
            }
            if (!ErrorControls.NotEmptyTextBox(btnPassword))
            {
                errs.Add(ErrorNewPass, "Поле не должно быть пустым");
            }
            else if (btnPassword.Text != btnRePassword.Text)
            {
                ErrorControls.CountErrors(false);
                errs.Add(ErrorNewPass, "Пароли должны совпадать");
                errs.Add(ErrorReNewPass, "Пароли должны совпадать");
            }

            return ErrorControls.GetCount();
        }
    }
}