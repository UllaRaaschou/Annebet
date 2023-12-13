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
    public class ProductSearchCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter is ProductUpdateViewModel puvm) 
            {
                ProductToViewModel ptvm = new ProductToViewModel();
                List <Product> trueProducts = ptvm.productRepo.GetAllProducts(puvm.Type, puvm.Name);
                foreach (Product p in trueProducts) 
                {
                    ProductToViewModel productViewModel = ptvm.ProductToViewModelConvert(p);
                    puvm.ProductsToView.Add(productViewModel);
                }
            }
            else if (parameter is ProductDeleteViewModel pdvm)
            {
                ProductToViewModel ptvm = new ProductToViewModel();
                List<Product> trueProducts = ptvm.productRepo.GetAllProducts(pdvm.Type, pdvm.Name);
                foreach (Product p in trueProducts)
                {
                    ProductToViewModel productViewModel = ptvm.ProductToViewModelConvert(p);
                    pdvm.ProductsToView.Add(productViewModel);
                }
            }
            else { throw new NotImplementedException(); }
        }
    }
}
