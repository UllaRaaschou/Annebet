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
    /// Interaction logic for POSCreate.xaml
    /// </summary>
    public partial class POSCreate : Page
    {
        POSCreateViewModel POScvm = new POSCreateViewModel();
        public POSCreate()
        {
            InitializeComponent();
            DataContext = POScvm;
        }

        private void Tb_Email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
