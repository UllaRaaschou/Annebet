using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public abstract class SalesItem
    {             

        public int Id{ get;  }
        public string Type { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }

        protected SalesItem(int id, string type, string name, string description, decimal price)
        {
            Id = id;
            Type = type;
            Name = name;
            Price = price;
            Description = description;
        }

        public static SalesItem CreateSalesItemFromDb(int id, EnumCategory category, string type, string name, string description, decimal price) 
        {
            if(category == EnumCategory.Product) 
            {
                Product product = Product.CreateProductFromDb(id, type, name, description, price);
                return product;
            }
            else if (category == EnumCategory.Treatment)
            {
                Treatment treatment = Treatment.CreateTreatmentFromDb(id, type, name, description, price);
                return treatment;
            }
            else 
            {
                throw new Exception("Fejl i category");
            }


        }

       
    }
}
