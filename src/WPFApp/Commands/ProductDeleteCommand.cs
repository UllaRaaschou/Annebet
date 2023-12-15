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
    // Nedarvning fra interfacet ICommand
    public class ProductDeleteCommand : ICommand
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


        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til pdvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            // Variablen sættes i første omgang til false
            bool result = false;

            // Tjek af, om datakontekst er den rette
            if (parameter is ProductDeleteViewModel pdvm) 
            {
                // Tjek af, at der er valgt en customer i listbox
                if (pdvm.SelectedProduct != null)
                {
                    // Tjek af, at alle selected Customers properties er udfyldt
                    if (pdvm.SelectedProduct.Type != null && pdvm.SelectedProduct.Name != null &&
                        pdvm.SelectedProduct.Description != null && pdvm.SelectedProduct.Price != null)
                    {
                        // Hvis ok, sættes variabel til  true
                        result = true; 
                    }
                }
                //Variabel returneres
                return result; 
            }
            // Hvis parameter test fejler, returenes false
            return false;
        }


        /// <summary>
        /// Metoden, der udfører slet_produkt_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til pdvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            // Tjek af parameter
            if (parameter is ProductDeleteViewModel pdvm)
            {
                // Instantiering af ProductRepo
                ProductRepository productRepo = new ProductRepository();

                // Repo deleter selected Product fra db
                productRepo.DeleteProductById(pdvm.SelectedProduct.Id);
                MessageBox.Show("Produkt slettet");
            }
            else throw new Exception("Wrong type of parameter");
        }
    }
}
