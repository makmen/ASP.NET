using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebVacuum.Classes;

namespace WebVacuum.article
{
    public partial class Contacts : System.Web.UI.Page
    {
        protected string GetUrl
        {
            get { return GlobalVariables.UrlHost; }
        }
        public bool success = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            ltError.Visible = false;
            ltSuccess.Visible = false;
        }

        protected void btnContact_Click(object sender, EventArgs e)
        {
            if (CheckUser() == 0) // if zero errors 
            {
                string message = "Имя: " + btnName.Text + "\r\n";
                message += "email: " + btnEmail.Text + "\r\n";
                message += "message: " + btnMessage.Text + "\r\n";
                //SendEmail sendMail = new SendEmail("makmen@inbox.ru", "Контактная информация", message);
                //success = sendMail.Success;
                success = true;
                if (success)
                {
                    ltSuccess.Visible = true;
                    btnName.Text = btnEmail.Text = btnMessage.Text = "";
                }
                else
                {
                    ltError.Visible = true;
                }
            }
        }

        private int CheckUser()
        {
            ErrorControls.NotEmptyTextBox(btnName);
            ErrorControls.RegexTextBox(btnEmail, "^[0-9a-z_\\.-]+@[0-9a-z_\\.-]+\\.[a-z]{2,}?$");
            ErrorControls.NotEmptyTextBox(btnMessage);

            return ErrorControls.GetCount();
        }
    }
}