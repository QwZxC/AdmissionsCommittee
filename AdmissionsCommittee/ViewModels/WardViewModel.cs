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
    public class WardViewModel
    {
        public WardViewModel()
        {
            Enrollees = DataBaseConnection.ApplicationContext.Enrollee.ToList();
            Wards = new ObservableCollection<Ward>();
            GoToLoadToFilePageCommand = new LambdaCommand(OnGoToLoadToFilePageCommandExecuted, CanGoToLoadToFilePageCommandExecute);
            GoToDisabilityPageCommand = new LambdaCommand(OnGoToDisabilityPageCommandExecuted, CanGoToDisabilityPageCommandExecute);
            SaveCommand = new LambdaCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            LoadImageCommand = new LambdaCommand(OnLoadImageCommandExecuted, CanLoadImageCommandExecute);
            RemoveImageCommand = new LambdaCommand(OnRemoveImageCommandExecuted, CanRemoveImageCommandExecute);
            LoadData();
        }

        public List<Enrollee> Enrollees { get; set; }

        public ObservableCollection<Ward> Wards { get; set; }

        #region GoToLoadToFilePageCommand

        public ICommand GoToLoadToFilePageCommand { get; set; }

        public bool CanGoToLoadToFilePageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToLoadToFilePageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.LoadToFilePage);
        }

        #endregion

        #region GoToDisabilityPageCommand

        public ICommand GoToDisabilityPageCommand { get; set; }

        public bool CanGoToDisabilityPageCommandExecute(object parameter)
        {
            return true;
        }

        public void OnGoToDisabilityPageCommandExecuted(object parameter)
        {
            MainViewModel.SwitchPage(MainPageType.DisabilityPage);
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
            DbSet<Ward> dbWards = DataBaseConnection.ApplicationContext.Ward;
            Wards.ToList().ForEach(ward =>
            {
                if (!dbWards.Contains(ward))
                {
                    dbWards.Add(ward);
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
                (parameter as Enrollee).Ward.Document = File.ReadAllBytes(openFileDialog.FileName);
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
            byte[] image = (parameter as Enrollee).Ward.Document;
            if (image != null)
            {
                (parameter as Enrollee).Ward.Document = null;
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
            return Enrollees.All(enrollee => enrollee.Ward?.Document != null);
        }

        private void LoadData()
        {
            Enrollees.ForEach(enrollee =>
            {
                enrollee.Ward = DataBaseConnection.ApplicationContext.Ward.ToList().Find(dbDisability => dbDisability.Id == enrollee.WardId);
                if (enrollee.Ward == null)
                {
                    enrollee.Ward = new Ward();
                    Wards.Add(enrollee.Ward);
                    return;
                }
                Wards.Add(enrollee.Ward);
            });
        }
    }
}
