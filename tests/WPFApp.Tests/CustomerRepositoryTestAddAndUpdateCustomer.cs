using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{

    [TestClass]
    public class CustomerRepositoryTestAddAndUpdateCustomer
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

        /// <summary>
        /// Guid-metoden genbruges, med id fra første Guid som parameter, for at skabe en unit, opdateret version.
        /// </summary>
      
        private Customer CreateUniqueCustomer2(int id)
        {
            Guid guid = Guid.NewGuid();
            return Customer.CreateCustomerFromDb(id, "uniqueName" + guid, "Petersen", "Granvej 5, 3000 Gryse", "789849302", "AnnaPetersen@gmail.com");
        }




    [TestMethod]
        public void TestCustomerRepositoryAddAndUpdateCustomer()
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

                int id = customerRepo.AddCustomer(uniqueCustomer);
                List<Customer> newList= customerRepo.GetAllCustomers(uniqueCustomer.FirstName, uniqueCustomer.LastName);
                Customer uniqueUpdatedValues = CreateUniqueCustomer2(id);
                
                
                // Act
                customerRepo.UpdateCustomer(uniqueUpdatedValues);
                List<Customer> updatedList = customerRepo.GetAllCustomers(uniqueUpdatedValues.FirstName, uniqueUpdatedValues.LastName);


                // Assert

                Assert.AreEqual(id, newList[0].Id);
                Assert.AreEqual(uniqueCustomer.FirstName, newList[0].FirstName);
                Assert.AreEqual(uniqueCustomer.LastName, newList[0].LastName);
                Assert.AreEqual(uniqueCustomer.Address, newList[0].Address);
                Assert.AreEqual(uniqueCustomer.Phone, newList[0].Phone);
                Assert.AreEqual(uniqueCustomer.Email, newList[0].Email);
               
                Assert.AreEqual(id, updatedList[0].Id);
                Assert.AreEqual(uniqueUpdatedValues.FirstName, updatedList[0].FirstName);
                Assert.AreEqual(uniqueUpdatedValues.LastName, updatedList[0].LastName);
                Assert.AreEqual(uniqueUpdatedValues.Address, updatedList[0].Address);
                Assert.AreEqual(uniqueUpdatedValues.Phone, updatedList[0].Phone);
                Assert.AreEqual(uniqueUpdatedValues.Email, updatedList[0].Email);


            }
            

        }
    }
}