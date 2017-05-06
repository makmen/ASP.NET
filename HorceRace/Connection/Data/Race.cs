using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.Data
{
    public class Race
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the title of the race")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the description of the race")]
        public string Description { get; set; }

        public string Status { get; set; }
        [Required(ErrorMessage = "Please enter the destance")]
        [RegularExpression(@"[1-9]\d+", ErrorMessage = "Please enter a correct field")]
        public int Distance { get; set; }

        public DateTime Created { get; set; }

        public virtual Account account { get; set; }
        public ICollection<Participant> Participants { get; set; }

        public Race()
        {
            Participants = new List<Participant>();
        }

    }
}
