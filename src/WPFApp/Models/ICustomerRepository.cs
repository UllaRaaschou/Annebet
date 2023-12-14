using System.Collections.Generic;

namespace WPFApp.Models
{
    public interface ICustomerRepository
    {
        int AddCustomer(Customer customer);
        List<Customer> GetAllCustomers(string firstName, string lastName);
        void UpdateCustomer(Customer customerWithUpdatedValues, int id);

        void DeleteCustomerById(int id);
    }
}