using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFines
{
    public class FinesContext: DbContext
    {
        public FinesContext(): base("FinesConnectionLocal")
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<UsersBrand> UsersBrand { get; set; }

        public DbSet<Fine> Fine { get; set; }
        public DbSet<History> History { get; set; }
    }
}
