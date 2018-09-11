using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using SAM_COMPANION2.Models;

namespace SAM_COMPANION2.Importers
{
    public class AttendanceImporter : ImporterBase
    {
        public override void Import(string filename)
        {
            var newlyAdded = new List<Attendance>();

            using (var file = File.OpenText(filename))
            using (var csvReader = new CsvReader(file, CsvConfig))
            {
                var attendances = csvReader.GetRecords<Attendance>();
                using (var db = new SamContext())
                {
                    foreach (var attendance in attendances)
                    {
                        if (!db.Attendances.Any(at => at.ClassName == attendance.ClassName &&
                                                      at.StudentNumber == attendance.StudentNumber &&
                                                      at.AttendanceType == attendance.AttendanceType &&
                                                      at.AttendanceDate == attendance.AttendanceDate) &&

                            !newlyAdded.Any(at => at.ClassName == attendance.ClassName &&
                                                 at.StudentNumber == attendance.StudentNumber &&
                                                 at.AttendanceType == attendance.AttendanceType &&
                                                 at.AttendanceDate == attendance.AttendanceDate))
                        {
                            db.Attendances.Add(attendance);
                            newlyAdded.Add(attendance);
                        }
                    }

                    db.SaveChanges();
                }
            }
        }
    }
}
