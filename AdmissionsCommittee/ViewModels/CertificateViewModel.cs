using AdmissionsCommittee.Commands;
using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AdmissionsCommittee.ViewModels
{
    public class CertificateViewModel
    {
        public CertificateViewModel()
        {
            Enrollees = DataBaseConnection.ApplicationContext.Enrollee.ToList();
            Certificates = new ObservableCollection<Certificate>();
            GoToDisabilityPageCommand = new LambdaCommand(OnDisabilityPageCommandExecuted, CanDisabilityPageCommandExecute);
            GoToEducationPageCommand = new LambdaCommand(OnGoToEducationPageCommandExecuted, CanGoToEducationPageExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            LoadData();
        }

        public List<Enrollee> Enrollees { get; set; }

        public ObservableCollection<Certificate> Certificates { get; set; }

        #region GoToPlaceOfResidencePageCommand

        public ICommand GoToDisabilityPageCommand { get; set; }

        public bool CanDisabilityPageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnDisabilityPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.DisabilityPage);
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
    
        public void LoadData()
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
