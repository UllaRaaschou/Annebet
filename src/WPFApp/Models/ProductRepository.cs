using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WPFApp.Models
{
    // Nedarver fra SalesItemRepository og implementerer interfacet IProductRepository
    public class ProductRepository : SalesItemRepository, IProductRepository
    {
        /// Kalder den abstrakte add-metode i parent-class med en product værdier
        public int AddProduct(Product product)
        {
            return base.AddSalesItem(product, EnumCategory.Product);
        }


       public List<Product> GetAllProducts(string type, string name)
        {
            // Instantiering af tom liste af produkter
            List<Product> wantedProducts = new List<Product>();

            // Kalder den abstrakte getAll-metode i parent-class med et produkts værdier.
            using (SqlDataReader reader = base.GetAllSalesItems(EnumCategory.Product, type, name))                                                                                           
            {
                // Tjekker om reader != 0, da den abstrakte metode altid returnerer et readerObjekt.
                // Metoden returnerer et reader-object, som derefter skal læses
                while (reader.Read()) 
                {
                    // Hvis reader læser
                    int id = (int)reader.GetInt32(0);
                    string description = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);

                    // Et product skabes ud fra de læste værdier
                    Product product = Product.CreateProductFromDb(id, type, name, description, price);
                    
                    // Produktet lægges i den instantierede liste
                    wantedProducts.Add(product);
                }
                // Listen returneres
                return wantedProducts;
            }         
        }


        /// Kalder Update-metoden i parent-class med et products værdier
        public void UpdateProduct(Product productWithUpdatedValues)
        {          
            base.UpdateSalesItem(productWithUpdatedValues, EnumCategory.Product);
        }


        /// Kalder Delete-metoden i parent-class med et products værdier
        public void DeleteProductById(int id)
        {
           base.DeleteSalesItemById(id); 
        }
    }
}
