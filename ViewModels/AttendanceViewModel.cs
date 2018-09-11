using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SAM_COMPANION2.Events;
using SAM_COMPANION2.Models;

namespace SAM_COMPANION2.ViewModels
{
    sealed class AttendanceViewModel : Screen, IHandle<AttendancesUpdated>
    {
        public BindableCollection<Attendance> Attendances { get; set; } = new BindableCollection<Attendance>();
        public AttendanceViewModel()
        {
            DisplayName = "Attendance";

            ViewAttached += AttendanceViewModel_ViewAttached;
        }

        private void AttendanceViewModel_ViewAttached(object sender, ViewAttachedEventArgs e)
        {
            Handle(null);
        }

        public void Handle(AttendancesUpdated message)
        {
            Attendances.Clear();
            using (var db = new SamContext())
            {
                Attendances.AddRange(db.Attendances.ToList());
            }
        }
    }
}
