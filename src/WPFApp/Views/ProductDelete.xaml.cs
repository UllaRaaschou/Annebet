﻿using System;
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

        }
    }
}