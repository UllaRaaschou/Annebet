using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class ProductRepositoryTestDeleteProductById
    {
        [TestMethod]
        public void TestDeleteProductById()
        {
            // "TranscationScope": Laver testen til et scope, hvor alle operationer udføres i een transaktion
            //                     Enten udføres alle - eller også rulles alle tilbage. Vi ruller alle tilbage.
            // "TransactionScopeOption.Required": Hvis der ikke allerede ER et scope, oprettes der ét.
            // "new TransactionOptions(){}": Instantiering af klasse, der angiver flere detaljer for transaktionen
            // "Isolationlevel.ReadUncommitted": Giver mulighed for at læse un-committet data
            // "Scope.Complete(): Hvis vi havde kaldt denne metode, ville transaktion blive comittet. Vi comitter ikke! 

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
                int count2 = products2.Count();


                // Assert
                Assert.AreEqual((count1-1),count2);
            }



        }
    }
}
