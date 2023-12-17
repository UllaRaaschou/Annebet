using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Commands;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Tests
{
    [TestClass]
    public class CustomerUpdateCommandTests
    {
        [TestMethod]
        public void TestExecute() 
        {
            // Arrange
            CustomerTESTRepository repository = new CustomerTESTRepository();
            CustomerUpdateCommand cuc = new CustomerUpdateCommand(repository);
            CustomerUpdateViewModel viewModel = new CustomerUpdateViewModel();
            Customer startCustomer = Customer.CreateCustomerFromDb(2, "", "", "", "", "");
            repository.customers.Add(startCustomer);
            Customer updatedCustomer = Customer.CreateCustomerFromDb(2, "Mikkel", "Madsen", "Munkevej 3", "67781111", "munk@gmail.com");
            CustomerToViewModel viewModelCustomer = new CustomerToViewModel();
            CustomerToViewModel updatedViewModelCustomer = viewModelCustomer.CustomerToViewModelConvert(updatedCustomer);
            viewModel.SelectedCustomer = updatedViewModelCustomer;


            // Act
            cuc.Execute(viewModel);

            //Assert
            Assert.AreEqual(repository.customers[0].Id, 2);
            Assert.AreEqual(repository.customers[0].FirstName, "Mikkel");
            Assert.AreEqual(repository.customers[0].LastName, "Madsen");
            Assert.AreEqual(repository.customers[0].Address, "Munkevej 3");
            Assert.AreEqual(repository.customers[0].Phone, "67781111");
            Assert.AreEqual(repository.customers[0].Email, "munk@gmail.com");

        }

        [TestMethod]
        public void TestIsSelectedCustomerNullAfterExecute() 
        {
            // Arrange
            CustomerTESTRepository repository = new CustomerTESTRepository();
            CustomerUpdateCommand cuc = new CustomerUpdateCommand(repository);
            CustomerUpdateViewModel viewModel = new CustomerUpdateViewModel();
            Customer startCustomer = Customer.CreateCustomerFromDb(2, "", "", "", "", "");
            repository.customers.Add(startCustomer);
            Customer updatedCustomer = Customer.CreateCustomerFromDb(2, "Mikkel", "Madsen", "Munkevej 3", "67789", "munk@gmail.com");
            CustomerToViewModel viewModelCustomer = new CustomerToViewModel();
            CustomerToViewModel updatedViewModelCustomer = viewModelCustomer.CustomerToViewModelConvert(updatedCustomer);
            viewModel.SelectedCustomer = updatedViewModelCustomer;


            // Act
            cuc.Execute(viewModel);

            // Assert
            Assert.IsNull(viewModel.SelectedCustomer);
            Assert.IsNull(viewModel.FirstName);
            Assert.IsNull(viewModel.LastName);

        }

        [TestMethod]
        public void TestcanEcecuteWithoutNullValues() 
        {
            // Arrange
            CustomerUpdateViewModel viewModel = new CustomerUpdateViewModel();
            Customer customer = Customer.CreateCustomerFromDb(2, "Mikkel", "Madsen", "Munkevej 3", "67789", "munk@gmail.com");
            CustomerToViewModel viewModelCustomer = new CustomerToViewModel();
            CustomerToViewModel convertedViewModelCustomer = viewModelCustomer.CustomerToViewModelConvert(customer);
            viewModel.SelectedCustomer = convertedViewModelCustomer;
            CustomerUpdateCommand cuc = new CustomerUpdateCommand();


            // Act
            bool result = cuc.CanExecute(viewModel);

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void TestcanEcecuteWithNullValues()
        {
            // Arrange
            CustomerUpdateViewModel viewModel = new CustomerUpdateViewModel();
            viewModel.SelectedCustomer = null;
            CustomerUpdateCommand cuc = new CustomerUpdateCommand();


            // Act
            bool result = cuc.CanExecute(viewModel);

            //Assert
            Assert.IsFalse(result);

        }



    }
}
