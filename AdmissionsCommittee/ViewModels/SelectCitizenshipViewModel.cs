using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class SelectCitizenshipViewModel
    {

        public SelectCitizenshipViewModel()
        {
            GoToPlaceOfResidencePageCommand = new LambdaCommand(OnGoToPlaceOfResidencePageCommandExecuted, CanGoToPlaceOfResidencePageCommandExecute);
            GoToRegistrationEnrollePageCommand = new LambdaCommand(OnGoToRegistrationEnrollePageCommandExecuted, CanGoToRegistrationEnrollePageCommandExecute);
        }

        #region GoToPlaceOfResidencePageCommand

        public ICommand GoToPlaceOfResidencePageCommand { get; set; }

        public bool CanGoToPlaceOfResidencePageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToPlaceOfResidencePageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.PlaceOfResidencePage);
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

    }
}
