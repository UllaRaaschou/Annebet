using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public abstract class SalesItem // Abstrakt parent-class med Product og Treatment som children
    {             

        public int Id{ get;  }
        public string Type { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }

        protected SalesItem(int id, string type, string name, string description, decimal price)  // protected konstructør, der bruges i de to create-metoder
        {
            Id = id;
            Type = type;
            Name = name;
            Price = price;
            Description = description;
        }

        public static SalesItem CreateSalesItemFromDb(int id, EnumCategory category, string type, string name, string description, decimal price) // salesItemværdier hentet fra db
        {
            if(category == EnumCategory.Product) // Hvis category er et product, anvendes et product's create-metode
            {
                Product product = Product.CreateProductFromDb(id, type, name, description, price);
                return product;
            }
            else if (category == EnumCategory.Treatment) // hvis category er en treatment, anvendes et treatment's create-metode
            {
                Treatment treatment = Treatment.CreateTreatmentFromDb(id, type, name, description, price);
                return treatment;
            }
            else 
            {
                throw new Exception("Fejl i category");
            }

        }

        public static SalesItem CreateSalesItemFromUI(EnumCategory category, string type, string name, string description, decimal price) // salesItemværdier fra UI
        {
            if (category == EnumCategory.Product) // Hvis category er et product, anvendes et product's create-metode
            {
                Product product = Product.CreateProductFromDb(0, type, name, description, price);
                return product;
            }
            else if (category == EnumCategory.Treatment) // hvis category er en treatment, anvendes et treatment's create-metode
            {
                Treatment treatment = Treatment.CreateTreatmentFromDb(0, type, name, description, price);
                return treatment;
            }
            else
            {
                throw new Exception("Fejl i category");
            }

        }
    }
}
