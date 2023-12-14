using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class ProductRepositoryTestDeleProductById
    {
        [TestMethod]
        public void TestDeleteProductById()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                ProductRepository productRepo = new ProductRepository();
                int idProdukt = productRepo.AddProduct(Product.CreateProductFromUI("Værktøj", "Negleklipper", "Effektiv negleklipper", 200.00m));
                List<Product> products = productRepo.GetAllProducts("Værktøj", "Negleklipper");
                int count1 = products.Count();  
               

                // Act
                productRepo.DeleteProductById(idProdukt);
                List<Product> products2 = productRepo.GetAllProducts("Værktøj", "Negleklipper");
                int count2 = products.Count();


                // Assert
                Assert.AreEqual((count1-1),count2);
            }



        }
    }
}
