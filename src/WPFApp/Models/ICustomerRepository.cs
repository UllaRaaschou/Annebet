using System.Collections.Generic;

namespace WPFApp.Models
{
    public interface ICustomerRepository
    {
       
        int AddCustomer(Customer customer);
        void UpdateCustomer(Customer customerWithUpdatedValues);
        void DeleteCustomerById(int id);
        List<Customer> GetAllCustomers(string firstName, string lastName);
        
       
    }
}