using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using WPFApp.Models;
using WPFApp.ViewModels;
using WPFApp.Views;

namespace WPFApp.Commands
{
    class CustomerCreateCommand : ICommand
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

            if (parameter is CustomerCreateViewModel ccvm)
            {
                if (ccvm.FirstName != null && ccvm.LastName != null && ccvm.Address != null && ccvm.Phone != null && ccvm.Email != null)
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
            if (parameter is CustomerCreateViewModel ccvm)
            {
                CustomerRepository customerRepo = new CustomerRepository();
                customerRepo.AddCustomer(Customer.MakeNewCustomerFromUI(ccvm.FirstName, ccvm.LastName, ccvm.Address, ccvm.Phone, ccvm.Email));
                ccvm.FirstName = null;
                ccvm.LastName = null;
                ccvm.Address = null;
                ccvm.Phone = null;
                ccvm.Email = null;
            }
            else throw new Exception("Wrong type of paratemer");

            MessageBox.Show("Kunde oprettet");
            


        }
    }
}
