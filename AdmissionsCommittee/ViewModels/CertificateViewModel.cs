using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Models.DTO;
using AdmissionsCommittee.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class CertificateViewModel
    {
        public CertificateViewModel()
        {
            Certificates = new ObservableCollection<Certificate>(DataBaseConnection.ApplicationContext.Certificate);
            GoToSpecialityPageCommand = new LambdaCommand(OnDisabilityPageCommandExecuted, CanDisabilityPageCommandExecute);
            GoToEducationPageCommand = new LambdaCommand(OnGoToEducationPageCommandExecuted, CanGoToEducationPageExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            LoadImageCommand = new LambdaCommand(OnLoadImageCommandExecuted, CanLoadImageCommandExecute);
            RemoveImageCommand = new LambdaCommand(OnRemoveImageCommandExecuted, CanRemoveImageCommandExecute);
            LoadData();
        }

        public List<EnrolleeDTO> Enrollees { get; set; }

        public ObservableCollection<Certificate> Certificates { get; set; }

        public bool IsChanged { get; set; }

        #region GoToPlaceOfResidencePageCommand

        public ICommand GoToSpecialityPageCommand { get; set; }

        public bool CanDisabilityPageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnDisabilityPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.SpecialityPage);
        }

        #endregion

        #region GoToRegistrationEnrollePageCommand

        public ICommand GoToEducationPageCommand { get; set; }

        public bool CanGoToEducationPageExecute(object parameter)
        {
            return true;
        }

        public void OnGoToEducationPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.EducationPage);
        }

        #endregion

        #region SaveCommand

        public ICommand SaveCommand { get; set; }

        public bool CanSaveCommandExecute(object parameter)
        {
            return IsAllValid();
        }

        public void OnSaveCommandExecuted(object parameter)
        {
            DbSet<Enrollee> dbEnrollees = DataBaseConnection.ApplicationContext.Enrollee;
            Enrollees.ForEach(enrollee =>
            {
                dbEnrollees.Find(enrollee.Id).Certificate = enrollee.Certificate;
            });

            DbSet<Certificate> dbCertificates = DataBaseConnection.ApplicationContext.Certificate;
            Certificates.ToList().ForEach(certificate =>
            {
                if (!dbCertificates.Contains(certificate))
                {
                    dbCertificates.Add(certificate);
                }
            });
            DataBaseConnection.ApplicationContext.SaveChanges();
            
        }

        #endregion

        #region LoadImageCommand

        public ICommand LoadImageCommand { get; set; }

        private bool CanLoadImageCommandExecute(object parameter)
        {
            return true;
        }

        private void OnLoadImageCommandExecuted(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG файлы (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            if (!string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                (parameter as EnrolleeDTO).Certificate.Photo = File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        #endregion

        #region RemoveImageCommand

        public ICommand RemoveImageCommand { get; set; }

        private bool CanRemoveImageCommandExecute(object parameter)
        {
            return true;
        }

        private void OnRemoveImageCommandExecuted(object parameter)
        {
            byte[] image = (parameter as EnrolleeDTO).Certificate.Photo;
            if (image != null)
            {
                (parameter as EnrolleeDTO).Certificate.Photo = null;
                MessageBox.Show("Успешно удалено!", "Успех!");
            }
            else
            {
                MessageBox.Show("Изображеия нет", "Ошибка!");
            }
        }

        #endregion

        private bool IsAllValid()
        {
            return Enrollees.All(enrollee => enrollee.Certificate?.AvarageScore > 0);
        }

        private void LoadData()
        {
            Enrollees = DataBaseConnection.ApplicationContext.Enrollee.ToList().ConvertAll(enrollee =>
            {
                return new EnrolleeDTO(enrollee.Id, enrollee.Name, enrollee.Surname,
                                       enrollee.Patronymic, enrollee.Gender, enrollee.DateOfBirth,
                                       enrollee.Snils, enrollee.YearOfAdmission,
                                       certificate: Certificates.ToList().Find(certificate => certificate.Id == enrollee.CertificateId));
            });

            Enrollees.ForEach(enrollee =>
            {
                if(enrollee.Certificate == null)
                {
                    enrollee.Certificate = new Certificate();
                    Certificates.Add(enrollee.Certificate);
                }
            });
        }
    }
}
