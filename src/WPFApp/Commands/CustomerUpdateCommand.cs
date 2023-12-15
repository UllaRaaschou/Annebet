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
    public class CustomerUpdateCommand : ICommand
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
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til ccvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            // Variablen sættes i første omgang til false
            bool result = false;

            // Parameter tjekkes
            if (parameter is CustomerUpdateViewModel cuvm) 
            {
                // Tjek af, at der er valgt et selected item
                if (cuvm.SelectedCustomer != null) 
                {
                    // Det tjekkes, om alle nødvendige tekstbokse er udfyldt
                    if (cuvm.SelectedCustomer.FirstName != null && cuvm.SelectedCustomer.LastName != null
                        && cuvm.SelectedCustomer.Address != null && cuvm.SelectedCustomer.Phone != null && cuvm.SelectedCustomer.Email != null)
                    {
                        // Så sættes variblen til true;
                        result = true;
                    }
                }
                // Variablen returneres
                return result;
            }
            // Hvis datacontext ikke er sat korrekt, returneres false
            return false;
        }


        /// <summary>
        /// Metoden, der udfører opdater_kunde_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til cuvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            // Parameter tjekkes
            if (parameter is CustomerUpdateViewModel cuvm)                                                            // og om datakontekten er kommet med som parameter
            {
                // Den statiske Customer-metode laver en updated Customer
                Customer updatedCustomer = Customer.CreateCustomerFromDb(cuvm.SelectedCustomer.Id,
                    cuvm.SelectedCustomer.FirstName, cuvm.SelectedCustomer.LastName,
                    cuvm.SelectedCustomer.Address, cuvm.SelectedCustomer.Phone, cuvm.SelectedCustomer.Email);

                // CutomerRepo instantieres
                CustomerRepository customerRepo = new CustomerRepository();

                // Repo opdaterer Customer 
                customerRepo.UpdateCustomer(updatedCustomer);                                  
                MessageBox.Show("Kunde opdateret");
            }
            else throw new Exception("Error");
        }
    }
}
