using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetOffice.ExcelApi;
using SAM_COMPANION2.Models;

namespace SAM_COMPANION2.Excel
{
    class ReportBuilder
    {
        public const int MaleRow = 14;
        public const int FemaleRow = 17;
        

        private readonly string _startUpPath;
        private readonly string _templateFile;

        public ReportBuilder()
        {
            _startUpPath = AppDomain.CurrentDomain.BaseDirectory;
            _templateFile = Path.Combine(_startUpPath, "Templates", "ALLSHS SF1-7 FORMS.xlsx");
        }

        public void Build(string outputFile, string className, DateTime reportDate)
        {
            using (var db = new SamContext())
            using (var excel = new NetOffice.ExcelApi.Application())
            {
                try
                {
                    excel.DisplayAlerts = false;
                    var wb = excel.Workbooks.Open(_templateFile, false, false);
                    var ws = (Worksheet)wb.Worksheets.First();
                    var students = db.Students.Where(s => s.ClassName == className).ToList();

                    AppendToList(ws, students.Where(s => s.Sex == "Female"), FemaleRow);
                    AppendToList(ws, students.Where(s => s.Sex == "Male"), MaleRow);

                    wb.SaveAs(Path.Combine(_startUpPath, "output.xlsx"));
                    wb.Close();

                    Process.Start(Path.Combine(_startUpPath, "output.xlsx"));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    if (excel.Workbooks.Any() == false)
                        excel.Quit();
                }
            }
        }


        private void AppendToList(Worksheet ws, IEnumerable<Student> queryable, int startRow)
        {
            var students = queryable.ToArray();
            for (int i = 0; i < students.Length; i++)
            {
                ws.Cells[startRow + i, 2].Value = students[i].StudentName;
                ws.Rows[startRow + i + 1].Insert();
            }
        }

        private void AppendDays(Worksheet ws, DateTime rd)
        {
            var d = new DateTime(rd.Year, rd.Month, 1);
            while (d.DayOfWeek != DayOfWeek.Monday)
            {
                d = d.AddDays(1);
            }

            
        }






    }
}
