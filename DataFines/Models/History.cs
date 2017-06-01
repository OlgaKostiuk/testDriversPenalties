using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFines
{
    public class History
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Fine))]
        public int FineId { get; set; }

        [ForeignKey(nameof(UserBrands))]
        public int UsersBrandId { get; set; }

        public bool State { get; set; }

        public Fine Fine { get; set; }
        public UsersBrand UserBrands { get; set; }
    }
}
