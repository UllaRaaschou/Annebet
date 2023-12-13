using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    public class CustomerSearchCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is CustomerUpdateViewModel cuvm)
            {
                CustomerToViewModel ctvm = new CustomerToViewModel();
                List<Customer> trueCustomers = ctvm.customerRepo.GetAllCustomers(cuvm.FirstName, cuvm.LastName);
                foreach (Customer c in trueCustomers)
                {
                    CustomerToViewModel customerViewModel = ctvm.CustomerToViewModelConvert(c);
                    cuvm.CustomersToView.Add(customerViewModel);
                }
            }
        }
    }
}
