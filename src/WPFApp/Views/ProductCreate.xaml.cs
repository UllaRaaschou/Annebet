using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for ProductCreate.xaml
    /// </summary>
    public partial class ProductCreate : Page
    {
        ProductCreateViewModel pvm = new ProductCreateViewModel();
        public ProductCreate()
        {
            InitializeComponent();
            DataContext = pvm;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());    
        }
    }
}
