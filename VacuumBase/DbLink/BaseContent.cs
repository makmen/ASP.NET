using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLink
{
    class BaseContent : DbContext
    {
        public BaseContent()
            : base("VacuumBase")
        { }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<News> News_ { get; set; }
    }
}
