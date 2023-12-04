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
    class ConstructCustomerCommand : ICommand
    {
       
        /// <summary>
        /// CanExecuteChanged-eventet har fået add'et et RequerySuggested-event. 
        /// Requery udløses så snart WPF mener, at command properties skal re-evalueres - ofte sfa bruger-acts.
        /// Dette trigger så CanExecuteChanged-eventet og dermed en re-evaluering af CanExecute-metoden, som så måske
        /// bliver true, hvorved knap bliver enabled og Execute-metoden kan udføres
        /// </summary>
        public event EventHandler? CanExecuteChanged 
        {
            add {CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value;}
        }

        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til cvm.
        /// </summary>

        public bool CanExecute(object? parameter)
        {
            bool result = false;

            if (parameter is CustomerViewModel cvm)
            {
                if (cvm.FirstName != null && cvm.LastName != null && cvm.Address != null && cvm.Phone != null && cvm.Email != null)
                {
                    result = true;
                }
                return result;

            }

            return false;
            
        }

        /// <summary>
        /// Metoden, der udfører opret_kunde_funktionen og får den add'et til repository.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til cvm.
        /// </summary>
      
        public void Execute(object? parameter)
        {
            if (parameter is CustomerViewModel cvm)
            {
                CustomerRepository customerRepo = new CustomerRepository();
                customerRepo.AddCustomer(Customer.MakeNewCustomerFromUI(cvm.FirstName, cvm.LastName, cvm.Address, cvm.Phone, cvm.Email));
                cvm.FirstName = null;
                cvm.LastName = null;
                cvm.Address = null;
                cvm.Phone = null;
                cvm.Email = null;
            }
            else throw new Exception("Wrong type of paratemer");
            

        }
    }
}
