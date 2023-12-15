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
    // Nedarvning fra interfacet ICommand
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
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til ccvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            // Variablen sættes i første omgang til false
            bool result = false;

            // Parameter tjekkes
            if (parameter is CustomerCreateViewModel ccvm)  
            {
                // Det tjekkes, om alle nødvendige tekstbokse er udfyldt
                if (ccvm.FirstName != null && ccvm.LastName != null && ccvm.Address != null 
                    && ccvm.Phone != null && ccvm.Email != null)
                {
                    // Så sættes variblen til true;
                    result = true; 
                }
                // Variablen returneres
                return result; 
            }
            // Hvis datacontext ikke er sat korrekt, returneres false
            return false;             
        }

       
        /// <summary>
        /// Metoden, der udfører opret_kunde_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til ccvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            // Parameter tjekkes
            if (parameter is CustomerCreateViewModel ccvm) 
            {
                // CustomerRepo instantieres
                CustomerRepository customerRepo = new CustomerRepository();

                // Repo add'er Customer til db
                customerRepo.AddCustomer(Customer.CreateCustomerFromUI(ccvm.FirstName, ccvm.LastName, ccvm.Address, ccvm.Phone, ccvm.Email));
                MessageBox.Show("Kunde oprettet");
            }
            else throw new Exception("Wrong type of paratemer");           
        }
    }
}
