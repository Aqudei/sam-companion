using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAM_COMPANION2.Models
{
    public class SamContext : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }

        public SamContext() : base("SamContext")
        {

        }
    }
}
