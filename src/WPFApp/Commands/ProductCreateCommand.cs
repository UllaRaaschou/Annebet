using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    public class ProductCreateCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;

        }

        public void Execute(object? parameter) 
        {
            if (parameter is ProductCreateViewModel pcvm) 
            {
                ProductRepository productRepo = new ProductRepository();
                productRepo.AddProduct(Product.CreateProductFromUI(pcvm.Type, pcvm.Name, pcvm.Description, pcvm.Price));
                pcvm.Type = null;
                pcvm.Name = null;
                pcvm.Description = null;
                pcvm.Price = 0;
            }
            else 
            {
                throw new NotImplementedException();
            }
            MessageBox.Show("Produkt oprettet");
        }

        
            


       
    }
}
