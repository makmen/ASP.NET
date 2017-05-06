using Connection;
using Connection.Data;
using HorceRace.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HorceRace.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        public static EntityRepository Link = EntityRepository.GetInstance();
        private Random rand = new Random();

        public ActionResult Index()
        {
            if (HttpContext.Session["login"] != null
                && HttpContext.Session["groupid"].ToString() != "1")
            {
                return Redirect("~/Register/Edit");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(Account account)
        {
            bool valid = false;
            if (ModelState.IsValid)
            {
                if (Request.Form["password"].ToString() !=
                     Request.Form["repassword"].ToString())
                {
                    ViewBag.RepasswordError = "Пароли не совпадают";
                }
                else
                {
                    valid = true;
                }
            }
            if (HttpContext.Session["login"] == null)
            {
                string capcha = Request.Form["captcha"].ToString();
                string capchaSession = HttpContext.Session["captcha"].ToString();
                if (string.IsNullOrEmpty(capcha) &&
                    capcha != capchaSession)
                {
                    ViewBag.CaptchaError = "Не верный код";
                    valid = false;
                }
            }  
            // check captcha
            /**/
            if (valid)// сохраняем 
            {
                bool success = false;
                try
                {
                    account.group = Link.Groups.Get(DefineGroup());
                    if (account.group == null)
                    {
                        throw new Exception();
                    }
                    Link.Accounts.Create(account);
                    Link.Save();
                    success = true;
                }
                catch 
                {
                    // логирование
                }
                if (success)
                {
                    if (HttpContext.Session["login"] != null)
                    {
                        ViewShowMessage(success, "Администратор добавлен", "Ошибка записи в базу данных");
                        return View();
                    }

                    Session["loginid"] = account.Id;
                    Session["login"] = account.Login;
                    Session["groupid"] = account.group.Id;
                    Session["group"] = account.group.Title;

                    return Redirect("~");
                }
                else
                {
                    ViewBag.Error = "Ошибка записи в базу данных";
                }
            }

            return View();
        }

        public ActionResult Edit()
        {
            if (HttpContext.Session["login"] == null)
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }
            int id;
            Int32.TryParse(Session["loginid"].ToString(), out id);
            Account account = Link.Accounts.Get(id);

            return View("Index", account);
        }

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            string[] editFields = new string[] { "name", "middlename", "lastname", "email", "phone" };
            bool valid = true;
            foreach (string str in editFields)
            {
                if (ModelState.IsValidField(str))
                {
                    continue;
                }
                valid = false;
                break;
            }

            if (valid)
            {
                int id;
                Int32.TryParse(Session["loginid"].ToString(), out id);
                Account accountNew = Link.Accounts.Get(id);

                accountNew.Name = Request.Form["name"].ToString();
                accountNew.MiddleName = Request.Form["middlename"].ToString();
                accountNew.LastName = Request.Form["lastname"].ToString();
                accountNew.Email = Request.Form["email"].ToString();
                accountNew.Phone = Request.Form["phone"].ToString();

                bool success = false;
                try
                {
                    Link.Accounts.Update(accountNew);
                    Link.Save();
                    success = true;
                }
                catch
                {
                    // логирование
                }
                ViewShowMessage(success, "Данные сохранены", "Ошибка записи в базу данных");
            }


            return View("Index");
        }

        private int DefineGroup()
        {
            int result = 2; // по умолчанию даем группу 2 
            object obj = Session["groupid"];
            if (obj != null)
            {
                Int32.TryParse(obj.ToString(), out result);
            }

            return result;
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            return Redirect("~");
        }

        [HttpPost]
        public ActionResult Check(Account account)
        {
            bool success = false;
            try
            {
                success = Link.Accounts.Authorization(account);
            }
            catch
            {
                // логирование
                success = false;
            }
            if (success)
            {
                Session["loginid"] = account.Id;
                Session["login"] = account.Login;
                Session["groupid"] = account.group.Id;
                Session["group"] = account.group.Title;

                return Redirect("~");
            }
            else
            {
                ViewBag.AuthorizationError = true;
            }

            return View("~/Views/Article/Index.cshtml");
        }

        public ActionResult Forget()
        {
            if (HttpContext.Session["login"] != null)
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Forget(Account account)
        {
            if (ModelState.IsValidField("email"))
            {
                // отправка письма
                bool send = true;
                ViewShowMessage(send, "Письмо отправлено", "Ошибка отправки письма");
            }

            return View();
        }

        public ActionResult Change()
        {
            if (HttpContext.Session["login"] == null)
            {
                ViewBag.noAccess = "1";
                return View("~/Views/Article/NoAccess.cshtml");
            }

            return View();
        }
        [HttpPost]
        public ActionResult Change(ChangePass changePass)
        {
            if (ModelState.IsValid)
            {
                int id;
                Int32.TryParse( Session["loginid"].ToString(), out id);
                Account account = Link.Accounts.Get(id);
                if (account.Password == Request.Form["oldpassword"].ToString())
                {

                    account.Password = Request.Form["newpassword"].ToString();
                    bool success = false;
                    try
                    {
                        Link.Accounts.Update(account);
                        Link.Save();
                        success = true;
                    }
                    catch
                    {
                        // логирование
                    }
                    ViewShowMessage(success, "Данные сохранены", "Ошибка записи в базу данных");
                }
                else
                {
                    ModelState.AddModelError("oldpassword", "Не верный пароль");
                }
            }

            return View();
        }

        private void ViewShowMessage(bool success, string good, string bad)
        {
            if (success)
            {
                ViewBag.Success = good;
            }
            else
            {
                ViewBag.Error = bad;
            }
        }


        public ActionResult Captcha()
        {
            Bitmap captchaImage = DrawCapcha();
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";
            captchaImage.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            captchaImage.Dispose();

            return null;
        }

        private Bitmap DrawCapcha()
        {
            // Высота и ширина картинки
            int height = 50;
            int width = 170;

            // Объект изображения
            Bitmap captchaImage = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(captchaImage);
            // формируем капчу
            string captchaString = GenerateCaptchaString(6);
            Session["captcha"] = captchaString;
            gr.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), new Rectangle(0, 0, width, height));
            // работаем с текстом
            int step = width / captchaString.Length;
            for (int i = 0, posx = 0, posy = 0, font; i < captchaString.Length; i++)
            {
                posx += rand.Next(step / 2, step);
                posy = rand.Next(8, height / 2);
                font = rand.Next(12, 18);
                gr.DrawString(captchaString[i].ToString(),
                    new Font("", (float)font),
                    new SolidBrush(Color.FromArgb(rand.Next(250), rand.Next(250), rand.Next(250))),
                    new Point(posx, posy));
                gr.RotateTransform(rand.Next(-2, 1));
            }
            // добавляем линии
            for (int i = 0, linesNum = 10; i < linesNum; i++)
            {
                gr.DrawLine(new Pen(Color.FromArgb(rand.Next(250), rand.Next(250), rand.Next(250))),
                    rand.Next(width),
                    rand.Next(height),
                    rand.Next(width),
                    rand.Next(height)
                    );
            }

            return captchaImage;
        }

        private string GenerateCaptchaString(int length)
        {
            string symbols = "abcdefghijklmnopqrstuvwxyz0123456789";
            string captchaString = string.Empty;
            for (int i = 0; i < length; i++)
            {
                captchaString += symbols[rand.Next(0, symbols.Length)];
            }

            return captchaString;
        }

    }
}
