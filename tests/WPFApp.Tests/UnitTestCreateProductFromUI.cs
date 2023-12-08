using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Tests
{
    [TestClass]
    public class UnitTestCreateProductFromUI    {
        [TestMethod]
        public void TestCreateProductFromUI() 
        {
            // Arrange           
            string type = "Scinbetter Science";
            string name = "AlphaRet® Clearing Serum";
            string description = "Denne serum giver den perfekte balance mellem hudforyngelse og klarhed. ";
            decimal price = 1315.00m;

            // Act
            Product product  = Product.CreateProductFromUI(type, name, description, price);

            // Assert
            Assert.AreEqual("Scinbetter Science", product.Type);
            Assert.AreEqual("AlphaRet® Clearing Serum", product.Name);
            Assert.AreEqual("Denne serum giver den perfekte balance mellem hudforyngelse og klarhed. ", product.Description);
            Assert.AreEqual(1315.00m, product.Price);


        }

    }
}
