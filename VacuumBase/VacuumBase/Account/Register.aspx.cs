using DbLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VacuumBase.Classes;

namespace VacuumBase.Account
{
    public partial class Register : System.Web.UI.Page
    {
        public string Mode { get; set; } 
        public string ErrMessage { get; set; }
        public string OkMessage { get; set; } 
        

        private Dictionary<string, string> errs;

        public Dictionary<string, string> Errs
        {
            get { return errs; }
            set { errs = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ok"] != null)
            {
                OkMessage = Session["ok"].ToString();
                Session["ok"] = null;
            }
            errs = new Dictionary<string, string>();
            Mode = string.IsNullOrEmpty(Request.QueryString["mode"]) ? "add" : "edit";
            // на редактировании не залогиненный пользователь не должен находится
            if (Mode == "edit" && Session["login"] == null)
            {
                Response.Redirect(GlobalVariables.UrlHost);
            }
            // проверяем может ли пользователь создавать новых пользователей
            if (Session["login"] != null && 
                Session["group"].ToString() == "2" &&
                Mode != "edit")
            {
                Response.Redirect(GlobalVariables.UrlHost);
            }
            if (Mode == "edit")
            {
                EditUser();
            }
        }

        protected void EditUser()
        {
            DbLink.Account account = new DbLink.Account();
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

        protected void GetError(string str)
        {
            Response.Write(errs[str]);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckUser() == 0) // ошибок не было
            {
                DbLink.Account newAccount= new DbLink.Account()
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
                        ErrMessage = "Ошибка вставки в базу данных";
                    }
                }
                else
                {
                    newAccount.Login = Session["login"].ToString();
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
                        ErrMessage = "Ошибка вставки в базу данных";
                    }
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
                errs.Add(tbName.ID, "Поле не должно быть пустым");
            }
            if (!ErrorControls.NotEmptyTextBox(tbMiddleName))
            {
                errs.Add(tbMiddleName.ID, "Поле не должно быть пустым");
            }
            if (!ErrorControls.NotEmptyTextBox(tbLastName))
            {
                errs.Add(tbLastName.ID, "Поле не должно быть пустым");
            }
            if (!ErrorControls.NotEmptyTextBox(tbEmail))
            {
                errs.Add(tbEmail.ID, "Поле не должно быть пустым");
            }
            else if (!ErrorControls.RegexTextBox(tbEmail, "^[0-9a-z_\\.-]+@[0-9a-z_\\.-]+\\.[a-z]{2,}?$"))
            {
                errs.Add(tbEmail.ID, "Не правильный формат");
            }
            // если режим добавления проверяем это
            if (Mode == "add")
            {
                if (!ErrorControls.NotEmptyTextBox(tbLogin))
                {
                    errs.Add(tbLogin.ID, "Поле не должно быть пустым");
                }
                else if (!ErrorControls.RegexTextBox(tbLogin, "^[a-zA-Z]+$"))
                {
                    errs.Add(tbLogin.ID, "Только латинские буквы");
                }
                else if (GlobalVariables.Link.UniqueLogin(tbLogin.Text))
                {
                    errs.Add(tbLogin.ID, "Присутствует в системе");
                }
                if (!ErrorControls.NotEmptyTextBox(tbPass))
                {
                    errs.Add(tbPass.ID, "Поле не должно быть пустым");
                }
                else if (tbPass.Text != tbRePass.Text)
                {
                    errs.Add(tbPass.ID, "Пароли должны совпадать");
                    errs.Add(tbRePass.ID, "Пароли должны совпадать");
                }
            }



            return ErrorControls.GetCount();
        }
    }
}