using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    public class ProductDeleteCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter is ProductDeleteViewModel pdvm) 
            {
                ProductRepository productRepo = new ProductRepository();
                productRepo.DeleteProductById(pdvm.SelectedProduct.Id );
                MessageBox.Show("Produkt slettet");
                pdvm.SelectedProduct.Id = 0;
                pdvm.SelectedProduct.Type = null;
                pdvm.SelectedProduct.Name = null;
                pdvm.SelectedProduct.Description = null;
                pdvm.SelectedProduct.Price = 0;
            }
        }
    }
}
