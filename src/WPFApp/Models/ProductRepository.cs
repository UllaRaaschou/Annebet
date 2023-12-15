using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WPFApp.Models
{
    public class ProductRepository : SalesItemRepository, IProductRepository  // Nedarver fra SalesItemRepository og implementerer interfacet IProductRepository
    {
        public int AddProduct(Product product)
        {
            return base.AddSalesItem(product, EnumCategory.Product);  // Kalder den abstrakte add-metode i parent-class med et products værdier
        }

       public List<Product> GetAllProducts(string type, string name)
        {
            List<Product> wantedProducts = new List<Product>(); // instantiering af tom liste af produkter

            using (SqlDataReader reader = base.GetAllSalesItems(EnumCategory.Product, type, name)) // Kalder den abstrakte getAll-metode i parent-class med et produkts værdier.
            // Her tjekker vi ikke for, om reader != 0, da den abstrakte metode altid returnerer et readerObjekt.                                                                                     // Metoden returnerer et reader-object, som derefter skal læses
            {
                while (reader.Read())  // Hvis reader læser
                {
                    int id = (int)reader.GetInt32(0);
                    string description = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);

                    Product product = Product.CreateProductFromDb(id, type, name, description, price); // et product skabes ud fra de læste værdier
                    wantedProducts.Add(product); // produktet lægges i den instantierede liste
                }

                return wantedProducts; // listen returneres
            }  // using lukker readeren
           
        }

        public void UpdateProduct(Product productWithUpdatedValues)
        {
            base.UpdateSalesItem(productWithUpdatedValues, EnumCategory.Product); // kalder Update-metoden i parent-class med et products værdier
        }

        public void DeleteProductById(int id)
        {
           base.DeleteSalesItemById(id); // kalder Delete-metoden i parent-class med et products værdier
        }
    }
}
