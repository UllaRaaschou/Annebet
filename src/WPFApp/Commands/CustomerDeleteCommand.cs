using System;
using System.Windows;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    public class CustomerDeleteCommand : ICommand // Nedarvning fra interfacet ICommand
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
     
        

        public CustomerDeleteCommand()  // Constructor, der som default vil blive aktiveret og som sætter repo-feltet til det almindelige CustomerReposity
        {
            this.repository = new CustomerRepository();
        }

        public CustomerDeleteCommand(ICustomerRepository repository)  // Constuctor, der kan bruges, når vi i unit-test bruger Test-repo som parameter,
                                                                      // og vi dermed sætter repo-feltet til test-Repo
        {
            this.repository = repository;
        }



        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til cdvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            bool result = false; // variablen sættes i første omgang til false

            if (parameter is CustomerDeleteViewModel cdvm) // tjek af, om datakontekst er den rette
            {
                if (cdvm.SelectedCustomer != null) // tjek af, at der er valgt en customer i listbox
                {
                    if (cdvm.SelectedCustomer.FirstName != null && cdvm.SelectedCustomer.LastName != null && // tjek af, at alle selected Customers properties er udfyldt
                        cdvm.SelectedCustomer.Address != null && cdvm.SelectedCustomer.Phone != null && cdvm.SelectedCustomer.Email != null)
                    {
                        result = true; // hvis ok, sættes variabel til  true
                    }
                }
                return result; //variabel returneres
            }   
            return false; // hvis parameter test fejler, returenes false
        }



        /// <summary>
        /// Metoden, der udfører slet_kunde_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til cdvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            MessageBoxResult choise = MessageBox.Show("Er du sikker?", "Bekræft", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (choise == MessageBoxResult.Yes)
            {
                if (parameter is CustomerDeleteViewModel cdvm) //det tjekkes, om datakontekst er kommet med som paramter                                                           // som parameter
                {
                    int id = cdvm.SelectedCustomer.Id;  // en variabel sættes til selected customers id

                    repository.DeleteCustomerById(id); //customerRepo sletter selected Customer

                    cdvm.SelectedCustomer = null;  //Tekstboksenes værdier nulstilles

                    cdvm.FirstName = null;
                    cdvm.LastName = null;
                    cdvm.CustomersToView.Clear();

                    if (repository is CustomerRepository)
                    {
                        MessageBox.Show("Kunde slettet");
                    }
                }
                else throw new Exception("Wrong type of parameter");
            }
            else { }
        }
    }
}
