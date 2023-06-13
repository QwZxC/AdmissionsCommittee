using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class EducationViewModel : NotifyPropertyChangedObject
    {
        private ObservableCollection<Education> educations;
        private ObservableCollection<Education> selectedEducations;
        private bool isAllSelected;

        public EducationViewModel()
        {
            SelectedEducations = new ObservableCollection<Education>();
            Educations = new ObservableCollection<Education>(DataBaseConnection.ApplicationContext.Education);
            Educations.CollectionChanged += IsAllSelectedCheck;
            SelectedEducations.CollectionChanged += SelectedCollectionChanged;
            SelectedEducations.CollectionChanged += IsAllSelectedCheck;
            AddCommand = new LambdaCommand(OnAddCommandExecuted, CanAddCommandExecute);
            RemoveCommand = new LambdaCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            ChangeItemSelectionCommand = new LambdaCommand(OnChangeItemSelectionCommandExecuted, CanChangeItemSelectionCommandExecute);
            ChangeAllSelectionCommand = new LambdaCommand(OnChangeAllSelectionCommandExecuted, CanChangeAllSelectionCommandExecute);
        }

        public ObservableCollection<Education> Educations
        {
            get { return educations; }
            set { Set(ref educations, value); }
        }

        public ObservableCollection<Education> SelectedEducations
        {
            get { return selectedEducations; }
            set { Set(ref selectedEducations, value); }
        }

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
            Educations.Add(new Education());
        }

        #endregion

        #region RemoveCommand

        public ICommand RemoveCommand { get; set; }

        public bool CanRemoveCommandExecute(object parameter)
        {
            return SelectedEducations.Any();
        }

        public void OnRemoveCommandExecuted(object parameter)
        {
            SelectedEducations.ToList().ForEach(item => Educations.Remove(item));
            SelectedEducations.Clear();
        }

        #endregion

        #region SaveCommand

        public ICommand SaveCommand { get; set; }

        public bool CanSaveCommandExecute(object parameter)
        {
            return Educations.Any();
        }

        public void OnSaveCommandExecuted(object parameter)
        {
            DbSet<Education> dbEducation = DataBaseConnection.ApplicationContext.Education;
            Educations.ToList().ForEach(enrollees =>
            {
                if (!dbEducation.Contains(enrollees))
                {
                    dbEducation.Add(enrollees);
                }
            });
            DataBaseConnection.ApplicationContext.SaveChanges();
        }

        #endregion

        #region ChangeAllSelectionCommand

        public ICommand ChangeAllSelectionCommand { get; }

        private bool CanChangeAllSelectionCommandExecute(object parameter)
        {
            return Educations.Count != 0;
        }

        private void OnChangeAllSelectionCommandExecuted(object parameter)
        {
            if (IsAllSelected)
                Educations.ToList().FindAll(education => !education.IsSelected).ForEach(item => selectedEducations.Add(item));
            else
                selectedEducations.ToList().ForEach(selectedEducation => selectedEducations.Remove(selectedEducation));
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
            Education education = parameter as Education;
            if (education.IsSelected)
                SelectedEducations.Add(education);
            else
                SelectedEducations.Remove(education);
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
                    IsAllSelected = Educations.Count != 0 && Educations.All(item => item.IsSelected);
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
                    foreach (Education education in e.NewItems)
                        education.IsSelected = true;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Education education in e.OldItems)
                        education.IsSelected = false;
                    break;
            }
        }
    }
}
