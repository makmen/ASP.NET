using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VacuumBase.Classes;

namespace VacuumBase
{
    public partial class Contacts : System.Web.UI.Page
    {
        public bool send = false;
        public bool success;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnContact_Click(object sender, EventArgs e)
        {
            if (CheckUser() == 0) // ошибок не было
            {
                send = true;
                string message = "Имя: " + btnName.Text + "\r\n";
                message += "email: " + btnEmail.Text + "\r\n";
                message += "message: " + btnMessage.Text + "\r\n";
                //SendEmail sendMail = new SendEmail("makmen@inbox.ru", "Контактная информация", message);
                //success = sendMail.Success;
                success = true;
                btnName.Text = btnEmail.Text = btnMessage.Text = "";
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