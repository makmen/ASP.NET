using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VacuumBase.Classes;

namespace VacuumBase.News
{
    public partial class News : System.Web.UI.Page
    {
        protected string Mode { get; set; }
        private delegate void RunMode();
        private RunMode run { get; set; }
        public DbLink.News NewNews { get; set; }
        public string ErrMessage { get; set; }

        public int NumTotalNews { get; set; }
        public int NumPages { get; set; }

        protected int Id = 0;

        private int Limit {get; set;}

        private List<DbLink.News> ListNews { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DefineMode();
            run();
        }

        protected string GetUrl
        {
            get { return GlobalVariables.UrlHost; }
        }

        private void DefineMode()
        {
            string str = Request.QueryString["mode"];
            if (string.IsNullOrEmpty(str))
            {
                run = new RunMode(ViewAll);
            }
            else if (str == "view")
            {
                run = new RunMode(View);
            }
            else if (str == "add")
            {
                run = new RunMode(Add);
            }
            else if (str == "edit")
            {
                run = new RunMode(Edit);
            }
            else
            {
                run = new RunMode(ViewAll);
            }
        }

        protected void Add()
        {
            if (Session["login"] == null || Session["group"].ToString() != "1")
            {
                Response.Redirect(GlobalVariables.UrlStop);
            }
            Mode = "Add";
            NewNews = new DbLink.News();
        }

        protected void Edit()
        {
            if (Session["login"] == null || Session["group"].ToString() != "1")
            {
                Response.Redirect(GlobalVariables.UrlStop);
            }
            Mode = "Edit";
            string strId = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(strId))
            {
                Int32.TryParse(strId, out Id);
                if (Id <= 0)
                {
                    Response.Redirect(GlobalVariables.UrlHost + "/News/News.aspx");
                }
                NewNews = GlobalVariables.Link.GetOneNews(Id);
                if (NewNews == null)
                {
                    Response.Redirect(GlobalVariables.UrlHost + "/News/News.aspx");
                }
                if (!IsPostBack)
                {
                    tbTitle.Text = NewNews.Title;
                    tbContent.Text = NewNews.Content;
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckUser() == 0) // ошибок не было
            {
                if (Mode == "Add")
                {
                    NewNews = new DbLink.News();
                    NewNews.Created = DateTime.Now;
                }
                NewNews.Title = tbTitle.Text;
                NewNews.Content = tbContent.Text;
                if (Mode == "Add")
                {
                    if (GlobalVariables.Link.AddNews(NewNews, Session["login"].ToString()))
                    {
                        Response.Redirect(GlobalVariables.UrlHost + "/News/News.aspx");
                    }
                    else
                    {
                        ErrMessage = "Ошибка вставки в базу данных";
                    }
                }
                else
                {
                    if (GlobalVariables.Link.EditNews(NewNews, Session["login"].ToString()))
                    {
                        Response.Redirect(GlobalVariables.UrlHost + "/News/News.aspx");
                    }
                    else
                    {
                        ErrMessage = "Ошибка вставки в базу данных";
                    }
                }
            }
        }

        private int CheckUser()
        {
            ErrorControls.NotEmptyTextBox(tbTitle);
            ErrorControls.NotEmptyTextBox(tbContent);

            return ErrorControls.GetCount();
        }

        protected void ViewAll()
        {
            Mode = "ViewAll";
            Limit = 5;
            string str = Request.QueryString["p"];
            int page = 1;
            if (!string.IsNullOrEmpty(str))
            {
                Int32.TryParse(str, out page);
                if (page == 0) 
                {
                    page = 1; 
                }
            }
            ListNews = GlobalVariables.Link.GetNews(Limit, page);
            foreach(DbLink.News news in ListNews) {
                if (news.Content.Length > 400)
                {
                    news.Content = news.Content.Substring(0, 400);
                }
            }
            pagging();
            Repeter.DataSource = ListNews;
            Repeter.DataBind();
        }

        private void pagging()
        {
            NumTotalNews = GlobalVariables.Link.GetTotalNews();
            NumPages = (int)Math.Ceiling(NumTotalNews / (double)Limit);
        }

        protected void View()
        {
            Mode = "View";
            string strId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(strId))
            {
                Response.Redirect(GlobalVariables.UrlHost + "/News/News.aspx");
            }

            Int32.TryParse(strId, out Id);
            if (Id <= 0)
            {
                Response.Redirect(GlobalVariables.UrlHost + "/News/News.aspx");
            }
            NewNews = GlobalVariables.Link.GetOneNews(Id);
            if (NewNews == null)
            {
                Response.Redirect(GlobalVariables.UrlHost + "/News/News.aspx");
            }
        }

    }
}