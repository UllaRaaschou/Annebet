using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public class Product : SalesItem  //Nedarvning fra SalesItem
    {
        private Product(int id, string type, string name, string description, decimal price) : 
            base(id, type, name, description, price) { }//privat constructor til brug for de 2 create-metoder                                                                                                                                      // create-metoder
        

        /// <summary>
        /// Create-metode ud fra UI-input, dvs uden et id.
        /// Metoden er statisk, så den kan kaldes uden en instans af klassen
        /// </summary>        
        public static Product CreateProductFromUI(string type, string name, string description, decimal price)
        {
            Product product = new Product(0, type, name, description, price);
            return product;
        }



        /// <summary>
        /// Create-metode ud fra Db, dvs med et id
        /// Metoden er statisk, så den kan kaldes uden en instans af klassen
        /// </summary>    
        public static Product CreateProductFromDb(int id, string type, string name, string description, decimal price)
        {
            Product product = new Product(id, type, name, description, price);
            return product;
        }


    }
}
