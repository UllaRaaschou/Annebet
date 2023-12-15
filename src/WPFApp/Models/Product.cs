using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    //Nedarvning fra SalesItem
    public class Product : SalesItem  
    {
        //Privat constructor til brug for de 2 create-metoder  
        private Product(int id, string type, string name, string description, decimal price)      
            : base(id, type, name, description, price) { }                                                                                                                       
        

        /// Create-metode ud fra UI-input, dvs uden et id.
        public static Product CreateProductFromUI(string type, string name, string description, decimal price)
        {
            Product product = new Product(0, type, name, description, price);
            return product;
        }


        /// Create-metode ud fra Db, dvs med et id
        public static Product CreateProductFromDb(int id, string type, string name, string description, decimal price)
        {
            Product product = new Product(id, type, name, description, price);
            return product;
        }
    }
}
