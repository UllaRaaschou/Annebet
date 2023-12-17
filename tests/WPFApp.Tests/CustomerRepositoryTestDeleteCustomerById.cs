using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class CustomerRepositoryTestDeleteCustomerById
    {
        [TestMethod]
        public void TestCustomerRepositoryDeleteCustomerById()
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
                int newId = customerRepo.AddCustomer(Customer.CreateCustomerFromUI("Ninna", "Nielsen", "Bogensevej 20, 8600 Bogense", "33334444", "NinnaNielsen@gmail.com"));
                List<Customer> listOfNinnaNielsen = customerRepo.GetAllCustomers("Ninna", "Nielsen");
                int count1 = listOfNinnaNielsen.Count();
                
                // Act
                customerRepo.DeleteCustomerById(newId);
                List<Customer> listOfNinnaNielsen2 = customerRepo.GetAllCustomers("Ninna", "Nielsen");
                int count2 = listOfNinnaNielsen2.Count();


                // Assert
                Assert.AreEqual((count1-1),count2);
            }
                
           

        }
    }
}