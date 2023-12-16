using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public class ProductTESTRepository : IProductRepository
    {
        public List<Product> products = new List<Product>();
        public int AddProduct(Product product)
        {
            int idValue = (products.Count + 1);
            Product productWithId = Product.CreateProductFromDb(idValue, product.Type, product.Name, product.Description,
               product.Price);
            products.Add(productWithId);
            return idValue;
        }

        public List<Product> GetAllProducts(string type, string name)
        {
            List<Product> wantedProducts = new List<Product>();
            
            foreach (Product product in products)
            {
                if (product.Type == type && product.Name == name) 
                {
                    wantedProducts.Add(product);
                }
                
            }
            return wantedProducts;
        }

        public void UpdateProduct(Product productWithUpdatedValues)
        {
            foreach (Product product in products) 
            {
                if(product.Id == productWithUpdatedValues.Id) 
                {
                    products.Remove(product);
                    products.Add(Product.CreateProductFromDb(productWithUpdatedValues.Id, productWithUpdatedValues.Type,
                        productWithUpdatedValues.Name, productWithUpdatedValues.Description, productWithUpdatedValues.Price));

                }
            }
        }

        public void DeleteProductById(int id)
        {
            Product product2 = null;
            foreach (Product product in products)
            {
                if (product.Id == id)
                {
                    product2 = product;
                    break;
                }
            }
            if (product2 != null)
            {
                products.Remove(product2);
            }
        }
    }
}
