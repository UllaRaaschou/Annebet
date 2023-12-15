using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class CustomerRepositoryTestAddAndGetCustomer
    {

        /// <summary>
        /// Metode, hvis eneste formål er at oprette en unik customer. Viste sig at være en god ide, fordi vi sad 3 studerende og arbejdede
        /// på samme database og de customers, som vi oprettede i vores unit-tests, havde samme for- og efternavn som kunder, der 
        /// allerede var i databasen
        /// </summary>      
        private Customer CreateUniqueCustomer() 
        {
            Guid guid = Guid.NewGuid();
            return Customer.CreateCustomerFromUI("uniqueName" + guid, "Petersen", "Granvej 5, 3000 Gryse", "789849302", "AnnaPetersen@gmail.com");
        }



        [TestMethod]
        public void TestAddAndGetCustomer()
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
                CustomerRepository customerRepo = new CustomerRepository();
                Customer uniqueCustomer = CreateUniqueCustomer();


                // Act
                int newId = customerRepo.AddCustomer(uniqueCustomer);
                List<Customer> listOfuniqueCustomer = customerRepo.GetAllCustomers(uniqueCustomer.FirstName, "Petersen");


                // Assert
                Assert.AreEqual(listOfuniqueCustomer[0].Id, newId);
                Assert.AreEqual(listOfuniqueCustomer[0].FirstName, uniqueCustomer.FirstName);
                Assert.AreEqual(listOfuniqueCustomer[0].LastName, "Petersen");
                Assert.AreEqual(listOfuniqueCustomer[0].Address, "Granvej 5, 3000 Gryse");
                Assert.AreEqual(listOfuniqueCustomer[0].Phone, "789849302");
                Assert.AreEqual(listOfuniqueCustomer[0].Email, "AnnaPetersen@gmail.com");
            }
            
           

        }
    }
}