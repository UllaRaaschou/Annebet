using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    // Nedarvning fra interfacet ICommand
    public class CustomerDeleteCommand : ICommand
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


        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til cdvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            // Variablen sættes i første omgang til false
            bool result = false;

            // Tjek af, om datakontekst er den rette
            if (parameter is CustomerDeleteViewModel cdvm) 
            {
                // Tjek af, at der er valgt en customer i listbox
                if (cdvm.SelectedCustomer != null) 
                {
                    // Tjek af, at alle selected Customers properties er udfyldt
                    if (cdvm.SelectedCustomer.FirstName != null && cdvm.SelectedCustomer.LastName != null &&
                        cdvm.SelectedCustomer.Address != null && cdvm.SelectedCustomer.Phone != null && cdvm.SelectedCustomer.Email != null)
                    {
                        // Hvis ok, sættes variabel til  true
                        result = true; 
                    }
                }
                //Variabel returneres
                return result; 
            }
            // Hvis parameter test fejler, returenes false
            return false; 
        }


        /// <summary>
        /// Selve commandens "execute-metode".
        /// </summary>
        public void Execute(object? parameter)
        {
            // Det tjekkes, om datakontekst er kommet med som paramter 
            if (parameter is CustomerDeleteViewModel cdvm)                                                     
            {
                // En variabel sættes til selected customers id
                int id = cdvm.SelectedCustomer.Id;

                // Ny instans af CustomerRepository
                CustomerRepository customerRepo = new CustomerRepository();

                // CustomerRepo sletter selected Customer
                customerRepo.DeleteCustomerById(id);
                MessageBox.Show("Kunde slettet");
            }
            else throw new Exception("Wrong type of parameter");
        }
    }
}
