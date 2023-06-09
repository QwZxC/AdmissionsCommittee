using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Types;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class PlaceOfResidenceViewModel
    {

        public PlaceOfResidenceViewModel() 
        {
            GoToEducationPageCommand = new LambdaCommand(OnEducationPageCommandExecuted, CanGoToEducationPageCommandExecute);
            GoToSelectCitizenshipPageCommand = new LambdaCommand(OnGoToSelectCitizenshipPageCommandExecuted, CanGoToSelectCitizenshipPageExecute);
        }

        #region GoToEducationPageCommand

        public ICommand GoToEducationPageCommand { get; set; }

        public bool CanGoToEducationPageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnEducationPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.EducationPage);
        }

        #endregion

        #region GoToSelectCitizenshipPageCommand

        public ICommand GoToSelectCitizenshipPageCommand { get; set; }

        public bool CanGoToSelectCitizenshipPageExecute(object parameter)
        {
            return true;
        }

        public void OnGoToSelectCitizenshipPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.SelectCitizenshipPage);
        }

        #endregion
    }
}
