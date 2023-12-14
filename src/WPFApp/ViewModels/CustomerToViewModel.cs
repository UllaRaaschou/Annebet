using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models;

namespace WPFApp.ViewModels
{
    public class CustomerToViewModel
    {
        public CustomerRepository customerRepo;  // CustomerRepo deklareres

        public CustomerToViewModel()  // Construktor, parameterløs,  der intantierer CustomerRepo
        {
            customerRepo = new CustomerRepository();
        }
        public CustomerToViewModel(Customer customer) // Constructor, der tager Customer som parameter og sætter klassens properties ud fra Customer-objekt
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Address = customer.Address;
            Phone = customer.Phone;
            Email = customer.Email;
        }

          /// <summary>
          /// Metode, der converter et object af typen Customer til et object af typen CustomerToViewModel
          /// </summary>
        public CustomerToViewModel CustomerToViewModelConvert(Customer customer)  
        {
            int id = customer.Id;
            string firstName = customer.FirstName;
            string lastName = customer.LastName;
            string address = customer.Address;
            string phone = customer.Phone;
            string email = customer.Email;
            Customer createdCustomer = Customer.CreateCustomerFromDb(id, firstName, lastName, address, phone, email); // Customer-klassens statiske metode creater Customer
            CustomerToViewModel model = new CustomerToViewModel(createdCustomer); // Customer converteres til CustomerToViewModel
            return model; //CustomerToViewModel returneres

        }



        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        
        /// <summary>
        /// Metode, der via repo henter ønskede Customers i db
        /// </summary>
       public List<Customer> SearchForCustomers(string firstName, string lastName)
        {
            List<Customer> customers = customerRepo.GetAllCustomers(FirstName, LastName);
            return customers;
        }
    }
}

