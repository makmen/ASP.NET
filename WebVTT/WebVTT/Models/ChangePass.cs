using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVTT.Models
{
    public class ChangePass
    {
        [Required(ErrorMessage = "Please enter your old password")]
        public string Oldpassword { get; set; }
        [Required(ErrorMessage = "Please enter your new password")]
        public string Newpassword { get; set; }
        [Compare("Newpassword", ErrorMessage = "Пароли не совпадают")]
        public string Renewpassword { get; set; }
    }
}