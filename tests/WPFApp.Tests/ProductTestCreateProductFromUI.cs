using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class ProductTestCreateProductFromUI
    {
        [TestMethod]
        public void TestProductCreateProductFromUI()
        {
            // Arrange           
            string type = "Massageolie";
            string name = "Lavendelolie";
            string description = "God massageolie med lavendel";
            decimal price = 300.00m;

            // Act
            Product product = Product.CreateProductFromUI(type, name, description, price);
            // Assert
            Assert.AreEqual("Massageolie", product.Type);
            Assert.AreEqual("Lavendelolie", product.Name);
            Assert.AreEqual("God massageolie med lavendel", product.Description);
            Assert.AreEqual(300.00m, product.Price);


        }
    }
}
