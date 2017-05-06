using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WebVacuum
{
    /// <summary>
    /// Сводное описание для Capcha
    /// </summary>
    public class Capcha : IHttpHandler, IRequiresSessionState
    {
        Random rand = new Random();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpeg";

            // Высота и ширина картинки
            int height = 50;
            int width = 170;
            string path = context.Request.PhysicalApplicationPath;

            // Объект изображения
            Bitmap captchaImage = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(captchaImage);
            // формируем капчу
            string captchaString = GenerateCaptchaString(6);
            HttpContext.Current.Session["captcha"] = captchaString;
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
            // выводим изображение
            captchaImage.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            context.Response.End();
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}