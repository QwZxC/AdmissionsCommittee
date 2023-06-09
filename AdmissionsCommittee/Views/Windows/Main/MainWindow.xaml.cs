using System.Windows;
using System.Windows.Navigation;

namespace AdmissionsCommittee.Views.Windows.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            e.Cancel = e.NavigationMode != NavigationMode.New;
        }
    }
}
