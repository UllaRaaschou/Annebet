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
        public void Testexecute() 
        {
            // Arrange
            CustomerTESTRepository repository = new CustomerTESTRepository();
            repository.customers.Add(Customer.CreateCustomerFromDb(4, "Lotte", "Hansen", "Lottevej 3", "8765", "Lotte@gmail.com"));
            repository.customers.Add(Customer.CreateCustomerFromDb(7, "Lotte", "Hansen", "Skovvej 5", "98765", "Hansen@gmail.com"));
            CustomerSearchCommand csc = new CustomerSearchCommand(repository);
            CustomerUpdateViewModel viewModel = new CustomerUpdateViewModel();  
            viewModel.FirstName = "Lotte";
            viewModel.LastName = "Hansen";
            //CustomerToViewModel viewModelCustomer = new CustomerToViewModel();
            
            //List<Customer> wantedCustomers = repository.GetAllCustomers("Lotte", "Hansen");
            //List<CustomerToViewModel> customersToView = new List<CustomerToViewModel>();

            // Act
            csc.Execute(viewModel);


            // Assert
            Assert.AreEqual(viewModel.CustomersToView.Count(), 2);
        }
        
    }
}
