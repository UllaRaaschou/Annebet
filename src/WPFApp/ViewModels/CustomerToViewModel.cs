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
        public CustomerRepository customerRepo;

        public CustomerToViewModel()
        {
            customerRepo = new CustomerRepository();
        }
        public CustomerToViewModel(Customer customer)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Address = customer.Address;
            Phone = customer.Phone;
            Email = customer.Email;


            CustomerToViewModel model = new CustomerToViewModel();
        }

        public CustomerToViewModel CustomerToViewModelConvert(Customer customer)
        {
            int id = customer.Id;
            string firstName = customer.FirstName;
            string lastName = customer.LastName;
            string address = customer.Address;
            string phone = customer.Phone;
            string email = customer.Email;
            Customer createdCustomer = Customer.CreateCustomerFromDb(id, firstName, lastName, address, phone, email);
            CustomerToViewModel model = new CustomerToViewModel(createdCustomer);
            return model;

        }



        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public List<Customer> SearchForCustomers(string firstName, string lastName)
        {
            List<Customer> customers = customerRepo.GetAllCustomers(FirstName, LastName);
            return customers;
        }
    }
}

