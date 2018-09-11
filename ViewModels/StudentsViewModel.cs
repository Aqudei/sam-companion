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
    sealed class StudentsViewModel : Screen, IHandle<StudentsUpdated>
    {
        public BindableCollection<Student> Students { get; set; } = new BindableCollection<Student>();
        public StudentsViewModel(IEventAggregator eventAggregator)
        {
            DisplayName = "Students";

            eventAggregator.Subscribe(this);

            ViewAttached += StudentsViewModel_ViewAttached;
        }

        private void StudentsViewModel_ViewAttached(object sender, ViewAttachedEventArgs e)
        {
            Handle(null);
        }

        public void Handle(StudentsUpdated message)
        {
            Students.Clear();
            using (var db = new SamContext())
            {
                Students.AddRange(db.Students.ToList());
            }
        }
    }
}
