using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Win32;
using SAM_COMPANION2.Events;
using SAM_COMPANION2.Importers;
using Unity.Interception.Utilities;

namespace SAM_COMPANION2.ViewModels
{
    sealed class CollectionsViewModel : Conductor<object>.Collection.OneActive,
        IHandle<StudentsUpdated>, IHandle<AttendancesUpdated>
    {
        private readonly StudentsImporter _studentsImporter;
        private readonly IEventAggregator _eventAggregator;
        private readonly StudentsViewModel _studentsViewModel;
        private readonly AttendanceImporter _attendanceImporter;
        private readonly AttendanceViewModel _attendanceViewModel;

        public CollectionsViewModel(StudentsImporter studentsImporter,
            IEventAggregator eventAggregator, StudentsViewModel studentsViewModel,
             AttendanceImporter attendanceImporter, AttendanceViewModel attendanceViewModel)
        {
            DisplayName = "Data";

            _studentsImporter = studentsImporter;
            _eventAggregator = eventAggregator;
            _studentsViewModel = studentsViewModel;
            _attendanceImporter = attendanceImporter;
            _attendanceViewModel = attendanceViewModel;

            Items.Add(studentsViewModel);
            Items.Add(attendanceViewModel);

            eventAggregator.Subscribe(this);
        }

        public async void ImportAttendance()
        {
            var files = AskFiles();
            foreach (var file in files)
                await _attendanceImporter.ImportAsync(file);
            _eventAggregator.PublishOnCurrentThread(new Events.AttendancesUpdated());
        }

        public async void ImportStudents()
        {
            var files = AskFiles();
            foreach (var file in files)
                await _studentsImporter.ImportAsync(file);
            _eventAggregator.PublishOnCurrentThread(new Events.StudentsUpdated());
        }

        private IEnumerable<string> AskFiles()
        {
            var ofd = new OpenFileDialog
            {
                Multiselect = true,
                CheckFileExists = true
            };
            var rslt = ofd.ShowDialog();
            if (rslt.HasValue && rslt.Value)
            {
                return ofd.FileNames;
            }

            return new List<string>();
        }

        public void Handle(StudentsUpdated message)
        {
            ActivateItem(_studentsViewModel);
        }

        public void Handle(AttendancesUpdated message)
        {
            ActivateItem(_attendanceViewModel);
        }
    }
}
