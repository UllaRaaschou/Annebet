using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for TreatmentUpdate.xaml
    /// </summary>
    public partial class TreatmentUpdate : Page
    {
        TreatmentUpdateViewModel tuvm = new TreatmentUpdateViewModel();
        public TreatmentUpdate()
        {
            InitializeComponent();
            DataContext = tuvm;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());
        }
    }
}
