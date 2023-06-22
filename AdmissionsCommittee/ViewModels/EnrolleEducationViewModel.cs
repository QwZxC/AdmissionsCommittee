using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Models.DTO;
using AdmissionsCommittee.Types;
using AdmissionsCommittee.Views.Windows.Education;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class EnrolleEducationViewModel
    {
        public EnrolleEducationViewModel()
        {
            Educations = new ObservableCollection<Education>(DataBaseConnection.ApplicationContext.Education);
            Enrollees = DataBaseConnection.ApplicationContext.Enrollee.ToList().ConvertAll(enrollee =>
            {
                return new EnrolleeDTO(enrollee.Id, enrollee.Name, enrollee.Surname,
                                       enrollee.Patronymic, enrollee.Gender, enrollee.DateOfBirth,
                                       enrollee.Snils, enrollee.YearOfAdmission, education: Educations.First(education => education.Id == enrollee.EducationId));
            });
            GoToAvarageScoreSnilsPageCommand = new LambdaCommand(OnAvarageScoreSnilsPageCommandExecuted, CanGoToAvarageScoreSnilsPageCommandExecute);
            GoToPlaceOfResidencePageCommand = new LambdaCommand(OnGoToPlaceOfResidencePageCommandExecuted, CanGoToPlaceOfResidencePageExecute);
            OpenEducationWindowCommand = new LambdaCommand(OnOpenEducationWindowCommandExecuted, CanOpenEducationWindowCommandExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
        }

        public List<EnrolleeDTO> Enrollees { get; set; }

        public ObservableCollection<Education> Educations { get; set; }

        public bool IsChanged { get; set; }

        #region OpenEducationWindowCommand

        public ICommand OpenEducationWindowCommand { get; set; }

        public bool CanOpenEducationWindowCommandExecute(object parameter)
        {
            return true;
        }

        public async void OnOpenEducationWindowCommandExecuted(object parameter)
        {
            EducationWindow educationWindow = new EducationWindow();
            educationWindow.ShowDialog();
            await DataBaseConnection.ApplicationContext.Education.ForEachAsync(education =>
            {
                if (!Educations.Contains(education))
                {
                    Educations.Add(education);
                }
            });
        }

        #endregion

        #region GoToAvarageScoreSnilsPageCommand

        public ICommand GoToAvarageScoreSnilsPageCommand { get; set; }

        public bool CanGoToAvarageScoreSnilsPageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnAvarageScoreSnilsPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.CertificatePage);
        }

        #endregion

        #region GoToPlaceOfResidencePageCommand

        public ICommand GoToPlaceOfResidencePageCommand { get; set; }

        public bool CanGoToPlaceOfResidencePageExecute(object parameter)
        {
            return true;
        }

        public void OnGoToPlaceOfResidencePageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.SelectCitizenshipPage);
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
                dbEnrollees.Find(enrollee.Id).Education = enrollee.Education;
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
            if (Enrollees.Count != dbEnrollees.Count())
            {
                return false;
            }
            return Enrollees.All(enrollee =>
            {
                Enrollee dbEnrollee = dbEnrollees.Find(enrollee.Id);
                return enrollee.Education == dbEnrollee.Education;
            });
        }

        private bool IsAllValid()
        {
            return Enrollees.All(enrollee => !string.IsNullOrWhiteSpace(enrollee.Name) &&
                                             !string.IsNullOrWhiteSpace(enrollee.Surname) &&
                                             enrollee.DateOfBirth > DateOnly.MinValue &&
                                             !string.IsNullOrWhiteSpace(enrollee.Snils) &&
                                             !string.IsNullOrWhiteSpace(enrollee.Gender));
        }

    }
}
