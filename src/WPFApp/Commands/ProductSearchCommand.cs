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

        public void Execute(object? parameter)
        {
            if(parameter is ProductUpdateViewModel puvm) // tjek af parameter og datacontext
            {
                ProductToViewModel ptvm = new ProductToViewModel(); // Procuct-to-Viewodel-Converter instantieres
                List <Product> trueProducts = ptvm.productRepo.GetAllProducts(puvm.Type, puvm.Name); // Dens repo henter ønskede produkter
                foreach (Product p in trueProducts) 
                {
                    ProductToViewModel productViewModel = ptvm.ProductToViewModelConvert(p); // de hentede producter omdannes til ViewModels
                    puvm.ProductsToView.Add(productViewModel); // ViewModels lægger i Observable Collection i puvm
                }
            }
            else if (parameter is ProductDeleteViewModel pdvm)
            {
                ProductToViewModel ptvm = new ProductToViewModel(); // Procuct-to-Viewodel-Converter instantieres
                List<Product> trueProducts = ptvm.productRepo.GetAllProducts(pdvm.Type, pdvm.Name); // Dens repo henter ønskede produkter
                foreach (Product p in trueProducts)
                {
                    ProductToViewModel productViewModel = ptvm.ProductToViewModelConvert(p); // de hentede producter omdannes til ViewModels
                    pdvm.ProductsToView.Add(productViewModel); // ViewModels lægger i Observable Collection i pdvm
                }
            }
            else { throw new NotImplementedException(); }
        }
    }
}
