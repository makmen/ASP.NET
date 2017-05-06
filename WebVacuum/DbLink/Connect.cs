using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLink
{
    public class Connect
    {
        private static Connect instance;
        private BaseContent link;

        private Connect()
        {
            link = new BaseContent();
        }

        public static Connect GetInstance() 
        {
            if (instance == null)
            {
                instance = new Connect();
            }

            return instance;
        }

        public bool AddUser(Account newAccount, Group newGroup)
        {
            bool done = true;
            try
            {
                Group findGroup = (from grbd in link.Groups
                                   where grbd.Id == newGroup.Id
                                   select grbd).FirstOrDefault();
                newGroup.Title = findGroup.Title;
                newAccount.group = findGroup;
                link.Accounts.Add(newAccount);
                link.SaveChanges();
            }
            catch
            {
                done = false;
            }

            return done;
        }

        public bool EditUser(Account newAccount)
        {
            bool done = true;
            try
            {
                Account findAccount =
                    (from acbd in link.Accounts
                     where acbd.Login == newAccount.Login
                     select acbd).FirstOrDefault();
                findAccount.Name = newAccount.Name;
                findAccount.MiddleName = newAccount.MiddleName;
                findAccount.LastName = newAccount.LastName;
                findAccount.Email = newAccount.Email;
                findAccount.Phone = newAccount.Phone;
                findAccount.Password = newAccount.Password;

                link.SaveChanges();
            }
            catch
            {
                done = false;
            }

            return done;
        }

        public bool Authorization(Account newAccount)
        {
            bool done = true;
            try
            {
                Account findAccount =
                    (from acbd in link.Accounts
                     where acbd.Login == newAccount.Login &&
                     acbd.Password == newAccount.Password
                     select acbd).FirstOrDefault();
                if (findAccount != null)
                {
                    newAccount.Name = findAccount.Name;
                    newAccount.MiddleName = findAccount.MiddleName;
                    newAccount.LastName = findAccount.LastName;
                    newAccount.group = findAccount.group;
                }
                else
                {
                    done = false;
                }
            }
            catch
            {
                done = false;
            }

            return done;
        }

        public bool UniqueLogin(string str)
        {
            bool done = true;
            try
            {
                Account findAccount =
                    (from acbd in link.Accounts
                     where acbd.Login == str
                     select acbd).FirstOrDefault();
                if (findAccount == null)
                {
                    done = false;
                }
            }
            catch
            {
                done = false;
            }

            return done;
        }

        public bool GetUser(Account account)
        {
            bool done = true;
            Account findAccount =
                (from acbd in link.Accounts
                 where acbd.Login == account.Login
                 select acbd).FirstOrDefault();
            account.Id = findAccount.Id;
            account.Name = findAccount.Name;
            account.MiddleName = findAccount.MiddleName;
            account.LastName = findAccount.LastName;
            account.Email = findAccount.Email;
            account.Phone = findAccount.Phone;
            account.Password = findAccount.Password;

            return done;
        }

        public int GetTotalNews()
        {
            int total;
            try
            {
                total = link.News_.Count();
            }
            catch
            {
                total = -1;
            }

            return total;
        }

        public List<News> GetNews(int limit, int page)
        {
            int offset = (page - 1) * limit;
            List<News> listNews = link.News_.OrderByDescending(news => news.Created).Skip(offset).Take(limit).ToList();

            return listNews;
        }

        public News GetOneNews(int id)
        {
            News news;
            try
            {
                news =
                    (from newsdb in link.News_
                     where newsdb.Id == id
                     select newsdb).FirstOrDefault();
            }
            catch
            {
                news = null;
            }

            return news;
        }

        public bool AddNews(News newNews, string login)
        {
            bool done = true;
            try
            {
                Account findAccount =
                    (from acbd in link.Accounts
                     where acbd.Login == login
                     select acbd).FirstOrDefault();
                newNews.account = findAccount;
                link.News_.Add(newNews);
                link.SaveChanges();
            }
            catch
            {
                done = false;
            }

            return done;
        }

        public bool EditNews(News newNews, string login)
        {
            bool done = true;
            try
            {
                News editNews =
                    (from newsbd in link.News_
                     where newsbd.Id == newNews.Id
                     select newsbd).FirstOrDefault();
                Account findAccount =
                    (from acbd in link.Accounts
                     where acbd.Login == login
                     select acbd).FirstOrDefault();
                editNews.account = findAccount;
                editNews.Content = newNews.Content;
                editNews.Title = newNews.Title;
                link.SaveChanges();
            }
            catch
            {
                done = false;
            }

            return done;
        }
  

    }
}
