using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDb
{
    public class BaseContent : DbContext
    {
        public BaseContent()
            : base("data source=PROGER3\\SQLEXPRESS;Initial Catalog=VttBase;Integrated Security=True;")
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<News> News_ { get; set; }
    }
}
