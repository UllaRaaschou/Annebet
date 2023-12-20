using System;
using System.Collections.Generic;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    public class ProductSearchCommand : ICommand // Nedarvning fra interfacet ICommand
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
    
        
        
        public ProductSearchCommand()  // Constructor, der som default vil blive aktiveret og som sætter repo-feltet til det almindelige CustomerReposity
        {
            this.repository = new ProductRepository();  
        }

        public ProductSearchCommand(IProductRepository repository)    // Constuctor, der kan bruges, når vi i unit-test bruger Test-repo som parameter,
        {                                                             // og vi dermed sætter repo-feltet til test-Repo
            this.repository = repository;
        }


        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til puvm eller pdvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            bool result = false;  // result sættes til false fra start

            if (parameter is ProductUpdateViewModel puvm) // Hvis datacontekst er puvm
            {
                if (puvm.Type != null && puvm.Name != null) // kontrol for, om tekstbokse til søgning er udfyldt
                {
                    result = true;
                }
                return result; // true result returneres
            }

            if (parameter is ProductDeleteViewModel pdvm) // Hvis datecontext er pdvm
            {
                if (pdvm.Type != null && pdvm.Name != null) // kontrol for, om tekstbokse til søgning er udfyldt
                {
                    result = true;
                }
                return result; // true result returneres
            }
            return false;
        }


        /// <summary>
        /// Metoden, der udfører søg_produkt_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til puvm eller pdvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            if(parameter is ProductUpdateViewModel puvm) // tjek af parameter og datacontext
            {
                puvm.ProductsToView.Clear(); //  Observable collection tømmes for eventuelle items
                ProductToViewModel ptvm = new ProductToViewModel(); // Procuct-to-Viewodel-Converter instantieres
                List <Product> trueProducts = repository.GetAllProducts(puvm.Type, puvm.Name); // Dens repo henter ønskede produkter
                foreach (Product p in trueProducts) 
                {
                    ProductToViewModel productViewModel = ptvm.ProductToViewModelConvert(p); // de hentede producter omdannes til ViewModels
                    puvm.ProductsToView.Add(productViewModel); // ViewModels lægger i Observable Collection i puvm
                }
            }
            else if (parameter is ProductDeleteViewModel pdvm)
            {
                pdvm.ProductsToView.Clear(); //  Observable collection tømmes for eventuelle items
                ProductToViewModel ptvm = new ProductToViewModel(); // Procuct-to-Viewodel-Converter instantieres
                List<Product> trueProducts = repository.GetAllProducts(pdvm.Type, pdvm.Name); // Dens repo henter ønskede produkter
                foreach (Product p in trueProducts)
                {
                    ProductToViewModel productViewModel = ptvm.ProductToViewModelConvert(p); // de hentede producter omdannes til ViewModels
                    pdvm.ProductsToView.Add(productViewModel); // ViewModels lægger i Observable Collection i pdvm
                }
            }
            else throw new Exception("Wrong type of parameter");
        }
    }
}
