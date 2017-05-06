using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionDb
{
    public class News
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        [Required(ErrorMessage = "Please enter the title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the content")]
        [Column(TypeName = "Text")]
        public string Content { get; set; }

        public virtual Account account { get; set; }
    }
}
