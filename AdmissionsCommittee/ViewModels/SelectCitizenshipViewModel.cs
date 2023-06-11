using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class SelectCitizenshipViewModel : NotifyPropertyChangedObject
    {
        public SelectCitizenshipViewModel()
        {
            Enrollees = DataBaseConnection.ApplicationContext.Enrollee.ToList();
            PlaceOfResidences = DataBaseConnection.ApplicationContext.PlaceOfResidence.ToList();
            Districts = DataBaseConnection.ApplicationContext.District.ToList();
            Citizenships = new ObservableCollection<Citizenship>();
            DataBaseConnection.ApplicationContext.Citizenship.ToList().ForEach(Citizenships.Add);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            GoToPlaceOfResidencePageCommand = new LambdaCommand(OnGoToPlaceOfResidencePageCommandExecuted, CanGoToPlaceOfResidencePageCommandExecute);
            GoToRegistrationEnrollePageCommand = new LambdaCommand(OnGoToRegistrationEnrollePageCommandExecuted, CanGoToRegistrationEnrollePageCommandExecute);
        }

        public ObservableCollection<Citizenship> Citizenships { get; set; }
        
        public List<Enrollee> Enrollees { get; set; }

        public List<PlaceOfResidence> PlaceOfResidences { get; set; }

        public List<District> Districts { get; set; }

        #region GoToPlaceOfResidencePageCommand

        public ICommand GoToPlaceOfResidencePageCommand { get; set; }

        public bool CanGoToPlaceOfResidencePageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToPlaceOfResidencePageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.EducationPage);
        }

        #endregion

        #region GoToRegistrationEnrollePageCommand

        public ICommand GoToRegistrationEnrollePageCommand { get; set; }

        public bool CanGoToRegistrationEnrollePageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToRegistrationEnrollePageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.RegistrationEnrollePage);
        }

        #endregion

        #region SaveCommand

        public ICommand SaveCommand { get; set; }

        public bool CanSaveCommandExecute(object parameter)
        {
            return Enrollees.All(enrollee => enrollee.Citizenship != null);
        }

        public void OnSaveCommandExecuted(object parameter)
        {
            DataBaseConnection.ApplicationContext.SaveChanges();
        }

        #endregion
    }
}
