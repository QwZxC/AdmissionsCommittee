using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class RegistrationEnrolleViewModel : NotifyPropertyChangedObject
    {
        private ObservableCollection<Enrollee> enrollees;

        public RegistrationEnrolleViewModel()
        {
            Enrollees = new ObservableCollection<Enrollee>(DataBaseConnection.ApplicationContext.Enrollee);
            AddCommand = new LambdaCommand(OnAddCommandExecuted,CanAddCommandExecute);
            RemoveCommand = new LambdaCommand(OnRemoveCommandExecuted ,CanRemoveCommandExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            GoToSelectCitizenshipCommand = new LambdaCommand(OnGoToSelectCitizenshipCommandExecuted, CanGoToSelectCitizenshipCommandExecute);
        }

        public ObservableCollection<Enrollee> Enrollees
        {
            get { return enrollees; }
            set { Set(ref enrollees, value); }
        }

        #region Commands

        #region AddCommand

        public ICommand AddCommand { get; set; }

        public bool CanAddCommandExecute(object parameter)
        {
            return true;
        }

        public void OnAddCommandExecuted(object parameter)
        {
            Enrollees.Add(new Enrollee());
        }

        #endregion

        #region RemoveCommand

        public ICommand RemoveCommand { get; set; }

        public bool CanRemoveCommandExecute(object parameter)
        {
            return true;
        }

        public void OnRemoveCommandExecuted(object parameter)
        {
            
        }

        #endregion

        #region SaveCommand

        public ICommand SaveCommand { get; set; }

        public bool CanSaveCommandExecute(object parameter)
        {
            return Enrollees.Any();
        }

        public void OnSaveCommandExecuted(object parameter)
        {
            DbSet<Enrollee> dbEnrolle = DataBaseConnection.ApplicationContext.Enrollee;
            Enrollees.ToList().ForEach(enrollees =>
            {
                if (!dbEnrolle.Contains(enrollees))
                {
                    dbEnrolle.Add(enrollees);
                }
            });
            DataBaseConnection.ApplicationContext.SaveChanges();
        }

        #endregion

        #region GoToSelectCitizenshipCommand

        public ICommand GoToSelectCitizenshipCommand { get; set; }

        public bool CanGoToSelectCitizenshipCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToSelectCitizenshipCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.SelectCitizenshipPage);
        }

        #endregion

        #endregion
    }
}
