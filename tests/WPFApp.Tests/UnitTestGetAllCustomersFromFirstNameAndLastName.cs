using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class UnitTestGetAllCustomersFromFirstNameAndLastName
    {
        [TestMethod]
        public void TestGetAllCustomersFromFirstNameAndLastName()
        {
            // Arrange
            CustomerRepository customerRepo = new CustomerRepository();
            customerRepo.AddCustomer(Customer.CreateCustomerFromUI("Fedt", "Mule", "Disneyvej 3", "11111", "fedtmule@hotmail.com"));
            customerRepo.AddCustomer(Customer.CreateCustomerFromUI("Bestemor", "And", "Disneyvej 20", "2222", "Bedste@hotmail.com"));


            // Act
            List<Customer> customerWithSpecificNames = customerRepo.GetAllCustomersFromFirstNameAndLastName("Fedt", "Mule");
            Customer chosenCustomer = customerWithSpecificNames[0];
           

            // Assert
            Assert.AreEqual(chosenCustomer.FirstName, "Fedt");
            Assert.AreEqual(chosenCustomer.LastName, "Mule");
            Assert.AreEqual(chosenCustomer.Address, "Disneyvej 3");
            Assert.AreEqual(chosenCustomer.Phone, "11111");
            Assert.AreEqual(chosenCustomer.Email, "fedtmule@hotmail.com");

        }

    }
}
