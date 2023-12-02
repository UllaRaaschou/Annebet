using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class UnitTestAddCustomer
    {
        [TestMethod]
        public void TestAddCustomer()
        {
            using(TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted })) 
            {
                // Arrange
                CustomerRepository customerRepo = new CustomerRepository();

                List<Customer> allCustomers = customerRepo.GetAllCustomers();
                int firstCount = allCustomers.Count();

                Customer customer = Customer.MakeNewCustomerFromUI("Anne999", "Petersen", "granvej 5, 3000 Gryse", "789849302", "AnnaPetersen@gmail.com");


                // Act
                customerRepo.AddCustomer(customer);
                List<Customer> allCustomers2 = customerRepo.GetAllCustomers();
                int secondCount = allCustomers2.Count();


                // Assert
                Assert.AreEqual((firstCount + 1), secondCount);
            }
            
           

        }
    }
}