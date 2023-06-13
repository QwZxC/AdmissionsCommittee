using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Types;
using AdmissionsCommittee.Views.Windows.Speciality;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class EnrolleSpecialityViewModel : NotifyPropertyChangedObject
    {
        public EnrolleSpecialityViewModel()
        {
            Enrollees = DataBaseConnection.ApplicationContext.Enrollee.ToList();
            Specialities = new ObservableCollection<Speciality>(DataBaseConnection.ApplicationContext.Speciality);
            GoToDisabilityPageCommand = new LambdaCommand(OnGoToDisabilityPageCommandExecuted, CanGoToDisabilityPageCommandExecute);
            GoToCertificatePageCommand = new LambdaCommand(OnGoToCertificatePageCommandExecuted, CanGoToCertificatePageCommandExecute);
            OpenSpecialityWindowCommand = new LambdaCommand(OnOpenSpecialityWindowCommandExecuted, CanOpenSpecialityWindowCommandExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
        }

        public List<Enrollee> Enrollees { get; set; }

        public ObservableCollection<Speciality> Specialities { get; set; }

        #region OpenSpecialityWindowCommand

        public ICommand OpenSpecialityWindowCommand { get; set; }

        private bool CanOpenSpecialityWindowCommandExecute(object parameter)
        {
            return true;
        }

        private async void OnOpenSpecialityWindowCommandExecuted(object parameter)
        {
            SpecialityWindow specialityWindow = new SpecialityWindow();
            specialityWindow.ShowDialog();
            await DataBaseConnection.ApplicationContext.Speciality.ForEachAsync(speciality =>
            {
                if (!Specialities.Contains(speciality))
                {
                    Specialities.Add(speciality);
                }
            });
        }

        #endregion

        #region GoToDisabilityPageCommand

        public ICommand GoToDisabilityPageCommand { get; set; }

        public bool CanGoToDisabilityPageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToDisabilityPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.DisabilityPage);
        }

        #endregion

        #region GoToCertificatePageCommand

        public ICommand GoToCertificatePageCommand { get; set; }

        public bool CanGoToCertificatePageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToCertificatePageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.CertificatePage);
        }

        #endregion

        #region SaveCommand

        public ICommand SaveCommand { get; set; }

        public bool CanSaveCommandExecute(object parameter)
        {
            return Enrollees.All(enrollee => enrollee.Speciality != null);
        }

        public void OnSaveCommandExecuted(object parameter)
        {
            DataBaseConnection.ApplicationContext.SaveChanges();
        }

        #endregion
    }
}
