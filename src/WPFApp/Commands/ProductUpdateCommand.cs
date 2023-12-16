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
    public class ProductUpdateCommand : ICommand
    {
        /// <summary>
        /// CanExecuteChanged-eventet har fået add'et et RequerySuggested-event. 
        /// Requery udløses så snart WPF mener, at command properties skal re-evalueres - ofte sfa bruger-acts.
        /// Dette trigger så CanExecuteChanged-eventet og dermed en re-evaluering af CanExecute-metoden, som så måske
        /// bliver true, hvorved knap bliver enabled og Execute-metoden kan udføres
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private IProductRepository repository;

        public ProductUpdateCommand()
        {
            this.repository = new ProductRepository();
        }

        public ProductUpdateCommand(IProductRepository repository)
        {
            this.repository = new ProductTESTRepository();
        }


        public bool CanExecute(object? parameter)
        {
            bool result = false;  // parameteren sættes i første omgang til false

            if (parameter is ProductUpdateViewModel tuvm) // tjek af parameter som datekontext
            {
                if (tuvm.SelectedProduct != null) // tjek af, at der er valgt et selected item
                {
                    if (tuvm.SelectedProduct.Type != null && tuvm.SelectedProduct.Name != null // tjek af, om selected items properties alle er sat
                        && tuvm.SelectedProduct.Description != null && tuvm.SelectedProduct.Price != null)
                    {
                        result = true; // hvis ja, sættes parameter til true
                    }
                }
                return result; // parameter returneres
            }
            return false; // hvis parametertjek fejler, returneres false
        }

        public void Execute(object? parameter)
        {
            if(parameter is ProductUpdateViewModel puvm) 
            {
                Product updatedProduct = Product.CreateProductFromDb(puvm.SelectedProduct.Id, puvm.SelectedProduct.Type, puvm.SelectedProduct.Name,
                    puvm.SelectedProduct.Description, puvm.SelectedProduct.Price);
                
                repository.UpdateProduct(updatedProduct);
                if(repository is ProductRepository) 
                {
                    MessageBox.Show("Produkt er opdateret");
                }
                    
            }
            else throw new Exception("Wrong type of parameter");
        }
    }
}
