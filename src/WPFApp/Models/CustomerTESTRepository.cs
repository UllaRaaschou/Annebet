using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public class CustomerTESTRepository : ICustomerRepository
    {
        List<Customer> customers = new List<Customer>();
        
        public int AddCustomer(Customer customer)
        {
            int idValue = (customers.Count + 1);
            Customer customerWithId  = Customer.CreateCustomerFromDb(idValue, customer.FirstName, customer.LastName, customer.Address, customer.Phone, customer.Email);
            customers.Add(customerWithId);
            return idValue;
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

        public void UpdateCustomer(Customer customerWithUpdatedValues, int id)
        {
            foreach (Customer customer in customers) 
            {
                if(customer.Id == id) 
                {
                    customers.Remove(customer);
                    customers.Add(Customer.CreateCustomerFromDb(customerWithUpdatedValues.Id, customerWithUpdatedValues.FirstName, 
                        customerWithUpdatedValues.LastName, customerWithUpdatedValues.Address, customerWithUpdatedValues.Phone,
                        customerWithUpdatedValues.Email));
                }
            }
        }
        public void DeleteCustomerById(int id)
        {
            foreach(Customer customer in customers) 
            {
                if(customer.Id == id) 
                {
                    customers.Remove(customer);
                }
            }
        }
    }
}
