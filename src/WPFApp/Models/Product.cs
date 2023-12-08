using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public class Product : SalesItem
    {
        private Product(int id, string type, string name, string description, decimal price) //privat constructor til brug for de 2
                                                                                                                        // create-metoder
        {
            Id = id;
            Type = type;
            Name = name;
            Description = description;
            Price = price;
        }

        public int Id { get; }
        public string Type { get; }
        public string Name { get; }       
        public string Description { get; }
        public decimal Price { get; }

        /// <summary>
        /// Create-metode ud fra UI-input, dvs uden et id
        /// </summary>        
        public static Product CreateProductFromUI(string type, string name, string description, decimal price)
        {
            Product product = new Product(0, type, name, description, price);
            return product;
        }
        /// <summary>
        /// Create-metode ud fra Db, dvs med et id
        /// </summary>    
        public static SalesItem CreateProductFromDb(int id, string type, string name, string description, decimal price)
        {
            Product product = new Product(id, type, name, description, price);
            return product;
        }


    }
}
