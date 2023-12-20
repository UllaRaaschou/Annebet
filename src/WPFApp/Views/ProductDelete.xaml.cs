using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for ProductDelete.xaml
    /// </summary>
    public partial class ProductDelete : Page
    {
        ProductDeleteViewModel pdvm = new ProductDeleteViewModel();
        public ProductDelete()
        {
            InitializeComponent();
            DataContext = pdvm;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());
        }
    }
}
