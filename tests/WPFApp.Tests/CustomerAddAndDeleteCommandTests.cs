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
    public class CustomerAddAndDeleteCommandTests
    {
        [TestMethod]
        public void TestExecute()
        {
            // Arrange
            CustomerTESTRepository repository = new CustomerTESTRepository();   // Instantiereing af Test-repo
                                                                                //Test - repo bruges som parameter i vores Command-Construcor, hvorved vi skifter fra brug
                                                                                // af løsningens normale repo til vores testRepo.

            Customer customer = Customer.CreateCustomerFromDb(3, "Minna", "Olsen", "Minnavej 3", "52627888", "Minna@gmail.com");
            repository.customers.Add(customer);

            CustomerDeleteCommand cdc = new CustomerDeleteCommand(repository);
            CustomerDeleteViewModel viewModel = new CustomerDeleteViewModel();
            
            CustomerToViewModel viewModelCustomer = new CustomerToViewModel();
            viewModel.SelectedCustomer = viewModelCustomer.CustomerToViewModelConvert(customer);

                      
           
           
            // Act
            cdc.Execute(viewModel);



            // assert
            Assert.IsTrue(repository.customers.Count() == 0);
            
        }




        [TestMethod]
        public void TestViewModelValuesToNullAfterExecute()
        {
            CustomerTESTRepository repository = new CustomerTESTRepository();

            Customer customer = Customer.CreateCustomerFromDb(3, "Minna", "Olsen", "Minnavej 3", "52627", "Minna@gmail.com");
            repository.customers.Add(customer);

            CustomerDeleteCommand cdc = new CustomerDeleteCommand(repository);
            CustomerDeleteViewModel viewModel = new CustomerDeleteViewModel();

            CustomerToViewModel viewModelCustomer = new CustomerToViewModel();
            viewModel.SelectedCustomer = viewModelCustomer.CustomerToViewModelConvert(customer);


            // Act
            cdc.Execute(viewModel);


            // Assert
            Assert.IsNull(viewModel.FirstName);
            Assert.IsNull(viewModel.LastName);
            Assert.IsNull(viewModel.SelectedCustomer);
        }



        
        [TestMethod]
        public void TestCanExecuteWithoutNullValuesInViewModelProperties()
        {
            // Arrange
            CustomerTESTRepository repository = new CustomerTESTRepository();
            CustomerDeleteCommand cdc = new CustomerDeleteCommand(repository);
            CustomerDeleteViewModel viewModel = new CustomerDeleteViewModel();
            Customer customer = Customer.CreateCustomerFromUI("Minna", "Olsen", "Minnavej 3", "52627", "Minna@gmail.com");
            CustomerToViewModel customerToViewModel = new CustomerToViewModel();
            CustomerToViewModel viewModelCustomer = new CustomerToViewModel ();
            
            viewModel.SelectedCustomer = viewModelCustomer.CustomerToViewModelConvert(customer);
            viewModel.SelectedCustomer.FirstName = "Minna";
            viewModel.SelectedCustomer.LastName = "Olsen";
            viewModel.SelectedCustomer.Address = "Minnavej 3";
            viewModel.SelectedCustomer.Phone = "52627";
            viewModel.SelectedCustomer.Email = "Minna@gmail.com";

            // Act
            bool result = cdc.CanExecute(viewModel);

            // Assert
            Assert.IsTrue(result);  // Hvis ViewModelProperties er udfyldt, skal execute returnere True

        }

        [TestMethod]
        public void TestCanExecuteWithNullValueInViewModelProperties()
        {
            // Arrange
            CustomerTESTRepository repository = new CustomerTESTRepository();
            CustomerDeleteCommand cdc = new CustomerDeleteCommand(repository);
            CustomerDeleteViewModel viewModel = new CustomerDeleteViewModel();
            Customer customer = Customer.CreateCustomerFromUI("Minna", "Olsen", "Minnavej 3", "52627", "Minna@gmail.com");
            CustomerToViewModel customerToViewModel = new CustomerToViewModel();
            CustomerToViewModel viewModelCustomer = new CustomerToViewModel();

            viewModel.SelectedCustomer = viewModelCustomer.CustomerToViewModelConvert(customer);
            viewModel.SelectedCustomer.FirstName = null;
            viewModel.SelectedCustomer.LastName = "Olsen";
            viewModel.SelectedCustomer.Address = "Minnavej 3";
            viewModel.SelectedCustomer.Phone = "52627";
            viewModel.SelectedCustomer.Email = "Minna@gmail.com";

            // Act
            bool result = cdc.CanExecute(viewModel);

            // Assert
            Assert.IsFalse(result);

        }

    }
}
