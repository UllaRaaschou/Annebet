using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class UnitTestUpdateCustomer
    {
        [TestMethod]
        public void TestUpdateCustomer()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                Customer customer = Customer.MakeNewCustomerFromUI("Thilde", "Torn", "Thisevej 3, 8000 Kolding", "748394857", "ThildeTorn@gmail.com");
                CustomerRepository customerRepo = new CustomerRepository();
                int id = customerRepo.AddCustomer(customer);
                Customer customerWithUpdatedValues = Customer.MakeNewCustomerFromUI("Tora", "Torn", "Thisevej 3, 8000 Kolding", "748394857", "ThildeTorn@gmail.com");


                
                // Act
                customerRepo.UpdateCustomer(customerWithUpdatedValues, id);


                // Assert
                Assert.AreEqual("Tora", customerWithUpdatedValues.FirstName);

            }
            

        }
    }
}