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
    class CustomerSearchToUpdateCommand : ICommand
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


            if (parameter is CustomerUpdateViewModel cuvm)
            
            {
               if (cuvm.FirstName != null && cuvm.LastName != null)
                {
                    result = true;
                }
                return result;
            }
            return false;

         
        }

        public void Execute(object? parameter)
        {
            addCustomersFromDbToList(parameter); // Metoden ligger nedenfor
        }

        public void addCustomersFromDbToList(object? parameter) // tager et parameter, som grundet vores binding i Xaml, skal være Xaml's
                                                                // datakontekst, som i dette tilfælde er cuvm
        {
            if (parameter is CustomerUpdateViewModel cuvm) 
            {
                CustomerRepository customerRepo = new CustomerRepository();
                List<Customer> customerList = customerRepo.GetAllCustomersFromFirstNameAndLastName(cuvm.FirstName, cuvm.LastName); //Fremsøger kunder
                foreach (Customer customer in customerList)
                {
                    CustomerSearchViewModel csvm = new CustomerSearchViewModel(customer); //Wrapper customers ind i en viewmodel 
                    cuvm.SearchedCustomers.Add(csvm);         // add'er de nu wrappede customers i en observable collection i cuvm           
                }
                
            }  
            else throw new Exception("Wrong type of parameter");
        }
    }
}
