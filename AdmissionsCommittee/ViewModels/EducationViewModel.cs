using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Types;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class EducationViewModel
    {
        public EducationViewModel()
        {
            GoToAvarageScoreSnilsPageCommand = new LambdaCommand(OnAvarageScoreSnilsPageCommandExecuted, CanGoToAvarageScoreSnilsPageCommandExecute);
            GoToPlaceOfResidencePageCommand = new LambdaCommand(OnGoToPlaceOfResidencePageCommandExecuted, CanGoToPlaceOfResidencePageExecute);
        }

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
