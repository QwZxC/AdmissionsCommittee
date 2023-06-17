using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Reports;
using AdmissionsCommittee.Reports.ExcelGenerators;
using AdmissionsCommittee.Types;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class LoadToFileViewModel
    {
        public LoadToFileViewModel()
        {
            Enrollees = DataBaseConnection.ApplicationContext.Enrollee.ToList();
            EnrolleeReport = new EnrolleeReport(Enrollees);
            ExcelGenerator = new ExcelGenerator();
            GoToWardPageCommand = new LambdaCommand(OnGoToWardPageCommandExecuted, CanGoToWardPageCommandExecute);
            LoadToExcelCommand = new LambdaCommand(OnLoadToExcelCommandexecuted, CanLoadToExcelCommandExecute);
        }

        public List<Enrollee> Enrollees { get; set; }

        public EnrolleeReport EnrolleeReport { get; set; }

        public ExcelGenerator ExcelGenerator { get; set; } 


        #region GoToWardPageCommand

        public ICommand GoToWardPageCommand { get; set; }

        public bool CanGoToWardPageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToWardPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.WardPage);
        }

        #endregion

        #region LoadToExcelCommand 

        public ICommand LoadToExcelCommand { get; set; }

        private bool CanLoadToExcelCommandExecute(object parameter) 
        {
            return true;
        }

        private void OnLoadToExcelCommandexecuted(object parameter)
        {
            byte[] ExelReport = ExcelGenerator.Generate(EnrolleeReport);
            try
            {
                File.WriteAllBytes("../../../Reports/Files/Приёмная коммисия.xlsx", ExelReport);
            }
            catch
            {
                MessageBox.Show("Пожалуйста,закройте файл и повторите сохранение", "Ошибка!", MessageBoxButton.OK);
            }

            EnrolleeReport.Enrollees = new List<Enrollee>();
        }

        #endregion
    }
}
