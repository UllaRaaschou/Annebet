using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WPFApp.Models
{
    public class ProductRepository : SalesItemRepository
    {
        public int AddProduct(Product product)
        {
          return base.AddSalesItem(product, EnumCategory.Product);
        }

        public void UpdateProduct(Product product) 
        {
            base.UpdateSalesItem(product, EnumCategory.Product);
        }
               
        public List<Product> GetAllProductsFromCategoryAndTypeAndName(string type, string name) 
        {
            List<Product> wantedProducts = new List<Product>();
            SqlDataReader reader =  base.GetAllSalesItemsFromCategoryAndTypeAndName(EnumCategory.Product, type, name);
            // Her tjekker vi ikke for, om reader != 0, da den abstrakte metode altid returnerer et readerObjekt.
            while(reader.Read()) 
            {
                int id = (int)reader.GetInt32(0);
                string description = reader.GetString(1);
                decimal price = reader.GetDecimal(2);

                Product product = Product.CreateProductFromDb(id, type, name, description, price);
                wantedProducts.Add(product);
            }               
           
            return wantedProducts;
        }
    }
}
