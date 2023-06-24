using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Models.Context;
using AdmissionsCommittee.Models.DTO;
using AdmissionsCommittee.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class SelectCitizenshipViewModel : NotifyPropertyChangedObject
    {
        public SelectCitizenshipViewModel()
        {
            ApplicationContext db = DataBaseConnection.ApplicationContext;
            PlaceOfResidences = db.PlaceOfResidence.ToList();
            Citizenships = db.Citizenship.ToList();
            Districts = db.District.ToList();
            Enrollees = db.Enrollee.ToList().ConvertAll(enrollee =>
            {
                return new EnrolleeDTO(enrollee.Id, enrollee.Name, enrollee.Surname,
                                       enrollee.Patronymic, enrollee.Gender, enrollee.DateOfBirth,
                                       enrollee.Snils, enrollee.YearOfAdmission,
                                       db.PlaceOfResidence.Find(enrollee.PlaceOfResidenceId),
                                       db.Citizenship.Find(enrollee.CitizenshipId),
                                       db.District.Find(enrollee.DistrictId));
            });
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            GoToPlaceOfResidencePageCommand = new LambdaCommand(OnGoToPlaceOfResidencePageCommandExecuted, CanGoToPlaceOfResidencePageCommandExecute);
            GoToRegistrationEnrollePageCommand = new LambdaCommand(OnGoToRegistrationEnrollePageCommandExecuted, CanGoToRegistrationEnrollePageCommandExecute);
        }

        public List<Citizenship> Citizenships { get; set; }
        
        public List<EnrolleeDTO> Enrollees { get; set; }

        public List<PlaceOfResidence> PlaceOfResidences { get; set; }

        public List<District> Districts { get; set; }

        public bool IsChanged { get; set; }

        #region GoToPlaceOfResidencePageCommand

        public ICommand GoToPlaceOfResidencePageCommand { get; set; }

        public bool CanGoToPlaceOfResidencePageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToPlaceOfResidencePageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.EducationPage);
        }

        #endregion

        #region GoToRegistrationEnrollePageCommand

        public ICommand GoToRegistrationEnrollePageCommand { get; set; }

        public bool CanGoToRegistrationEnrollePageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToRegistrationEnrollePageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.RegistrationEnrollePage);
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
            DataBaseConnection.ApplicationContext.Enrollee.ForEachAsync(dbEnrollee =>
            {
                EnrolleeDTO enrollee = Enrollees.Find(e => e.Id == dbEnrollee.Id);
                dbEnrollee.Citizenship = enrollee.Citizenship;
                dbEnrollee.PlaceOfResidence = enrollee.PlaceOfResidence;
                dbEnrollee.District = enrollee.District;
            });
            DataBaseConnection.ApplicationContext.SaveChanges();
        }

        #endregion

        private bool IsAllValid()
        {
            return Enrollees.All(enrollee => enrollee.Citizenship != null &&
                                             enrollee.PlaceOfResidence != null &&
                                             enrollee.District != null);
        }
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
                return enrollee.Citizenship == dbEnrollee.Citizenship &&
                       enrollee.PlaceOfResidence == dbEnrollee.PlaceOfResidence &&
                       enrollee.District == dbEnrollee.District;
            });
        }
    }
}
