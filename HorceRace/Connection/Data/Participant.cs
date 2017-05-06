using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Data
{
    public class Participant
    {
        public int Id { get; set; }
        public string Num { get; set; }
        [Required(ErrorMessage = "Please enter the Jokey of the horse")]
        public string Jokey { get; set; }
        [Required(ErrorMessage = "Please enter the Jokey of the horse")]
        public string Trener { get; set; }
        [Required(ErrorMessage = "Please enter the horse")]
        public string Horse { get; set; }

        public int Run { get; set; }

        public virtual Race race { get; set; }
    }
}
