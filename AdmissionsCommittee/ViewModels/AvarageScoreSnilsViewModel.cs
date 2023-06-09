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
    public class AvarageScoreSnilsViewModel
    {
        public AvarageScoreSnilsViewModel()
        {
            GoToDisabilityPageCommand = new LambdaCommand(OnDisabilityPageCommandExecuted, CanDisabilityPageCommandExecute);
            GoToEducationPageCommand = new LambdaCommand(OnGoToEducationPageCommandExecuted, CanGoToEducationPageExecute);
        }

        #region GoToPlaceOfResidencePageCommand

        public ICommand GoToDisabilityPageCommand { get; set; }

        public bool CanDisabilityPageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnDisabilityPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.DisabilityPage);
        }

        #endregion

        #region GoToRegistrationEnrollePageCommand

        public ICommand GoToEducationPageCommand { get; set; }

        public bool CanGoToEducationPageExecute(object parameter)
        {
            return true;
        }

        public void OnGoToEducationPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.EducationPage);
        }

        #endregion
    }
}
