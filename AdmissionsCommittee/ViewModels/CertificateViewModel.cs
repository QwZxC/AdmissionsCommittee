using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
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
            Enrollees = DataBaseConnection.ApplicationContext.Enrollee.ToList();
            Certificates = new ObservableCollection<Certificate>();
            GoToSpecialityPageCommand = new LambdaCommand(OnDisabilityPageCommandExecuted, CanDisabilityPageCommandExecute);
            GoToEducationPageCommand = new LambdaCommand(OnGoToEducationPageCommandExecuted, CanGoToEducationPageExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            LoadImageCommand = new LambdaCommand(OnLoadImageCommandExecuted, CanLoadImageCommandExecute);
            RemoveImageCommand = new LambdaCommand(OnRemoveImageCommandExecuted, CanRemoveImageCommandExecute);
            LoadData();
        }

        public List<Enrollee> Enrollees { get; set; }

        public ObservableCollection<Certificate> Certificates { get; set; }

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
            return Certificates.Any();
        }

        public void OnSaveCommandExecuted(object parameter)
        {
            DbSet<Certificate> dbCertificates = DataBaseConnection.ApplicationContext.Certificate;
            Certificates.ToList().ForEach(enrollees =>
            {
                if (!dbCertificates.Contains(enrollees))
                {
                    dbCertificates.Add(enrollees);
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
            openFileDialog.ShowDialog();
            if (!string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                (parameter as Enrollee).Certificate.Photo = File.ReadAllBytes(openFileDialog.FileName);
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
            byte[] image = (parameter as Enrollee).Certificate.Photo;
            if (image != null)
            {
                (parameter as Enrollee).Certificate.Photo = null;
                MessageBox.Show("Успешно удалено!", "Успех!");
            }
            else
            {
                MessageBox.Show("Изображеия нет", "Ошибка!");
            }
        }

        #endregion

        private void LoadData()
        {
            Enrollees.ForEach(enrollee =>
            {
                enrollee.Certificate = DataBaseConnection.ApplicationContext.Certificate.ToList().Find(dbСertificate => dbСertificate.Id == enrollee.CertificateId);
                if (enrollee.Certificate == null)
                {
                    enrollee.Certificate = new Certificate();
                    Certificates.Add(enrollee.Certificate);
                    return;
                }
                Certificates.Add(enrollee.Certificate);
            });
        }    
    }
}
