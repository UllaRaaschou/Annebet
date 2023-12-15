using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models;

namespace WPFApp.ViewModels
{
    public class ProductToViewModel
    {
        // productRepo deklareres
        public ProductRepository productRepo;


        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }


        // parameterløs constructor, der instantierer productRepo
        public ProductToViewModel()
        {
            productRepo = new ProductRepository();
        }

        // constructor, der tager Product som parameter
        public ProductToViewModel(Product product)  
        {
            Id = product.Id;
            Type = product.Type;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
        }


        /// Metode, der converter et object af typen Product til et object af typen ProductToViewModel
        public ProductToViewModel ProductToViewModelConvert(Product product) 
        {            
            int id = product.Id;
            string type = product.Type;
            string name = product.Name;
            string description = product.Description;
            decimal price = product.Price;
            Product createdProduct = Product.CreateProductFromDb(id, type, name, description, price); // product-klassens statiske metode creater product
            ProductToViewModel model = new ProductToViewModel(createdProduct); // product konverteres til ProductToViewModel
            return model; // ProductToViewModel returneres
        }

       
        /// Metode, der via repo henter ønskede products i db
        public List<Product> SearchForProducts(string type, string name) 
        {
            List<Product> products = productRepo.GetAllProducts(type, name);
            return products;
        }  
    }
}
