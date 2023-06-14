﻿using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class SpecialityViewModel : NotifyPropertyChangedObject
    {
        private ObservableCollection<Speciality> specialities;
        private ObservableCollection<Speciality> selectedSpecialities;
        private bool isAllSelected;

        public SpecialityViewModel() 
        {
            Specialities = new ObservableCollection<Speciality>(DataBaseConnection.ApplicationContext.Speciality);
            SelectedSpecialities = new ObservableCollection<Speciality>();
            Specialities.CollectionChanged += IsAllSelectedCheck;
            SelectedSpecialities.CollectionChanged += SelectedCollectionChanged;
            SelectedSpecialities.CollectionChanged += IsAllSelectedCheck;
            AddCommand = new LambdaCommand(OnAddCommandExecuted, CanAddCommandExecute);
            RemoveCommand = new LambdaCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            ChangeItemSelectionCommand = new LambdaCommand(OnChangeItemSelectionCommandExecuted, CanChangeItemSelectionCommandExecute);
            ChangeAllSelectionCommand = new LambdaCommand(OnChangeAllSelectionCommandExecuted, CanChangeAllSelectionCommandExecute);
        }

        public ObservableCollection<Speciality> Specialities 
        {
            get { return specialities; } 
            set { Set(ref specialities, value); }
        }

        public ObservableCollection<Speciality> SelectedSpecialities
        {
            get { return selectedSpecialities; }
            set { Set(ref selectedSpecialities, value); }
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
            Specialities.Add(new Speciality());
        }

        #endregion

        #region RemoveCommand

        public ICommand RemoveCommand { get; set; }

        public bool CanRemoveCommandExecute(object parameter)
        {
            return SelectedSpecialities.Any();
        }

        public void OnRemoveCommandExecuted(object parameter)
        {
            SelectedSpecialities.ToList().ForEach(item => Specialities.Remove(item));
            SelectedSpecialities.Clear();
        }

        #endregion

        #region SaveCommand

        public ICommand SaveCommand { get; set; }

        public bool CanSaveCommandExecute(object parameter)
        {
            return Specialities.Any();
        }

        public void OnSaveCommandExecuted(object parameter)
        {
            DbSet<Speciality> dbSpecialities = DataBaseConnection.ApplicationContext.Speciality;
            Specialities.ToList().ForEach(speciality =>
            {
                if (!dbSpecialities.Contains(speciality))
                {
                    dbSpecialities.Add(speciality);
                }
            });
            DataBaseConnection.ApplicationContext.SaveChanges();
        }

        #endregion

        #region ChangeAllSelectionCommand

        public ICommand ChangeAllSelectionCommand { get; }

        private bool CanChangeAllSelectionCommandExecute(object parameter)
        {
            return Specialities.Count != 0;
        }

        private void OnChangeAllSelectionCommandExecuted(object parameter)
        {
            if (IsAllSelected)
                Specialities.ToList().FindAll(education => !education.IsSelected).ForEach(SelectedSpecialities.Add);
            else
                SelectedSpecialities.ToList().ForEach(selectedEducation => Specialities.Remove(selectedEducation));
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
            Speciality speciality = parameter as Speciality;
            if (speciality.IsSelected)
                SelectedSpecialities.Add(speciality);
            else
                SelectedSpecialities.Remove(speciality);
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
                    IsAllSelected = Specialities.Count != 0 && Specialities.All(item => item.IsSelected);
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
                    foreach (Speciality speciality in e.NewItems)
                        speciality.IsSelected = true;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Speciality speciality in e.OldItems)
                        speciality.IsSelected = false;
                    break;
            }
        }
    }
}