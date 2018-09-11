using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using SAM_COMPANION2.Models;

namespace SAM_COMPANION2.Importers
{
    public class StudentsImporter : ImporterBase
    {
        public override void Import(string filename)
        {
            using (var file = File.OpenText(filename))
            using (var csvReader = new CsvReader(file, CsvConfig))
            {
                var students = csvReader.GetRecords<Student>();
                var newStudents = new List<Student>();

                using (var db = new SamContext())
                {
                    foreach (var student in students)
                    {
                        if (db.Students.Any(s => s.StudentNumber == student.StudentNumber) ||
                            newStudents.Any(s => s.StudentNumber == student.StudentNumber))

                            continue;
                        db.Students.Add(student);
                        newStudents.Add(student);
                    }

                    db.SaveChanges();
                }
            }
        }
    }
}
