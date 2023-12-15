using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class ProductRepositoryTestAddAndUpdate
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

        /// <summary>
        /// Guid-metoden genbruges, med id fra første Guid som parameter, for at skabe en unit, opdateret version.
        /// </summary>

        private Product CreateUniqueProduct2(int id)
        {
            Guid guid = Guid.NewGuid();
            return Product.CreateProductFromDb(id, "Måneserien" + guid, "AnsigtFugtighedsscreme", "Ansigtscreme med fugt", 550.00m);
        }




        [TestMethod]
        public void TestProductRepositoryAddAndUpdate()
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
                Product uniqueProduct = CreateUniqueProduct();                
                int id = productRepo.AddProduct(uniqueProduct);
                List<Product> newList = productRepo.GetAllProducts(uniqueProduct.Type, uniqueProduct.Name);
                Product uniqueUpdatedValues = CreateUniqueProduct2(id);
                
                                
                // Act
                productRepo.UpdateProduct(uniqueUpdatedValues);
                List<Product> updatedList = productRepo.GetAllProducts(uniqueUpdatedValues.Type, uniqueUpdatedValues.Name);


                // Assert
                Assert.AreEqual(id, newList[0].Id);
                Assert.AreEqual(uniqueProduct.Type, newList[0].Type);
                Assert.AreEqual(uniqueProduct.Name, newList[0].Name);
                Assert.AreEqual(uniqueProduct.Description, newList[0].Description);
                Assert.AreEqual(uniqueProduct.Price, newList[0].Price);


                Assert.AreEqual(id, updatedList[0].Id);
                Assert.AreEqual(uniqueUpdatedValues.Type, updatedList[0].Type);
                Assert.AreEqual(uniqueUpdatedValues.Name, updatedList[0].Name);
                Assert.AreEqual(uniqueUpdatedValues.Description, updatedList[0].Description);
                Assert.AreEqual(uniqueUpdatedValues.Price, updatedList[0].Price);


            }


        }
    }
}
