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
    public class CustomerCreateCommand : ICommand  // Nedarvning fra interfacet ICommand
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



        private ICustomerRepository repository; // simpel deklarering af repo. Dette kan skiftes afhængigt af den anvendte konstructor
        public CustomerCreateCommand()  // Constructor, der som default vil blive aktiveret og som sætter repo-feltet til det almindelige CustomerReposity
        {
            this.repository = new CustomerRepository();
        }
        public CustomerCreateCommand(ICustomerRepository repository)  // Constuctor, der kan bruges, når vi i unit-test bruger Test-repo som parameter,
                                                                      // og vi dermed sætter repo-feltet til test-Repo
        {
            this.repository = repository;
        }

        

        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til ccvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            bool result = false;  // variablen sættes i første omgang til false

            if (parameter is CustomerCreateViewModel ccvm) // parameter tjekkes 
            {
                if (ccvm.FirstName != null && ccvm.LastName != null && ccvm.Address != null // Det tjekkes, om alle nødvendige tekstbokse er udfyldt
                    && ccvm.Phone != null && ccvm.Email != null)
                {
                    result = true; // så sættes variblen til true;
                }
                return result; // variablen returneres
            }
            return false; // hvis datacontext ikke er sat korrekt, returneres false            
        }

       

        /// <summary>
        /// Metoden, der udfører opret_kunde_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til ccvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            if (parameter is CustomerCreateViewModel ccvm)  // parameter tjekkes
            {               
                repository.AddCustomer(Customer.CreateCustomerFromUI(ccvm.FirstName, ccvm.LastName, ccvm.Address, ccvm.Phone, ccvm.Email)); // Repo add'er Customer til db
                ccvm.FirstName = null; // Tekstboksenes indhold nulstilles
                ccvm.LastName = null;
                ccvm.Address = null;
                ccvm.Phone = null;
                ccvm.Email = null;

                ccvm.FirstName = null;
                ccvm.LastName = null;

                if(repository is CustomerRepository)
                {
                    MessageBox.Show("Kunde oprettet");
                }                               
            }
            else throw new Exception("Wrong type of paratemer");
       }
    }
}
