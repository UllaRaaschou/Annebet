using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WPFApp.Models
{
    public class ProductRepository : SalesItemRepository, IProductRepository
    {
        public int AddProduct(Product product)
        {
            return base.AddSalesItem(product, EnumCategory.Product);
        }

       public List<Product> GetAllProducts(string type, string name)
        {
            List<Product> wantedProducts = new List<Product>();

            using (SqlDataReader reader = base.GetAllSalesItems(EnumCategory.Product, type, name))
            {
                // Her tjekker vi ikke for, om reader != 0, da den abstrakte metode altid returnerer et readerObjekt.
                while (reader.Read())
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

        public void UpdateProduct(Product productWithUpdatedValues)
        {
            base.UpdateSalesItem(productWithUpdatedValues, EnumCategory.Product);
        }

        public void DeleteProductById(int id)
        {
           base.DeleteSalesItemById(id);
        }
    }
}
