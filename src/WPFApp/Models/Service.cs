using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public class Service : SalesItem
    {
        private Service(int id, string type, string name, string description, decimal price) //privat constructor til brug for de 2
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
        public static Service CreateServiceFromUI(string type, string name, string description, decimal price)
        {
            Service service = new Service(0, type, name, description, price);
            return service;
        }
        /// <summary>
        /// Create-metode ud fra Db, dvs med et id
        /// </summary>    
        public static Service CreateServiceFromDb(int id, string type, string name, string description, decimal price)
        {
            Service service = new Service(id, type, name, description, price);
            return service;
        }
    }
}
