using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    class CustomerSearchToDeleteCommand : ICommand
    {
        /// <summary>
        /// CanExecuteChanged-eventet har fået add'et et RequerySuggested-event. 
        /// Requery udløses så snart WPF mener, at command properties skal re-evalueres - ofte sfa bruger-acts.
        /// Dette trigger så CanExecuteChanged-eventet og dermed en re-evaluering af CanExecute-metoden, som så måske
        /// bliver true, hvorved knap bliver enabled og Execute-metoden kan udføres
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object? parameter)
        {
          
            bool result = false;

            if (parameter is CustomerDeleteViewModel cdvm)
            {
                if (cdvm.FirstName != null && cdvm.LastName != null)
                {
                    result = true;
                }
                return result;
            }

            return result;
        }

        public void Execute(object? parameter)
        {
            addCustomersFromDbToList(parameter);
        }

        public void addCustomersFromDbToList(object? parameter) 
        {
            if (parameter is CustomerDeleteViewModel cdvm) 
            {
                CustomerRepository customerRepo = new CustomerRepository();
                List<Customer> customerList = customerRepo.GetAllCustomers(cdvm.FirstName, cdvm.LastName);
                foreach (Customer customer in customerList)
                {
                    CustomerSearchViewModel csvm = new CustomerSearchViewModel(customer);
                    cdvm.SearchedCustomers.Add(csvm);                    
                }
                
            }  
            else throw new Exception("Wrong type of parameter");
        }
    }
}
