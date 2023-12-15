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
    public class CustomerSearchCommandTests
    {
        [TestMethod] 
        public void TestexecuteWithCustomerUpdateViewModel() 
        {
            // Arrange
            CustomerTESTRepository repository = new CustomerTESTRepository();
            repository.customers.Add(Customer.CreateCustomerFromDb(4, "Lotte", "Hansen", "Lottevej 3", "8765", "Lotte@gmail.com"));
            repository.customers.Add(Customer.CreateCustomerFromDb(7, "Lotte", "Hansen", "Skovvej 5", "98765", "Hansen@gmail.com"));
            CustomerSearchCommand csc = new CustomerSearchCommand(repository);
            CustomerUpdateViewModel viewModel = new CustomerUpdateViewModel();  
            viewModel.FirstName = "Lotte";
            viewModel.LastName = "Hansen";
           

            // Act
            csc.Execute(viewModel);


            // Assert
            Assert.AreEqual(viewModel.CustomersToView.Count(), 2);

        }

        [TestMethod]
        public void TestexecuteWithCustomerDeleteViewModel()
        {
            // Arrange
            CustomerTESTRepository repository = new CustomerTESTRepository();
            repository.customers.Add(Customer.CreateCustomerFromDb(4, "Lotte", "Hansen", "Lottevej 3", "8765", "Lotte@gmail.com"));
            repository.customers.Add(Customer.CreateCustomerFromDb(7, "Lotte", "Hansen", "Skovvej 5", "98765", "Hansen@gmail.com"));
            CustomerSearchCommand csc = new CustomerSearchCommand(repository);
            CustomerDeleteViewModel viewModel = new CustomerDeleteViewModel();
            viewModel.FirstName = "Lotte";
            viewModel.LastName = "Hansen";


            // Act
            csc.Execute(viewModel);


            // Assert
            Assert.AreEqual(viewModel.CustomersToView.Count(), 2);

        }

        [TestMethod]
        public void TestCanExecuteWithCustomerUpdateViewModelWithoutNullValues() 
        {
            // Arrange
            CustomerSearchCommand csc = new CustomerSearchCommand();
            CustomerUpdateViewModel viewModel = new CustomerUpdateViewModel();
            viewModel.FirstName = "Lotte";
            viewModel.LastName = "Hansen";

            // Act
            bool result = csc.CanExecute(viewModel);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void TestCanExecuteWithCustomerUpdateViewModelWithNullValues()
        {
            // Arrange
            CustomerSearchCommand csc = new CustomerSearchCommand();
            CustomerUpdateViewModel viewModel = new CustomerUpdateViewModel();
            viewModel.FirstName = "Lotte";
            viewModel.LastName = null;

            // Act
            bool result = csc.CanExecute(viewModel);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCanExecuteWithCustomerDeleteViewModelWithoutNullValues()
        {
            // Arrange
            CustomerSearchCommand csc = new CustomerSearchCommand();
            CustomerDeleteViewModel viewModel = new CustomerDeleteViewModel();
            viewModel.FirstName = "Lotte";
            viewModel.LastName = "Hansen";

            // Act
            bool result = csc.CanExecute(viewModel);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void TestCanExecuteWithCustomerDeleteViewModelWithNullValues()
        {
            // Arrange
            CustomerSearchCommand csc = new CustomerSearchCommand();
            CustomerDeleteViewModel viewModel = new CustomerDeleteViewModel();
            viewModel.FirstName = "Lotte";
            viewModel.LastName = null;

            // Act
            bool result = csc.CanExecute(viewModel);

            // Assert
            Assert.IsFalse(result);
        }


    }
}
