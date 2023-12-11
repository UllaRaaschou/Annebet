using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class ProductRepositoryTestAddAndGetProduct
    {
        [TestMethod]
        public void TestAddAndGetProduct()
        {
           using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                ProductRepository productRepo = new ProductRepository();


                // Act
                int productId = productRepo.AddProduct(Product.CreateProductFromUI("HerreSerien", "HerreCreme", "En herrecreme", 1200.00m));
                List<Product> addetProduct = productRepo.GetAllProductsFromCategoryAndTypeAndName("HerreSerien", "Herrecreme");

                // Assert
                Assert.AreEqual((addetProduct[0].Id), productId);
                Assert.AreEqual((addetProduct[0].Description), "En herrecreme");
                Assert.AreEqual((addetProduct[0].Price), 1200.00m);

            }

        }
    }
}
