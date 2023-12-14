using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFApp.Models;
using WPFApp.ViewModels;
using WPFApp.Commands;


namespace WPFApp.Tests
{
    [TestClass]
    public class CustomerCreateCommandTests
    {
        [TestMethod]
        public void TestCustomerCreateCommand()
        {
            // Arrange
            CustomerTESTRepository repository = new CustomerTESTRepository(); // Instantiereing af Test-repo
            CustomerCreateCommand ccc = new CustomerCreateCommand(repository);  // Test-repo bruges som parameter i vores Command-Construcor, hvorved vi skifter fra brug
                                                                                // af løsningens normale repo til vores testRepo.

            CustomerCreateViewModel viewModel = new CustomerCreateViewModel();
            viewModel.FirstName = "Karen";
            viewModel.LastName = "Kost";
            viewModel.Address = "Kirkevej 4, 4000 Roskilde";
            viewModel.Phone = "1234567";
            viewModel.Email = "Karen@gmail.com";

            // Act
            ccc.Execute(viewModel);


            // assert
            Assert.AreEqual(repository.customers[0].FirstName, "Karen");
            Assert.AreEqual(repository.customers[0].LastName, "Kost");
            Assert.AreEqual(repository.customers[0].Address, "Kirkevej 4, 4000 Roskilde");
            Assert.AreEqual(repository.customers[0].Phone, "1234567");
            Assert.AreEqual(repository.customers[0].Email, "Karen@gmail.com");
        }

        [TestMethod]
        public void TestViewModelValuesToNull() 
        {
            CustomerTESTRepository repository = new CustomerTESTRepository();
            CustomerCreateCommand ccc = new CustomerCreateCommand(repository);
            CustomerCreateViewModel viewModel = new CustomerCreateViewModel();
            viewModel.FirstName = "Karen";
            viewModel.LastName = "Kost";
            viewModel.Address = "Kirkevej 4, 4000 Roskilde";
            viewModel.Phone = "1234567";
            viewModel.Email = "karen@gmail.com";

            // Act
            ccc.Execute(viewModel);

            // Assert
            Assert.IsNull(viewModel.FirstName);
            Assert.IsNull(viewModel.LastName);
            Assert.IsNull(viewModel.Address);
            Assert.IsNull(viewModel.Phone);
            Assert.IsNull(viewModel.Email);
        }

        [TestMethod]
        public void TestCanExecuteWithoutNullValues() 
        {
            // Arrange
            CustomerTESTRepository repository = new CustomerTESTRepository();
            CustomerCreateCommand ccc = new CustomerCreateCommand(repository);
            CustomerCreateViewModel viewModel = new CustomerCreateViewModel();
            viewModel.FirstName = "Karen";
            viewModel.LastName = "Kost";
            viewModel.Address = "Kirkevej 4, 4000 Roskilde";
            viewModel.Phone = "1234567";
            viewModel.Email = "karen@gmail.com";

            // Act
            bool result = ccc.CanExecute(viewModel);

            // Assert
            Assert.IsTrue(result);

        }

        public void TestCanExecuteWithNullValues()
        {
            // Arrange
            CustomerTESTRepository repository = new CustomerTESTRepository();
            CustomerCreateCommand ccc = new CustomerCreateCommand(repository);
            CustomerCreateViewModel viewModel = new CustomerCreateViewModel();
            viewModel.FirstName = null;
            viewModel.LastName = "Kost";
            viewModel.Address = "Kirkevej 4, 4000 Roskilde";
            viewModel.Phone = "1234567";
            viewModel.Email = "karen@gmail.com";

            // Act
            bool result = ccc.CanExecute(viewModel);

            // Assert
            Assert.IsFalse(result);

        }

    }
}
