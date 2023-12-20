using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for ProductUpdate.xaml
    /// </summary>
    public partial class ProductUpdate : Page
    {   
        ProductUpdateViewModel puvm = new ProductUpdateViewModel();
        public ProductUpdate()
        {
            InitializeComponent();
            DataContext = puvm;
        }
      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());
        }
    }
}
