using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GalleryUploader
{
    public partial class Upload : System.Web.UI.Page
    {
        protected bool ErrorImage { get; set; }
        protected string OkMessage { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            OkMessage = string.Empty;
            if (Session["ok"] != null)
            {
                OkMessage = Session["ok"].ToString();
                Session["ok"] = null;
            }
        }

        protected void btUpload_Click(object sender, EventArgs e)
        {
            if (fuImage.PostedFile.FileName != "")
            {
                List<string> extensions = new List<string>() { ".png", ".gif", ".jpeg", ".jpg" };
                int maxLength = 10485760; // Максимальный размер файла. 10 Mb (10 * 1024 * 1024)
                try
                {
                    string fileExtension = Path.GetExtension(fuImage.PostedFile.FileName);
                    int fileLenght = fuImage.PostedFile.ContentLength;
                    if (!extensions.Contains(fileExtension.ToLower()))
                    {
                        throw new Exception("Файлы с таким расширением не принимаются для загрузки.");
                    }
                    if (fileLenght > maxLength)
                    {
                        throw new Exception("Размер файла не более 10 Mb.");
                    }
                    // Создание уникального имени для файла.
                    string newFileName = ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();// Guid.NewGuid().ToString();
                    // Создание пути для сохранения файла на сервере.
                    string newPathForSave = Path.Combine(Server.MapPath("tmp"), newFileName + fileExtension);
                    // Сохранение файла.
                    fuImage.SaveAs(newPathForSave);
                    Session["ok"] = "Файл загружен";
                    Response.Redirect(Request.Url.ToString());
                }
                catch (Exception ex)
                {
                    ErrorImage = true;
                    lbMessage.Text = ex.Message;
                }
            }
            else
            {
                ErrorImage = true;
                lbMessage.Text = "Загрузите файл";
            }
        }
    }
}