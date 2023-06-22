using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Models.DTO;
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
        private ObservableCollection<EducationDTO> educations;
        private ObservableCollection<EducationDTO> selectedEducations;
        private bool isAllSelected;

        public EducationViewModel()
        {
            SelectedEducations = new ObservableCollection<EducationDTO>();
            Educations = new ObservableCollection<EducationDTO>(DataBaseConnection.ApplicationContext.Education.ToList().ConvertAll(education =>
            {
                return new EducationDTO(education.Id, education.After11School, education.After9School, education.AdditionalEducation);
            }));
            Educations.CollectionChanged += IsAllSelectedCheck;
            SelectedEducations.CollectionChanged += SelectedCollectionChanged;
            SelectedEducations.CollectionChanged += IsAllSelectedCheck;
            AddCommand = new LambdaCommand(OnAddCommandExecuted, CanAddCommandExecute);
            RemoveCommand = new LambdaCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            ChangeItemSelectionCommand = new LambdaCommand(OnChangeItemSelectionCommandExecuted, CanChangeItemSelectionCommandExecute);
            ChangeAllSelectionCommand = new LambdaCommand(OnChangeAllSelectionCommandExecuted, CanChangeAllSelectionCommandExecute);
        }

        public ObservableCollection<EducationDTO> Educations
        {
            get { return educations; }
            set { Set(ref educations, value); }
        }

        public ObservableCollection<EducationDTO> SelectedEducations
        {
            get { return selectedEducations; }
            set { Set(ref selectedEducations, value); }
        }

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
            Educations.Add(new EducationDTO());
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
            return IsAllValid() && !IsAllSaved();
        }

        public void OnSaveCommandExecuted(object parameter)
        {
            DbSet<Education> dbEducations = DataBaseConnection.ApplicationContext.Education;
            Educations.ToList().ForEach(education =>
            {
                if (education.Id == 0)
                {
                    dbEducations.Add(new Education(education.After11School, education.After9School, education.AdditionalEducation));
                }
                else
                {
                    Education dbEducation = dbEducations.Find(education.Id);
                    dbEducation.After9School = education.After9School;
                    dbEducation.After11School = education.After11School;
                    dbEducation.AdditionalEducation = education.AdditionalEducation;
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
            EducationDTO education = parameter as EducationDTO;
            if (education.IsSelected)
            {
                SelectedEducations.Add(education);
            }
            else
            {
                SelectedEducations.Remove(education);
            }
        }

        #endregion

        #endregion

        private bool IsAllSaved()
        {
            return IsChanged = CheckIsSaved();
        }

        private bool CheckIsSaved()
        {
            DbSet<Education> dbEducations = DataBaseConnection.ApplicationContext.Education;
            if (Educations.Count != dbEducations.Count())
            {
                return false;
            }
            return Educations.All(education =>
            {
                Education dbEducation = dbEducations.Find(education.Id);
                if (dbEducation != null)
                {
                    return education.After11School == dbEducation.After11School &&
                           education.After9School == dbEducation.After9School &&
                           education.AdditionalEducation == dbEducation.AdditionalEducation;
                }
                return true;
            });
        }

        private bool IsAllValid()
        {
            return Educations.All(education => education.After11School || 
                                               education.After9School ||
                                               !string.IsNullOrWhiteSpace(education.AdditionalEducation));
        }


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
                    foreach (EducationDTO education in e.NewItems)
                        education.IsSelected = true;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (EducationDTO education in e.OldItems)
                        education.IsSelected = false;
                    break;
            }
        }
    }
}
