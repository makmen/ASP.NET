using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebVacuum.Classes;

namespace WebVacuum.account
{
    public partial class Register : System.Web.UI.Page
    {
        protected string GetUrl
        {
            get { return GlobalVariables.UrlHost; }
        }

        public string Mode { get; set; }

        private Dictionary<PlaceHolder, string> errs;
        public Dictionary<PlaceHolder, string> Errs
        {
            get { return errs; }
            set { errs = value; }
        }

        private DbLink.Account account;

        protected void Page_Load(object sender, EventArgs e)
        {
            Mode = string.IsNullOrEmpty(Request.QueryString["mode"]) ? "add" : "edit";
            errs = new Dictionary<PlaceHolder, string>();
            if (Session["ok"] != null)
            {
                SuccessText.Visible = true;
                showSuccessText.Text = Session["ok"].ToString();
                Session["ok"] = null;
            }
            ErrorName.Visible = false;
            ErrorMiddleName.Visible = false;
            ErrorLastName.Visible = false;
            ErrorEmail.Visible = false;
            ErrorLogin.Visible = false;
            ErrorPass.Visible = false;
            ErrorRePass.Visible = false;
            ErrorCaptcha.Visible = false;
            if (Mode == "edit" && Session["login"] == null)
            {
                Response.Redirect(GlobalVariables.UrlHost);
            }
            if (Session["login"] != null &&
                Session["group"].ToString() == "2" &&
                Mode != "edit")
            {
                Response.Redirect(GlobalVariables.UrlHost);
            }
            if (Mode == "add")
            {
                holderAdd.Visible = true;
            }
            else
            {
                EditUser();
            }
        }

        protected void EditUser()
        {
            account = new DbLink.Account();
            account.Login = Session["login"].ToString();
            if (GlobalVariables.Link.GetUser(account))
            {
                if (!IsPostBack)
                {
                    tbName.Text = account.Name;
                    tbMiddleName.Text = account.MiddleName;
                    tbLastName.Text = account.LastName;
                    tbEmail.Text = account.Email;
                    tbPhone.Text = account.Phone;
                }
            }
            else
            {
                Response.Redirect(GlobalVariables.UrlHost);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckUser() == 0)
            {
                DbLink.Account newAccount = new DbLink.Account()
                {
                    Name = tbName.Text,
                    MiddleName = tbMiddleName.Text,
                    LastName = tbLastName.Text,
                    Email = tbEmail.Text,
                    Phone = tbPhone.Text,
                };
                DbLink.Group newGroup = new DbLink.Group() { Id = DefineGroup() };
                if (Mode == "add")
                {
                    newAccount.Login = tbLogin.Text;
                    newAccount.Password = tbPass.Text;
                    if (GlobalVariables.Link.AddUser(newAccount, newGroup))
                    {
                        if (Session["group"] == null)
                        {
                            Session["login"] = tbLogin.Text;
                            Session["name"] = tbName.Text;
                            Session["middlename"] = tbMiddleName.Text;
                            Session["lastname"] = tbLastName.Text;
                            Session["group"] = newGroup.Id;
                            Session["groupTitle"] = newGroup.Title;
                        }
                        else
                        {
                            Session["ok"] = "Администратор добавлен";
                            Response.Redirect(Request.Url.ToString());
                        }
                        Response.Redirect(GlobalVariables.UrlHost);
                    }
                    else
                    {
                        ErrorInsert.Visible = true;
                        showErrorInsert.Text = "Ошибка вставки в базу данных";
                    }
                }
                else // edit
                {
                    newAccount.Login = Session["login"].ToString();
                    newAccount.Password = account.Password;
                    if (GlobalVariables.Link.EditUser(newAccount))
                    {
                        Session["name"] = tbName.Text;
                        Session["middlename"] = tbMiddleName.Text;
                        Session["lastname"] = tbLastName.Text;
                        Session["ok"] = "Данные изменены";
                        Response.Redirect(Request.Url.ToString());
                    }
                    else
                    {
                        ErrorInsert.Visible = true;
                        showErrorInsert.Text = "Ошибка вставки в базу данных";
                    }
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

        private int DefineGroup()
        {
            int result = 2; // по умолчанию даем группу 2 
            object obj = Session["group"];
            if (obj != null)
            {
                Int32.TryParse(obj.ToString(), out result);
            }

            return result;
        }

        private int CheckUser()
        {
            if (!ErrorControls.NotEmptyTextBox(tbName))
            {
                errs.Add(ErrorName, "Поле не должно быть пустым");
            }
            if (!ErrorControls.NotEmptyTextBox(tbMiddleName))
            {
                errs.Add(ErrorMiddleName, "Поле не должно быть пустым");
            }
            if (!ErrorControls.NotEmptyTextBox(tbLastName))
            {
                errs.Add(ErrorLastName, "Поле не должно быть пустым");
            }
            if (!ErrorControls.NotEmptyTextBox(tbEmail))
            {
                errs.Add(ErrorEmail, "Поле не должно быть пустым");
            }
            else if (!ErrorControls.RegexTextBox(tbEmail, "^[0-9a-z_\\.-]+@[0-9a-z_\\.-]+\\.[a-z]{2,}?$"))
            {
                errs.Add(ErrorEmail, "Не правильный формат");
            }
            if (Mode == "add")
            {
                if (!ErrorControls.NotEmptyTextBox(tbLogin))
                {
                    errs.Add(ErrorLogin, "Поле не должно быть пустым");
                }
                else if (!ErrorControls.RegexTextBox(tbLogin, "^[a-zA-Z]+$"))
                {
                    errs.Add(ErrorLogin, "Только латинские буквы");
                }
                else if (GlobalVariables.Link.UniqueLogin(tbLogin.Text))
                {
                    ErrorControls.CountErrors(false);
                    errs.Add(ErrorLogin, "Присутствует в системе");
                }
                if (!ErrorControls.NotEmptyTextBox(tbPass))
                {
                    errs.Add(ErrorPass, "Поле не должно быть пустым");
                }
                else if (tbPass.Text != tbRePass.Text)
                {
                    ErrorControls.CountErrors(false);
                    errs.Add(ErrorPass, "Пароли должны совпадать");
                    errs.Add(ErrorRePass, "Пароли должны совпадать");
                }
                string captcha = Session["captcha"] as string;
                if (!ErrorControls.NotEmptyTextBox(tbCaptcha))
                {
                    errs.Add(ErrorCaptcha, "Поле не должно быть пустым");
                }
                else if ((Session["captcha"] as string) != tbCaptcha.Text.ToLower())
                {
                    ErrorControls.CountErrors(false);
                    errs.Add(ErrorCaptcha, "Символы введены не верно");
                }
                Session["captcha"] = null;
            }

            return ErrorControls.GetCount();
        }


    }
}