using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Types;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class DisabilityViewModel
    {

        public DisabilityViewModel()
        {
            GoToWardPageCommand = new LambdaCommand(OnGoToWardPageCommandExecuted, CanGoToWardPageCommandExecute);
            GoToAvarageScoreSnilsPageCommand = new LambdaCommand(OnGoToAvarageScoreSnilsPageCommandExecuted, CanGoToAvarageScoreSnilsPageExecute);
        }

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

        #region GoToAvarageScoreSnilsPageCommand

        public ICommand GoToAvarageScoreSnilsPageCommand { get; set; }

        public bool CanGoToAvarageScoreSnilsPageExecute(object parameter)
        {
            return true;
        }

        public void OnGoToAvarageScoreSnilsPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.AvarageScoreSnilsPage);
        }

        #endregion
    }
}
