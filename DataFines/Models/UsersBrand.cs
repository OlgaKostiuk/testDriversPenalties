using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFines
{
    public class UsersBrand
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        public User User { get; set; }

        public Brand Brand { get; set; }

        public virtual ICollection<History> Histories { get; set; }

        public UsersBrand()
        {
            Histories = new HashSet<History>();
        }
    }
}
