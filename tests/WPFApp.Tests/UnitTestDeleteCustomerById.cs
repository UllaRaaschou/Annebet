using System.Net;
using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class UnitTestDeleteCustomerById
    {
        [TestMethod]
        public void TestDeleteCustomerById()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted })) 
            {
                // Arrange
                Customer customer1 = Customer.CreateCustomerFromUI("Ninna", "Nielsen", "Bogensevej 20, 8600 Bogense", "33333333", "NinnaNielsen@gmail.com");
                Customer customer2 = Customer.CreateCustomerFromUI("Peter", "Krakow", "Hyttevej 10", "857937493", "PeterKrakow@gmail.com");

                CustomerRepository customerRepo = new CustomerRepository();
                customerRepo.AddCustomer(customer1);
                int IdCustomer2 = customerRepo.AddCustomer(customer2);

                List<Customer> allCustomers = customerRepo.GetAllCustomers();
                int customerCount = allCustomers.Count();


                // Act
                customerRepo.DeleteCustomerById(IdCustomer2);
                List<Customer> allCustomers2 = customerRepo.GetAllCustomers();
                int customerCount2 = allCustomers2.Count();

                // Assert
                Assert.AreEqual((customerCount - 1), customerCount2);
            }
                
           

        }
    }
}