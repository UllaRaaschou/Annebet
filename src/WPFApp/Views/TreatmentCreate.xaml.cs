using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for TreatmentCreate.xaml
    /// </summary>
    public partial class TreatmentCreate : Page
    {
        TreatmentCreateViewModel tcvm = new TreatmentCreateViewModel();
        public TreatmentCreate()
        {
            InitializeComponent();
            DataContext = tcvm;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());
        }
    }
}
