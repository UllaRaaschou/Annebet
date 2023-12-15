using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class CustomerTestCreateCustomerFromUI
    {
        [TestMethod]

        public void TestCustomerCreateCustomerFromUI()
        {
            // Arrange
            string firstName = "Jens";
            string lastName = "Hansen";
            string address = "Enebærvej 17, 4000 Roskilde";
            string phone = "1234567890";
            string email = "JensHansen@gmail.com";

            // Act
            Customer customer = Customer.CreateCustomerFromUI(firstName, lastName, address, phone, email);

            // Assert
            Assert.AreEqual("Jens", customer.FirstName);
            Assert.AreEqual("Hansen", customer.LastName);
            Assert.AreEqual("Enebærvej 17, 4000 Roskilde", customer.Address);
            Assert.AreEqual("1234567890", customer.Phone);
            Assert.AreEqual("JensHansen@gmail.com", customer.Email);
            
                           
        }
    }
}