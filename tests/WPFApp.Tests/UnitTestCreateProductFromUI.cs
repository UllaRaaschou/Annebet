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
            EnumCategory category = EnumCategory.Product;
            string type = "Scinbetter Science";
            string name = "AlphaRet® Clearing Serum";
            string description = "Denne serum giver den perfekte balance mellem hudforyngelse og klarhed. ";
            decimal price = 1315.00m;

            // Act
            SalesItem salesItem = SalesItem.CreateSalesItemFromUI(type, name, description, price, category);

            // Assert
            Assert.AreEqual(EnumCategory.Product, salesItem.Category);
            Assert.AreEqual("Scinbetter Science", salesItem.Type);
            Assert.AreEqual("AlphaRet® Clearing Serum", salesItem.Name);
            Assert.AreEqual("Denne serum giver den perfekte balance mellem hudforyngelse og klarhed. ", salesItem.Description);
            Assert.AreEqual(1315.00m, salesItem.Price);


        }

    }
}
