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
    public class DisabilityViewModel
    {

        public DisabilityViewModel()
        {
            Enrollees = DataBaseConnection.ApplicationContext.Enrollee.ToList();
            Disabilities = new ObservableCollection<Disability>();
            GoToWardPageCommand = new LambdaCommand(OnGoToWardPageCommandExecuted, CanGoToWardPageCommandExecute);
            GoToAvarageScoreSnilsPageCommand = new LambdaCommand(OnGoToAvarageScoreSnilsPageCommandExecuted, CanGoToAvarageScoreSnilsPageExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            LoadImageCommand = new LambdaCommand(OnLoadImageCommandExecuted, CanLoadImageCommandExecute);
            RemoveImageCommand = new LambdaCommand(OnRemoveImageCommandExecuted, CanRemoveImageCommandExecute);
            LoadData();
        }

        public List<Enrollee> Enrollees { get; set; }

        public ObservableCollection<Disability> Disabilities { get; set; }


        #region GoToWardPageCommand

        public ICommand GoToWardPageCommand { get; set; }

        public bool CanGoToWardPageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToWardPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.WardPage);
        }

        #endregion

        #region GoToAvarageScoreSnilsPageCommand

        public ICommand GoToAvarageScoreSnilsPageCommand { get; set; }

        public bool CanGoToAvarageScoreSnilsPageExecute(object parameter)
        {
            return true;
        }

        public void OnGoToAvarageScoreSnilsPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.CertificatePage);
        }

        #endregion

        #region SaveCommand

        public ICommand SaveCommand { get; set; }

        public bool CanSaveCommandExecute(object parameter)
        {
            return Disabilities.Any();
        }

        public void OnSaveCommandExecuted(object parameter)
        {
            DbSet<Disability> dbDisabilities = DataBaseConnection.ApplicationContext.Disability;
            Disabilities.ToList().ForEach(disability =>
            {
                if (!dbDisabilities.Contains(disability))
                {
                    dbDisabilities.Add(disability);
                }
            });
            DataBaseConnection.ApplicationContext.SaveChanges();
        }

        #endregion

        #region LoadImageCommand
        
        public ICommand LoadImageCommand { get; set;}

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
                (parameter as Enrollee).Disability.Document = File.ReadAllBytes(openFileDialog.FileName);
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
            byte[] image = (parameter as Enrollee).Disability.Document;
            if (image != null)
            {
                image = null;
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
                enrollee.Disability = DataBaseConnection.ApplicationContext.Disability.ToList().Find(dbDisability => dbDisability.Id == enrollee.DisabilityId);
                if (enrollee.Disability == null)
                {
                    enrollee.Disability = new Disability();
                    Disabilities.Add(enrollee.Disability);
                    return;
                }
                Disabilities.Add(enrollee.Disability);
            });
        }
    }
}
