using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class UnitTestGetAllSalesItemsFromCategoryAndTypeAndName
    {
        [TestMethod]
        public void TestGetAllSalesItemsFromCategoryAndTypeAndName() 
        {
            // Arrange
            POTRepository pOTRepo = new POTRepository();
            pOTRepo.AddSalesItem(Product.CreateProductFromUI("Fodpleje", "FeetScrub", "God scrubbecreme", 300.00m));
            pOTRepo.AddSalesItem(Treatment.CreateTreatmentFromUI("Massage", "Ansigtmassage", "Blid massage", 1200.00m));

            // Act
            List<SalesItem> salesItems = pOTRepo.GetAllSalesItemsFromCategoryAndTypeAndName(EnumCategory.Product, "Fodpleje", "Feetscrub");
            Product chosenProduct = (Product)salesItems[0];

            // Assert
            Assert.AreEqual(chosenProduct.Type, "Fodpleje");
            Assert.AreEqual(chosenProduct.Name, "Feetscrub");
            Assert.AreEqual(chosenProduct.Description, "God scrubbecreme");
            Assert.AreEqual(chosenProduct.Price, 300.00m);

         }

    }
}
