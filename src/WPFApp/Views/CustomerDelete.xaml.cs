using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for CustomerDelete.xaml
    /// </summary>
    public partial class CustomerDelete : Page
    {
        CustomerDeleteViewModel cdvm = new CustomerDeleteViewModel();
        public CustomerDelete()
        {
            InitializeComponent();
            DataContext = cdvm;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());
        }
    }
}
