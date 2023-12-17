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
    public class CustomerUpdateCommand : ICommand // Nedarvning fra interfacet ICommand
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



        private ICustomerRepository repository; // simpel deklarering af repo. Dette kan skiftes afhængigt af den anvendte konstructor

        public CustomerUpdateCommand()  // Constructor, der som default vil blive aktiveret og som sætter repo-feltet til det almindelige CustomerReposity
        {
            this.repository = new CustomerRepository();
        }

        public CustomerUpdateCommand(ICustomerRepository repository)   // Constuctor, der kan bruges, når vi i unit-test bruger Test-repo som parameter,
        {                                                              // og vi dermed sætter repo-feltet til test-Repo
            this.repository = repository;
        }


        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til cuvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            bool result = false;  // parameteren sættes i første omgang til false

            if (parameter is CustomerUpdateViewModel cuvm) // tjek af parameter som datekontext
            {
                if (cuvm.SelectedCustomer != null) // tjek af, at der er valgt et selected item
                {
                    if (cuvm.SelectedCustomer.FirstName != null && cuvm.SelectedCustomer.LastName != null // tjek af, om selected items properties alle er sat
                        && cuvm.SelectedCustomer.Address != null && cuvm.SelectedCustomer.Phone != null && cuvm.SelectedCustomer.Email != null)
                    {
                        result = true; // hvis ja, sættes parameter til true
                    }
                }
                return result; // parameter returneres
            }
            return false; // hvis parametertjek fejler, returneres false
        }


        /// <summary>
        /// Metoden, der udfører opdater_kunde_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til cuvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            if(parameter is CustomerUpdateViewModel cuvm)  // kontrol af, om datakonteksten er cuvm                                                            // og om datakontekten er kommet med som parameter
            {                              
                Customer updatedCustomer = Customer.CreateCustomerFromDb(cuvm.SelectedCustomer.Id, // Den statiske Customer-metode laver en updated Customer
                    cuvm.SelectedCustomer.FirstName, cuvm.SelectedCustomer.LastName,
                    cuvm.SelectedCustomer.Address, cuvm.SelectedCustomer.Phone, cuvm.SelectedCustomer.Email);
                repository.UpdateCustomer(updatedCustomer);     // Repo opdaterer Customer                              

                cuvm.SelectedCustomer = null; // Tekstboksenes indhold nulstilles
                cuvm.FirstName = null;
                cuvm.LastName = null;
                cuvm.CustomersToView.Clear();

                if (repository is CustomerRepository)
                {
                    MessageBox.Show("Kunde opdateret");

                }

            }
            else throw new Exception("Wrong type of parameter");
        }
    }
}
