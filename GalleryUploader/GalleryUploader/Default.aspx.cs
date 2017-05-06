using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GalleryUploader
{
    public partial class Default : System.Web.UI.Page
    {
        private List<string> ListImages { get; set; }
        private List<string[]> ListArray { get; set; } 
        private int NumImageRow = 3;
        protected void Page_Load(object sender, EventArgs e)
        {
            ListImages = new List<string>();
            ListArray = new List<string[]>();
            GetListImages();
            repeater.DataSource = ListArray;
            repeater.DataBind();
        }

        private void GetListImages()
        {
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("tmp"));
            DirectoryInfo[] resultDirectory = di.GetDirectories();
            DirectoryInfo[] dirs = di.GetDirectories();
            FileInfo[] files = di.GetFiles();
            string[] row = new string[NumImageRow];
            foreach (FileInfo file in files)
            {
                ListImages.Add("tmp/" + file.Name);
            }
            int i = 0, numImageFilled = ListImages.Count;
            do {
                for (int j = 0, length = (numImageFilled > NumImageRow) ? NumImageRow : numImageFilled; j < length; j++)
                {
                    row[j] = ListImages[i++];
                }
                ListArray.Add(row);
                numImageFilled -= NumImageRow;
                row = new string[NumImageRow];
            } while (numImageFilled > 0);
        }

        protected string GetPath(object obj, int index)
        {
            string[] row = obj as string[];
            if (row != null && index < NumImageRow) 
            {
                return row[index];
            }

            return "";
        }
    }
}