using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class RegistrationEnrolleViewModel : NotifyPropertyChangedObject
    {
        private ObservableCollection<Enrollee> enrollees;
        private bool isAllSelected;
        public RegistrationEnrolleViewModel()
        {
            Enrollees = new ObservableCollection<Enrollee>(DataBaseConnection.ApplicationContext.Enrollee);
            SelectedEnrolle = new ObservableCollection<Enrollee>();
            Enrollees.CollectionChanged += IsAllSelectedCheck;
            SelectedEnrolle.CollectionChanged += SelectedCollectionChanged;
            SelectedEnrolle.CollectionChanged += IsAllSelectedCheck;
            AddCommand = new LambdaCommand(OnAddCommandExecuted,CanAddCommandExecute);
            RemoveCommand = new LambdaCommand(OnRemoveCommandExecuted ,CanRemoveCommandExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            GoToSelectCitizenshipCommand = new LambdaCommand(OnGoToSelectCitizenshipCommandExecuted, CanGoToSelectCitizenshipCommandExecute);
            ChangeItemSelectionCommand = new LambdaCommand(OnChangeItemSelectionCommandExecuted, CanChangeItemSelectionCommandExecute);
            ChangeAllSelectionCommand = new LambdaCommand(OnChangeAllSelectionCommandExecuted, CanChangeAllSelectionCommandExecute);
        }

        public ObservableCollection<Enrollee> Enrollees
        {
            get { return enrollees; }
            set { Set(ref enrollees, value); }
        }

        public ObservableCollection<Enrollee> SelectedEnrolle { get; set; }

        public bool IsAllSelected 
        {
            get { return isAllSelected; }
            set { Set(ref isAllSelected, value); } 
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
            SelectedEnrolle.ToList().ForEach(item => Enrollees.Remove(item));
            SelectedEnrolle.Clear();
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

        #region ChangeItemSelectionCommand

        public ICommand ChangeItemSelectionCommand { get; }

        private bool CanChangeItemSelectionCommandExecute(object parameter)
        {
            return true;
        }

        private void OnChangeItemSelectionCommandExecuted(object parameter)
        {
            Enrollee enrollee = parameter as Enrollee;
            if (enrollee.IsSelected)
                SelectedEnrolle.Add(enrollee);
            else
                SelectedEnrolle.Remove(enrollee);
        }

        #endregion

        #region ChangeAllSelectionCommand

        public ICommand ChangeAllSelectionCommand { get; }

        private bool CanChangeAllSelectionCommandExecute(object parameter)
        {
            return Enrollees.Count != 0;
        }

        private void OnChangeAllSelectionCommandExecuted(object parameter)
        {
            if (IsAllSelected)
                Enrollees.ToList().FindAll(item => !item.IsSelected).ForEach(item => SelectedEnrolle.Add(item));
            else
                SelectedEnrolle.ToList().ForEach(selectedItem => SelectedEnrolle.Remove(selectedItem));
        }

        #endregion


        #endregion

        private void IsAllSelectedCheck(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Reset:
                case NotifyCollectionChangedAction.Replace:
                    IsAllSelected = Enrollees.Count != 0 && Enrollees.All(item => item.IsSelected);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    IsAllSelected = false;
                    break;
            }
        }

        private void SelectedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Enrollee item in e.NewItems)
                        item.IsSelected = true;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Enrollee item in e.OldItems)
                        item.IsSelected = false;
                    break;
            }
        }
    }
}
