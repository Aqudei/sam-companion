using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAM_COMPANION2.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
        public string ClassName { get; set; }
        public string AttendanceType { get; set; }
        public DateTime AttendanceDate { get; set; }

        public DateTime AttendanceDateOnly => AttendanceDate.Date;
    }
}
