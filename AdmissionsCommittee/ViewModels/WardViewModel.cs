using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Types;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class WardViewModel
    {
        public WardViewModel()
        {
            GoToLoadToFilePageCommand = new LambdaCommand(OnGoToLoadToFilePageCommandExecuted, CanGoToLoadToFilePageCommandExecute);
            GoToDisabilityPageCommand = new LambdaCommand(OnGoToDisabilityPageCommandExecuted, CanGoToDisabilityPageCommandExecute);
        }

        #region GoToLoadToFilePageCommand

        public ICommand GoToLoadToFilePageCommand { get; set; }

        public bool CanGoToLoadToFilePageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToLoadToFilePageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.LoadToFilePage);
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
    }
}
