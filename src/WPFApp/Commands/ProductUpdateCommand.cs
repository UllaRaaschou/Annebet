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
    public class ProductUpdateCommand : ICommand // Nedarvning fra interfacet ICommand
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

        private IProductRepository repository;  // simpel deklarering af repo. Dette kan skiftes afhængigt af den anvendte konstructor

        public ProductUpdateCommand()  // Constructor, der som default vil blive aktiveret og som sætter repo-feltet til det almindelige CustomerReposity
        {
            this.repository = new ProductRepository();
        }

        public ProductUpdateCommand(IProductRepository repository)   // Constuctor, der kan bruges, når vi i unit-test bruger Test-repo som parameter,
        {                                                            // og vi dermed sætter repo-feltet til test-Repo
            this.repository = repository;   
        }


        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til puvm.
        /// </summary>
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


        /// <summary>
        /// Metoden, der udfører opdater_produkt_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til puvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            if(parameter is ProductUpdateViewModel puvm) 
            {
                Product updatedProduct = Product.CreateProductFromDb(puvm.SelectedProduct.Id, puvm.SelectedProduct.Type, puvm.SelectedProduct.Name,
                    puvm.SelectedProduct.Description, puvm.SelectedProduct.Price);

                puvm.SelectedProduct = null; // Tekstboksenes indhold nulstilles
                puvm.Type = null;
                puvm.Name = null;

                puvm.ProductsToView.Clear();

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
