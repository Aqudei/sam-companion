using System;
using System.Linq;

namespace SAM_COMPANION2.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
        public string StudentEmailAddress { get; set; }
        public string StudentMobilePhone { get; set; }
        public string ClassName { get; set; }
        public string Status { get; set; }

        public string Sex => StudentNumber.StartsWith("F") ? "Female" : "Male";
    }
}
