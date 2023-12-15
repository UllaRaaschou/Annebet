using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    // Nedarvning fra interfacet ICommand
    public class ProductSearchCommand : ICommand
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
                // Det tjekkes, om alle nødvendige tekstbokse er udfyldt
                if (puvm.Type != null && puvm.Name != null) // kontrol for, om tekstbokse til søgning er udfyldt
                {
                    // Så sættes variblen til true;
                    result = true;
                }
                // Variablen returneres
                return result; 
            }

            if (parameter is ProductDeleteViewModel pdvm) // Hvis datecontext er pdvm
            {
                // Parameter tjekkes
                if (pdvm.Type != null && pdvm.Name != null) 
                {
                    // Så sættes variblen til true;
                    result = true;
                }
                // Variablen returneres
                return result;
            }
            return false;
        }


        /// <summary>
        /// Metoden, der udfører opdater_kunde_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til cuvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            // Parameter tjekkes
            if (parameter is ProductUpdateViewModel puvm)
            {
                // Procuct-to-Viewodel-Converter instantieres
                ProductToViewModel ptvm = new ProductToViewModel();

                // Dens repo henter ønskede produkter
                List<Product> trueProducts = ptvm.productRepo.GetAllProducts(puvm.Type, puvm.Name);
               
                foreach (Product p in trueProducts) 
                {
                    // De hentede Products converteres til ViewModels
                    ProductToViewModel productViewModel = ptvm.ProductToViewModelConvert(p);

                    // ViewModels lægger i Observable Collection i puvm
                    puvm.ProductsToView.Add(productViewModel); 
                }
            }
            // Parameter tjekkes
            else if (parameter is ProductDeleteViewModel pdvm)
            {
                // Procuct-to-Viewodel-Converter instantieres
                ProductToViewModel ptvm = new ProductToViewModel();

                // Dens repo henter ønskede produkter
                List<Product> trueProducts = ptvm.productRepo.GetAllProducts(pdvm.Type, pdvm.Name);
                
                foreach (Product p in trueProducts)
                {
                    // De hentede producter omdannes til ViewModels
                    ProductToViewModel productViewModel = ptvm.ProductToViewModelConvert(p);

                    // ViewModels lægger i Observable Collection i pdvm
                    pdvm.ProductsToView.Add(productViewModel); 
                }
            }
            else throw new Exception("Wrong type of paratemer");
        }
    }
}
