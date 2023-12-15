using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public class CustomerTESTRepository : ICustomerRepository
    {
        public List<Customer> customers { get; } = new List<Customer>();  // privat liste erstatter db
        
        public int AddCustomer(Customer customer)
        {
            int id = (customers.Count + 1);
            Customer customerWithId  = Customer.CreateCustomerFromDb(id, customer.FirstName, customer.LastName, customer.Address, customer.Phone, customer.Email);
            customers.Add(customerWithId);
            return id;
        }
        public void UpdateCustomer(Customer customerWithUpdatedValues)
        {
            Customer customer2 = null;

            foreach (Customer customer in customers)
            {
                if (customer.Id == customerWithUpdatedValues.Id)
                {
                    customer2 = customer;
                    break;
                    
                }
                
            }
            if (customer2 != null)
            {
                customers.Remove(customer2);
                customers.Add(Customer.CreateCustomerFromDb(customerWithUpdatedValues.Id, customerWithUpdatedValues.FirstName,
                    customerWithUpdatedValues.LastName, customerWithUpdatedValues.Address, customerWithUpdatedValues.Phone,
                    customerWithUpdatedValues.Email));

            }
        }
        public void DeleteCustomerById(int id)
        {
            Customer customer2 = null; 
            foreach (Customer customer in customers)
            {
                if (customer.Id == id)
                {
                    customer2 = customer;
                    break;
                }
            }
            if (customer2 != null)
            {
                customers.Remove(customer2);
            }
        }


        public List<Customer> GetAllCustomers(string firstName, string lastName)
        {
            List <Customer> wantedCustomers = new List<Customer>(); 
            foreach (Customer customer in customers) 
            {
                if(firstName == customer.FirstName && lastName == customer.LastName)
                {
                    wantedCustomers.Add(customer);
                }
            }return wantedCustomers;    
        }

        
        
    }
}
