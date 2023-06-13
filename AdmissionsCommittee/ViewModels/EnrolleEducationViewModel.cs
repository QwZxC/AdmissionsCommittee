using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Types;
using AdmissionsCommittee.Views.Windows.Education;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class EnrolleEducationViewModel
    {
        public EnrolleEducationViewModel()
        {
            Enrollees = DataBaseConnection.ApplicationContext.Enrollee.ToList();
            Educations = new ObservableCollection<Education>(DataBaseConnection.ApplicationContext.Education);
            GoToAvarageScoreSnilsPageCommand = new LambdaCommand(OnAvarageScoreSnilsPageCommandExecuted, CanGoToAvarageScoreSnilsPageCommandExecute);
            GoToPlaceOfResidencePageCommand = new LambdaCommand(OnGoToPlaceOfResidencePageCommandExecuted, CanGoToPlaceOfResidencePageExecute);
            OpenEducationWindowCommand = new LambdaCommand(OnOpenEducationWindowCommandExecuted, CanOpenEducationWindowCommandExecute);
        }

        public List<Enrollee> Enrollees { get; set; }

        public ObservableCollection<Education> Educations { get; set; }

        #region OpenEducationWindowCommand

        public ICommand OpenEducationWindowCommand { get; set; }

        public bool CanOpenEducationWindowCommandExecute(object parameter)
        {
            return true;
        }

        public async void OnOpenEducationWindowCommandExecuted(object parameter)
        {
            EducationWindow educationWindow = new EducationWindow();
            educationWindow.ShowDialog();
            await DataBaseConnection.ApplicationContext.Education.ForEachAsync(education =>
            {
                if (!Educations.Contains(education))
                {
                    Educations.Add(education);
                }
            });
        }

        #endregion

        #region GoToAvarageScoreSnilsPageCommand

        public ICommand GoToAvarageScoreSnilsPageCommand { get; set; }

        public bool CanGoToAvarageScoreSnilsPageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnAvarageScoreSnilsPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.AvarageScoreSnilsPage);
        }

        #endregion

        #region GoToPlaceOfResidencePageCommand

        public ICommand GoToPlaceOfResidencePageCommand { get; set; }

        public bool CanGoToPlaceOfResidencePageExecute(object parameter)
        {
            return true;
        }

        public void OnGoToPlaceOfResidencePageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.SelectCitizenshipPage);
        }
        
        #endregion
    }
}
