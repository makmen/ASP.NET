using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Galery.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        

        public ActionResult Index()
        {
            GetListImages(3);
            return View();
        }

        private void GetListImages(int num)
        {
            List<string> ListImages = new List<string>();
            List<string[]> ListArray = new List<string[]>();
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("tmp"));
            DirectoryInfo[] resultDirectory = di.GetDirectories();
            DirectoryInfo[] dirs = di.GetDirectories();
            FileInfo[] files = di.GetFiles();
            string[] row = new string[num];
            foreach (FileInfo file in files)
            {
                ListImages.Add("tmp/" + file.Name);
            }
            int i = 0, numImageFilled = ListImages.Count;
            do
            {
                for (int j = 0, length = (numImageFilled > num) ? num : numImageFilled; j < length; j++)
                {
                    row[j] = ListImages[i++];
                }
                ListArray.Add(row);
                numImageFilled -= num;
                row = new string[num];
            } while (numImageFilled > 0);
            ViewBag.Images = ListArray;
        }

        public ActionResult Upload()
        {
            if (Session["ok"] != null)
            {
                ViewData["ok"] = Session["ok"].ToString();
                Session["ok"] = null;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                List<string> extensions = new List<string>() { ".png", ".gif", ".jpeg", ".jpg" };
                int maxLength = 10485760; // Максимальный размер файла. 10 Mb (10 * 1024 * 1024)
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                int fileLenght = upload.ContentLength;
                try {
                    string fileExtension = Path.GetExtension(fileName);
                    if (!extensions.Contains(fileExtension.ToLower()))
                    {
                        throw new Exception("Файлы с таким расширением не принимаются для загрузки.");
                    }
                    if (fileLenght > maxLength)
                    {
                        throw new Exception("Размер файла не более 10 Mb.");
                    }
                    // Создание уникального имени для файла.
                    string newFileName = ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
                    // Сохранение файла
                    upload.SaveAs(Server.MapPath("~/tmp/" + newFileName + fileExtension));
                    Session["ok"] = "Файл загружен";

                    return Redirect("/Home/Upload");
                }
                catch (Exception ex)
                {
                    ViewData["error"] = ex.Message;
                }
            }




            return View();
        }

    }
}
