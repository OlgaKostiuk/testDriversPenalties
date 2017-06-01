using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFines
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FName { get; set; }

        [Required]
        public string LName { get; set; }

        [Required]
        public string DriveLicense { get; set; }

        public virtual ICollection<UsersBrand> UserBrands { get; set; }

        public User()
        {
            UserBrands = new HashSet<UsersBrand>();
        }
    }
}
