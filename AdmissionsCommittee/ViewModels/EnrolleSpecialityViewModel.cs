using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Models.DTO;
using AdmissionsCommittee.Types;
using AdmissionsCommittee.Views.Windows.Speciality;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class EnrolleSpecialityViewModel : NotifyPropertyChangedObject
    {
        public EnrolleSpecialityViewModel()
        {
            Specialities = new ObservableCollection<Speciality>(DataBaseConnection.ApplicationContext.Speciality);
            Enrollees = DataBaseConnection.ApplicationContext.Enrollee.ToList().ConvertAll(enrollee =>
            {
                return new EnrolleeDTO(enrollee.Id, enrollee.Name, enrollee.Surname,
                                       enrollee.Patronymic, enrollee.Gender, enrollee.DateOfBirth,
                                       enrollee.Snils, enrollee.YearOfAdmission, speciality: Specialities.FirstOrDefault(speciality => speciality.Id == enrollee.SpecialityId));
            });
            GoToDisabilityPageCommand = new LambdaCommand(OnGoToDisabilityPageCommandExecuted, CanGoToDisabilityPageCommandExecute);
            GoToCertificatePageCommand = new LambdaCommand(OnGoToCertificatePageCommandExecuted, CanGoToCertificatePageCommandExecute);
            OpenSpecialityWindowCommand = new LambdaCommand(OnOpenSpecialityWindowCommandExecuted, CanOpenSpecialityWindowCommandExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
        }

        public List<EnrolleeDTO> Enrollees { get; set; }

        public ObservableCollection<Speciality> Specialities { get; set; }
        
        public bool IsChanged { get; set; }

        #region OpenSpecialityWindowCommand

        public ICommand OpenSpecialityWindowCommand { get; set; }

        private bool CanOpenSpecialityWindowCommandExecute(object parameter)
        {
            return true;
        }

        private void OnOpenSpecialityWindowCommandExecuted(object parameter)
        {
            SpecialityWindow specialityWindow = new SpecialityWindow();
            specialityWindow.ShowDialog();
            DataBaseConnection.ApplicationContext.Speciality.ToList().ForEach(speciality =>
            {
                if (!Specialities.Contains(speciality))
                {
                    Specialities.Add(speciality);
                }
            });
        }

        #endregion

        #region GoToDisabilityPageCommand

        public ICommand GoToDisabilityPageCommand { get; set; }

        public bool CanGoToDisabilityPageCommandExecute(object parameter)
        {
            return IsAllValid();
        }

        public void OnGoToDisabilityPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.DisabilityPage);
        }

        #endregion

        #region GoToCertificatePageCommand

        public ICommand GoToCertificatePageCommand { get; set; }

        public bool CanGoToCertificatePageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToCertificatePageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.CertificatePage);
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
            Enrollees.ToList().ForEach(enrollee =>
            {
                dbEnrollees.Find(enrollee.Id).Speciality = enrollee.Speciality;
            });
            DataBaseConnection.ApplicationContext.SaveChanges();
        }

        #endregion

        private bool IsAllSaved()
        {
            return IsChanged = CheckIsSaved();
        }

        private bool CheckIsSaved()
        {
            DbSet<Enrollee> dbEnrollees = DataBaseConnection.ApplicationContext.Enrollee;
            return Enrollees.All(enrollee =>
            {
                Enrollee dbEnrollee = dbEnrollees.Find(enrollee.Id);
                return enrollee.Speciality == dbEnrollee.Speciality;
            });
        }

        private bool IsAllValid()
        {
            return Enrollees.All(enrollee => enrollee.Speciality != null);
        }
    }
}
