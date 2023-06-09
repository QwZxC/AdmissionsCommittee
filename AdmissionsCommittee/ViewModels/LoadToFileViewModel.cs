using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Types;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class LoadToFileViewModel
    {
        public LoadToFileViewModel()
        {
            GoToWardPageCommand = new LambdaCommand(OnGoToWardPageCommandExecuted, CanGoToWardPageCommandExecute);
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
    }
}
