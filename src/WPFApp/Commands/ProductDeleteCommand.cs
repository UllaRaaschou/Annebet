using System;
using System.Windows;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    public class ProductDeleteCommand : ICommand // Nedarvning fra interfacet ICommand
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



        private IProductRepository repository; // simpel deklarering af repo. Dette kan skiftes afhængigt af den anvendte konstructor
     
        
        
        public ProductDeleteCommand()  // Constructor, der som default vil blive aktiveret og som sætter repo-feltet til det almindelige CustomerReposity
        {
            this.repository = new ProductRepository();
        }
     
        public ProductDeleteCommand(IProductRepository repository)  // Constuctor, der kan bruges, når vi i unit-test bruger Test-repo som parameter,
                                                                    // og vi dermed sætter repo-feltet til test-Repo
        {
            this.repository = repository;
        }



        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til pdvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            bool result = false; // variablen sættes i første omgang til false

            if (parameter is ProductDeleteViewModel pdvm) // tjek af, om datakontekst er den rette
            {
                if (pdvm.SelectedProduct != null) // tjek af, at der er valgt en customer i listbox
                {
                    if (pdvm.SelectedProduct.Type != null && pdvm.SelectedProduct.Name != null && // tjek af, at alle selected Customers properties er udfyldt
                        pdvm.SelectedProduct.Description != null && pdvm.SelectedProduct.Price != null)
                    {
                        result = true; // hvis ok, sættes variabel til  true
                    }
                }
                return result; //variabel returneres
            }
            return false; // hvis parameter test fejler, returenes false
        }



        /// <summary>
        /// Metoden, der udfører slet_produkt_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til pdvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            if (ConfirmProductDeletion() == MessageBoxResult.Yes)
            {
                if (parameter is ProductDeleteViewModel pdvm) // tjek af parameter
                {
                    repository.DeleteProductById(pdvm.SelectedProduct.Id); // Repo deleter selected Product fra db
                    if (repository is ProductRepository)
                    {
                        MessageBox.Show("Produkt slettet");
                    }
                    pdvm.SelectedProduct = null;// Tekstbokse nulstilles
                    pdvm.Type = null;
                    pdvm.Name = null;

                    pdvm.ProductsToView.Clear();
                }
                else throw new Exception("Wrong type of parameter");
            }
        }

        /// <summary>
        /// Metode, der returnerer yes, når kunden har bekræftet sletning.
        /// I unittests returneres altid yes
        /// </summary
        private MessageBoxResult ConfirmProductDeletion()
        {
            if(repository is ProductRepository)
            {
                return MessageBox.Show("Er du sikker?", "Bekræft", MessageBoxButton.YesNo, MessageBoxImage.Question);
            }

            return MessageBoxResult.Yes;
        }
    }
}
