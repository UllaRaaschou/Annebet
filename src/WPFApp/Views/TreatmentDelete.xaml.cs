using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for TreatmentDelete.xaml
    /// </summary>
    public partial class TreatmentDelete : Page
    {
        TreatmentDeleteViewModel tdvm = new TreatmentDeleteViewModel();
        public TreatmentDelete()
        {
            InitializeComponent();
            DataContext = tdvm;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());
        }
    }
}
