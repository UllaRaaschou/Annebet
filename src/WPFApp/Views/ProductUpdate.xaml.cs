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

        private void Tb_Navn_Copy2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());
        }
    }
}