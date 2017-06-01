using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFines
{
    public class Fine
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<History> Histories { get; set; }

        public Fine()
        {
            Histories = new HashSet<History>();
        }
    }
}
