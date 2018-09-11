using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SAM_COMPANION2.Excel;
using SAM_COMPANION2.Models;

namespace SAM_COMPANION2.ViewModels
{
    sealed class ReportingViewModel : Screen
    {
        private readonly ReportBuilder _reportBuilder;
        public BindableCollection<string> Classes { get; set; } = new BindableCollection<string>();
        public string SelectedClass { get; set; }

        public BindableCollection<string> Years { get; set; } = new BindableCollection<string>();
        public string SelectedYear { get; set; }

        public Dictionary<string, int> Months { get; set; } = new Dictionary<string, int>
        {
            { "January", 1 },
            { "February", 2 },
            { "March", 3 },
            { "April", 4 },
            { "May", 5 },
            { "June", 6 },
            { "July", 7 },
            { "August", 8 },
            { "September", 9 },
            { "October", 10 },
            { "November", 11 },
            { "December", 12 },
        };

        public KeyValuePair<string, int> SelectedMonth { get; set; }

        public ReportingViewModel(ReportBuilder reportBuilder)
        {
            _reportBuilder = reportBuilder;
            DisplayName = "Reporting";

            Activated += ReportingViewModel_Activated;
        }

        private void ReportingViewModel_Activated(object sender, ActivationEventArgs e)
        {
            using (var db = new SamContext())
            {
                Classes.Clear();
                var classes = db.Students.GroupBy(student => student.ClassName)
                    .Select(students => students.Key);
                Classes.AddRange(classes);
            }
        }

        public void GenerateSf2()
        {
            _reportBuilder.Build("output.xlsx", SelectedClass, new DateTime(int.Parse(SelectedYear), SelectedMonth.Value, 1));
        }
    }
}

