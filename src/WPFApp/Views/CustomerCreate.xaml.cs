using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class CustomerCreate  : Page
    {
        CustomerCreateViewModel ccvm = new CustomerCreateViewModel();
        public CustomerCreate()
        {
            InitializeComponent();
            DataContext = ccvm;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());
        }
    }
}
