using WPFApp.Models;

namespace WPFApp.ViewModels
{
    public class ProductToViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }


        public ProductToViewModel() { }// parameterløs constructor
       
        public ProductToViewModel(Product product)  // constructor, der tager Product som parameter
        {
            Id = product.Id;
            Type = product.Type;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;

        }


        /// <summary>
        /// Metode, der converter et object af typen Product til et object af typen ProductToViewModel
        /// </summary>
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
    }
}
