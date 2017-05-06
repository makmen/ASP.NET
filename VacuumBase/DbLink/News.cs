using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLink
{
    public class News
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public string Title { get; set; }

        [Column(TypeName = "Text")]
        public string Content { get; set; }

        public virtual Account account { get; set; }

    }
}
