using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Models.DTO;
using AdmissionsCommittee.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class RegistrationEnrolleViewModel : NotifyPropertyChangedObject
    {
        private ObservableCollection<EnrolleeDTO> enrollees;
        private bool isAllSelected;
        public RegistrationEnrolleViewModel()
        {
            Enrollees = new ObservableCollection<EnrolleeDTO>(DataBaseConnection.ApplicationContext.Enrollee.ToList().ConvertAll(enrollee =>
            {
                return new EnrolleeDTO(enrollee.Id, enrollee.Name, enrollee.Surname,
                                       enrollee.Patronymic, enrollee.Gender, enrollee.DateOfBirth,
                                       enrollee.Snils, enrollee.YearOfAdmission);
            }));
            SelectedEnrolle = new ObservableCollection<EnrolleeDTO>();
            RemovedEnrolle = new List<Enrollee>();
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

        public ObservableCollection<EnrolleeDTO> Enrollees
        {
            get { return enrollees; }
            set { Set(ref enrollees, value); }
        }

        public ObservableCollection<EnrolleeDTO> SelectedEnrolle { get; set; }
        public List<Enrollee> RemovedEnrolle { get; set; }

        public bool IsAllSelected 
        {
            get { return isAllSelected; }
            set { Set(ref isAllSelected, value); } 
        }

        public bool IsChanged { get; set; }

        #region Commands

        #region AddCommand

        public ICommand AddCommand { get; set; }

        public bool CanAddCommandExecute(object parameter)
        {
            return true;
        }

        public void OnAddCommandExecuted(object parameter)
        {
            Enrollees.Add(new EnrolleeDTO());
        }

        #endregion

        #region RemoveCommand

        public ICommand RemoveCommand { get; set; }

        public bool CanRemoveCommandExecute(object parameter)
        {
            return SelectedEnrolle.Any();
        }

        public void OnRemoveCommandExecuted(object parameter)
        {
            SelectedEnrolle.ToList().ForEach(enrolee => Enrollees.Remove(enrolee));
            DbSet<Enrollee> dbEnroleee = DataBaseConnection.ApplicationContext.Enrollee;
            SelectedEnrolle.ToList().ForEach(enrollee => 
            {
                Enrollee dbEnrollee = dbEnroleee.Find(enrollee.Id);
                if (dbEnrollee != null)
                {
                    RemovedEnrolle.Add(dbEnrollee);
                    IsChanged = false;
                }
            });
            SelectedEnrolle.Clear();
        }

        #endregion

        #region SaveCommand

        public ICommand SaveCommand { get; set; }

        public bool CanSaveCommandExecute(object parameter)
        {
            return IsAllValid() && !IsAllSaved();
        }

        public void OnSaveCommandExecuted(object parameter)
        {
            DbSet<Enrollee> dbEnrollees = DataBaseConnection.ApplicationContext.Enrollee;
            RemovedEnrolle.ForEach(enrollee => dbEnrollees.Remove(enrollee));
            Enrollees.ToList().ForEach(enrollee =>
            {
                if (enrollee.Id == 0)
                {
                    dbEnrollees.Add(new Enrollee(enrollee.Name, enrollee.Surname, enrollee.Patronymic,
                                                enrollee.Gender, enrollee.DateOfBirth, enrollee.Snils,
                                                enrollee.YearOfAdmission));
                }
                else
                {
                    Enrollee dbEnrollee = dbEnrollees.Find(enrollee.Id);
                    dbEnrollee.Name = enrollee.Name;
                    dbEnrollee.Surname = enrollee.Surname;
                    dbEnrollee.Patronymic = enrollee.Patronymic;
                    dbEnrollee.Gender = enrollee.Gender;
                    dbEnrollee.DateOfBirth = enrollee.DateOfBirth;
                    dbEnrollee.Snils = enrollee.Snils;
                    dbEnrollee.YearOfAdmission = enrollee.YearOfAdmission;
                }
            });
            DataBaseConnection.ApplicationContext.SaveChanges();
        }

        #endregion

        #region GoToSelectCitizenshipCommand

        public ICommand GoToSelectCitizenshipCommand { get; set; }

        public bool CanGoToSelectCitizenshipCommandExecute(object parameter)
        {
            return Enrollees.Any() && IsAllValid();
        }

        public void OnGoToSelectCitizenshipCommandExecuted(object parameter)
        {
            if (!IsChanged && MessageBox.Show("Вы не сохранили изменения.", "Вы действительно хотите продолжить?", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                return;
            }
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
            EnrolleeDTO enrollee = parameter as EnrolleeDTO;
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

        private bool IsAllSaved()
        {
            return IsChanged = CheckIsSaved();
        }

        private bool CheckIsSaved()
        {
            DbSet<Enrollee> dbEnrollees = DataBaseConnection.ApplicationContext.Enrollee;
            if (Enrollees.Count != dbEnrollees.Count())
            {
                return false;
            }
            return Enrollees.All(enrollee =>
            {
                Enrollee dbEnrollee = dbEnrollees.Find(enrollee.Id);
                if (dbEnrollee != null)
                {
                    return enrollee.Name == dbEnrollee.Name &&
                           enrollee.Surname == dbEnrollee.Surname &&
                           enrollee.Patronymic == dbEnrollee.Patronymic &&
                           enrollee.Gender == dbEnrollee.Gender &&
                           enrollee.DateOfBirth == dbEnrollee.DateOfBirth &&
                           enrollee.Snils == dbEnrollee.Snils;
                }
                return true;
            });
        }

        private bool IsAllValid()
        {
            return Enrollees.All(enrollee => !string.IsNullOrWhiteSpace(enrollee.Name) &&
                                             !string.IsNullOrWhiteSpace(enrollee.Surname) &&
                                             enrollee.DateOfBirth > DateTime.MinValue &&
                                             !string.IsNullOrWhiteSpace(enrollee.Snils) &&
                                             !string.IsNullOrWhiteSpace(enrollee.Gender));
        }

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
                    foreach (EnrolleeDTO item in e.NewItems)
                        item.IsSelected = true;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (EnrolleeDTO item in e.OldItems)
                        item.IsSelected = false;
                    break;
            }
        }
    }
}
