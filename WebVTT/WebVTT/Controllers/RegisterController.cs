using ConnectionDb;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebVTT.Models;

namespace WebVTT.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        public static Connect Link = Connect.GetInstance();
        Random rand = new Random();

        public ActionResult Index()
        {
            if (HttpContext.Session["login"] != null)
            {
                return Redirect("~");
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
            // check captcha
            string capcha = Request.Form["captcha"].ToString();
            string capchaSession = HttpContext.Session["captcha"].ToString();
            if (string.IsNullOrEmpty(capcha) &&
                capcha != capchaSession)
            {
                ViewBag.CaptchaError = "Не верный код";
                valid = false;
            }

            if (valid)// сохраняем 
            {
                if (Link.AddUser(account))
                {
                    Session["login"] = account.Login;
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
                return Redirect("~");
            }
            Account account = new Account();
            account.Login = Session["login"].ToString();
            Link.GetUser(account);

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
                Account accPassword = new Account();
                account.Login = accPassword.Login = Session["login"].ToString();
                Link.GetUser(accPassword);
                account.Password = accPassword.Password;
                Link.EditUser(account);
                ViewBag.Success = "Данные сохранены";
            }
            else
            {
                ViewBag.Error = "Ошибка записи в базу данных";
            }

            return View("Index");
        }

        public ActionResult Change()
        {
            if (HttpContext.Session["login"] == null)
            {
                return Redirect("~");
            }

            return View();
        }
        [HttpPost]
        public ActionResult Change(ChangePass changePass)
        {
            if (HttpContext.Session["login"] == null)
            {
                return Redirect("~");
            }

            if (ModelState.IsValid)
            {
                // check password
                Account account = new Account();
                account.Login = Session["login"].ToString();
                Link.GetUser(account);
                if (account.Password == Request.Form["oldpassword"].ToString())
                {
                    // сохраняем
                    account.Password = Request.Form["newpassword"].ToString();
                    if (Link.EditUser(account))
                    {
                        ViewBag.Success = "Данные сохранены";
                    }
                    else
                    {
                        ViewBag.Error = "Ошибка записи в базу данных";
                    }
                }
                else
                {
                    ModelState.AddModelError("oldpassword", "Не верный пароль");
                }
            }

            return View();
        }

        public ActionResult logout()
        {
            Session.Abandon();

            return Redirect("~");
        }
        
        [HttpPost]
        public ActionResult Check(Account account)
        {
            if (Link.Authorization(account))
            {
                Session["login"] = account.Login;
                return Redirect("~");
            }
            else
            {
              ViewBag.AuthorizationError = true;
            }

            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult Forget()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Forget(Account account)
        {
            if (ModelState.IsValidField("email"))
            {
                // отправка письма
                bool send = true;
                if (send)
                {
                    ViewBag.Success = "Данные сохранены";
                }
                else
                {
                    ViewBag.Error = "Ошибка отправки письма";
                }
            }

            return View();
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
