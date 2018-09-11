using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace SAM_COMPANION2.ViewModels
{
    sealed class MainViewModel : Conductor<object>.Collection.OneActive
    {
        public MainViewModel()
        {
            DisplayName = "Students Attendance Monitoring (SAM) - Companion";

            Items.Add(IoC.Get<CollectionsViewModel>());
            Items.Add(IoC.Get<ReportingViewModel>());

            ViewAttached += MainViewModel_ViewAttached;
        }

        private void MainViewModel_ViewAttached(object sender, ViewAttachedEventArgs e)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
                "SamContext.sdf");

            if (!File.Exists(dbPath))
                return;

            var rslt = MessageBox.Show("Database already exist.\n" +
                                       $"Do you want to delete the existing database\nlocated at {dbPath}", "SAM Companion", MessageBoxButton.YesNo);

            if (rslt == MessageBoxResult.Yes)
            {
                File.Delete(dbPath);
            }
        }
    }
}
