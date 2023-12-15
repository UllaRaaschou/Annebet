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

        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til puvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            // Variablen sættes i første omgang til false
            bool result = false;

            // Parameter tjekkes
            if (parameter is ProductUpdateViewModel puvm)
            {
                // Tjek af, at der er valgt et selected item
                if (puvm.SelectedProduct != null)
                {
                    // Det tjekkes, om alle nødvendige tekstbokse er udfyldt
                    if (puvm.SelectedProduct.Type != null && puvm.SelectedProduct.Name != null
                        && puvm.SelectedProduct.Description != null && puvm.SelectedProduct.Price != null)
                    {
                        // Så sættes variblen til true;
                        result = true;
                    }
                }
                // Variablen returneres
                return result;
            }
            // Hvis datacontext ikke er sat korrekt, returneres false
            return false;
        }


        /// <summary>
        /// Metoden, der udfører opdater_produkt_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til puvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            // Parameter tjekkes
            if (parameter is ProductUpdateViewModel puvm) 
            {
                // Den statiske Product-metode laver en updated Product
                Product updatedProduct = Product.CreateProductFromDb(puvm.SelectedProduct.Id, puvm.SelectedProduct.Type, puvm.SelectedProduct.Name,
                    puvm.SelectedProduct.Description, puvm.SelectedProduct.Price);

                // ProductRepo instantieres
                ProductRepository productRepo = new ProductRepository();

                // Repo opdaterer Product 
                productRepo.UpdateProduct(updatedProduct);
                MessageBox.Show("Produkt er opdateret");
            }
            else throw new Exception("Error");
        }
    }
}
