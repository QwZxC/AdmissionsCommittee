using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class SpecialityViewModel : NotifyPropertyChangedObject
    {
        private ObservableCollection<SpecialityDTO> specialities;
        private ObservableCollection<SpecialityDTO> selectedSpecialities;
        private bool isAllSelected;

        public SpecialityViewModel() 
        {
            Specialities = new ObservableCollection<SpecialityDTO>(DataBaseConnection.ApplicationContext.Speciality.ToList().ConvertAll(speciality =>
            {
                return new SpecialityDTO(speciality.Id, speciality.Name, speciality.DivisionСode);
            }));
            SelectedSpecialities = new ObservableCollection<SpecialityDTO>();
            Specialities.CollectionChanged += IsAllSelectedCheck;
            SelectedSpecialities.CollectionChanged += SelectedCollectionChanged;
            SelectedSpecialities.CollectionChanged += IsAllSelectedCheck;
            AddCommand = new LambdaCommand(OnAddCommandExecuted, CanAddCommandExecute);
            RemoveCommand = new LambdaCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            ChangeItemSelectionCommand = new LambdaCommand(OnChangeItemSelectionCommandExecuted, CanChangeItemSelectionCommandExecute);
            ChangeAllSelectionCommand = new LambdaCommand(OnChangeAllSelectionCommandExecuted, CanChangeAllSelectionCommandExecute);
        }

        public ObservableCollection<SpecialityDTO> Specialities 
        {
            get { return specialities; } 
            set { Set(ref specialities, value); }
        }

        public ObservableCollection<SpecialityDTO> SelectedSpecialities
        {
            get { return selectedSpecialities; }
            set { Set(ref selectedSpecialities, value); }
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
            Specialities.Add(new SpecialityDTO());
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
            DbSet<Speciality> dbSpecialities = DataBaseConnection.ApplicationContext.Speciality;
            SelectedSpecialities.ToList().ForEach(speciality => 
            {
                Speciality dbSpeciality = dbSpecialities.Find(speciality.Id);
                Specialities.Remove(speciality);
                if (dbSpeciality != null)
                {
                    dbSpecialities.Remove(dbSpeciality);
                }
            });
            SelectedSpecialities.Clear();
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
            DbSet<Speciality> dbSpecialities = DataBaseConnection.ApplicationContext.Speciality;
            Specialities.ToList().ForEach(speciality =>
            {
                Speciality dbSpeciality = dbSpecialities.Find(speciality.Id);
                if (dbSpeciality == null)
                {
                    dbSpecialities.Add(new Speciality(speciality.Name, speciality.DivisionСode));
                }
                else
                {
                    dbSpeciality.Name = speciality.Name;
                    dbSpeciality.DivisionСode = speciality.DivisionСode;
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
            SpecialityDTO speciality = parameter as SpecialityDTO;
            if (speciality.IsSelected)
                SelectedSpecialities.Add(speciality);
            else
                SelectedSpecialities.Remove(speciality);
        }

        #endregion

        #endregion

        private bool IsAllSaved()
        {
            return IsChanged = CheckIsSaved();
        }

        private bool CheckIsSaved()
        {
            DbSet<Speciality> dbSpecialities = DataBaseConnection.ApplicationContext.Speciality;
            if (Specialities.Count != dbSpecialities.Count())
            {
                return false;
            }
            return Specialities.All(speciality =>
            {
                Speciality dbSpeciality = dbSpecialities.Find(speciality.Id);
                if (dbSpeciality != null)
                {
                    return speciality.Name == dbSpeciality.Name &&
                           speciality.DivisionСode == dbSpeciality.DivisionСode;
                }
                return true;
            });
        }

        private bool IsAllValid()
        {
            return Specialities.All(enrollee => !string.IsNullOrWhiteSpace(enrollee.Name) &&
                                             !string.IsNullOrWhiteSpace(enrollee.DivisionСode));
        }

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
                    foreach (SpecialityDTO speciality in e.NewItems)
                        speciality.IsSelected = true;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (SpecialityDTO speciality in e.OldItems)
                        speciality.IsSelected = false;
                    break;
            }
        }
    }
}
