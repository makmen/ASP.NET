using Connection.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class BaseConnect : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }

}
