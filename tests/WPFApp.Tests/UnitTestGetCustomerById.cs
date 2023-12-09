using System.Net;
using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class UnitTestGetCustomerById
    {
        [TestMethod]
        public void TestGetCustomerById()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                Customer customer = Customer.CreateCustomerFromUI("Benny", "Gron", "Tidsel All4 8, 5599 Nitby", "837483930", "GerdaGrøn@gmail.com");
                CustomerRepository customerRepo = new CustomerRepository();
                int id = customerRepo.AddCustomer(customer);
                Customer customer2 = Customer.CreateCustomerFromDb(id, "Benny", "Gron", "Tidsel All4 8, 5599 Nitby", "837483930", "GerdaGrøn@gmail.com");

                // Act
                Customer? customer1 = customerRepo.GetCustomerById(id);

                // Assert
                Assert.AreEqual(customer2.Id, customer1.Id);
                Assert.AreEqual(customer2.FirstName, customer1.FirstName);
                Assert.AreEqual(customer2.LastName, customer1.LastName);
                Assert.AreEqual(customer2.Phone, customer1.Phone);
                Assert.AreEqual(customer2.Email, customer1.Email);

            }
            
        


        }
    }
}
