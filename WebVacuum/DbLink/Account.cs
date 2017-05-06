using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLink
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }
        public virtual Group group { get; set; }
        public ICollection<News> News_ { get; set; }
        public Account()
        {
            News_ = new List<News>();
        }
    }
}
