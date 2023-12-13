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
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted })) 
            {
                // Arrange
                CustomerRepository customerRepo = new CustomerRepository();
                int newId = customerRepo.AddCustomer(Customer.CreateCustomerFromUI("Ninna", "Nielsen", "Bogensevej 20, 8600 Bogense", "33333333", "NinnaNielsen@gmail.com"));
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