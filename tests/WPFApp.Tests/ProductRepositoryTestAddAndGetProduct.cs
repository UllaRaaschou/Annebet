using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class ProductRepositoryTestAddAndGetProduct
    {
        /// <summary>
        /// Metode, hvis eneste formål er at oprette et unikt produkt. Viste sig at være en god ide, fordi vi sad 3 studerende og arbejdede
        /// på samme database og de produkter, som vi oprettede i vores unit-tests, havde navne som produkter, der 
        /// allerede var i databasen
        /// </summary>      
        private Product CreateUniqueProduct()
        {
            Guid guid = Guid.NewGuid();
            return Product.CreateProductFromUI("Stjerneserien" + guid, "Ansigtscreme", "Fugtcreme til ansigtet", 450.00m);
        }




        [TestMethod]
        public void TestAddAndGetProduct()
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
                Product uniqueProduct = CreateUniqueProduct();  // unikt product oprettes


               // Act
                int newId = productRepo.AddProduct(uniqueProduct);
                List<Product> listOfuniqueProduct = productRepo.GetAllProducts(uniqueProduct.Type, "Ansigtscreme");



                // Assert
                Assert.AreEqual(listOfuniqueProduct[0].Id, newId);
                Assert.AreEqual(listOfuniqueProduct[0].Type, uniqueProduct.Type);
                Assert.AreEqual(listOfuniqueProduct[0].Name, uniqueProduct.Name);
                Assert.AreEqual(listOfuniqueProduct[0].Description, uniqueProduct.Description);
                Assert.AreEqual(listOfuniqueProduct[0].Price, uniqueProduct.Price);

            }

        }
    }
}
