using System.Net;
using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class UnitTestGetAllCustomers
    {
        [TestMethod]
        public void TestGetAllCustomers()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                CustomerRepository customerRepo = new CustomerRepository();

                List<Customer> allCustomers0 = customerRepo.GetAllCustomers();
                int count0 = allCustomers0.Count();

                Customer customer1 = Customer.MakeNewCustomerFromUI("Tove", "Hils", "Ypper All4 8, 6688 Høng", "837483930", "ToveHils@gmail.com");
                Customer customer2 = Customer.MakeNewCustomerFromUI("Mads", "Hils", "Ypper All4 8, 6688 Høng", "837483930", "MadsHils@gmail.com");

                customerRepo.AddCustomer(customer1);
                customerRepo.AddCustomer(customer2);



                // Act
                List<Customer> allCustomers1 = customerRepo.GetAllCustomers();
                int count1 = allCustomers1.Count();


                // Assert
                Assert.AreEqual((count0 + 2), count1);
               

            }




        }
    }
}

