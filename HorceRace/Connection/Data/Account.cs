using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Data
{
    public class Account
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your middlename")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please enter your lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter your login")]
        [RegularExpression(@"[a-zA-Z]+", ErrorMessage = "Please enter a valid login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
        public virtual Group group { get; set; }
        public ICollection<Race> Races { get; set; }
        public Account()
        {
            Races = new List<Race>();
        }

    }
}
