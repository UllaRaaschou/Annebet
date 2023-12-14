using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class StartPage : Page
    {

        public StartPage()
        {
            InitializeComponent();

        }

        private void Bt_OpretKunde_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomerCreate());
        }

        private void Bt_OpdaterKunde_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomerUpdate());
        }

        private void Bt_SletKunde_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomerDelete());
        }

        private void Bt_OpretProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductCreate());
        }

        private void Bt_OpdaterProdukt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductUpdate());
        }


        private void Bt_SletProdukt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductDelete());
        }

        private void Bt_OpretBehandling_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TreatmentCreate());
        }

        private void Bt_OpdaterBehandling_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TreatmentUpdate());
        }

        private void Bt_SletBehandling_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TreatmentDelete());
        }
    }
}

