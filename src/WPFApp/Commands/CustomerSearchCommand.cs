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
    public class CustomerSearchCommand : ICommand // Nedarvning fra interfacet ICommand
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



        private ICustomerRepository repository;  // simpel deklarering af repo. Dette kan skiftes afhængigt af den anvendte konstructor

        public CustomerSearchCommand() // Constructor, der som default vil blive aktiveret og som sætter repo-feltet til det almindelige CustomerReposity
        {
            this.repository = new CustomerRepository();
        }

        public CustomerSearchCommand(ICustomerRepository repository) // Constuctor, der kan bruges, når vi i unit-test bruger Test-repo som parameter,
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
            bool result = false;  // result sættes til false fra start

            if (parameter is CustomerUpdateViewModel cuvm) // Hvis datacontekst er cuvm
            {
                if (cuvm.FirstName != null && cuvm.LastName != null) // kontrol for, om tekstbokse til søgning er udfyldt
                {
                    result = true;
                }
                return result; // true result returneres
            }
            
            if (parameter is CustomerDeleteViewModel cdvm) // Hvis datecontext er cdvm
            {
                if (cdvm.FirstName != null && cdvm.LastName != null) // kontrol for, om tekstbokse til søgning er udfyldt
                {
                    result = true;
                }
                return result; // true result returneres
            }
            return false;
        }


        public void Execute(object? parameter)
        {
            if (parameter is CustomerUpdateViewModel cuvm) // Hvis datekontekst er cuvm
            {
                CustomerToViewModel ctvm = new CustomerToViewModel(); // Customer-til-ViemModel-Converter instantieres
                List<Customer> trueCustomers = repository.GetAllCustomers(cuvm.FirstName, cuvm.LastName); // Dens CustomerRepo henter Customers ud fra kriterier
                foreach (Customer c in trueCustomers)
                {
                    CustomerToViewModel customerViewModel = ctvm.CustomerToViewModelConvert(c); // De hentede Customers converteres til ViewModels
                    cuvm.CustomersToView.Add(customerViewModel); // viewModels lægges i Observable Collection i dataontexten
                }
            }

            if (parameter is CustomerDeleteViewModel cdvm) // Hvis datekontekst er cdvm
            {
                CustomerToViewModel ctvm = new CustomerToViewModel(); // Customer-til-ViemModel-Converter instantieres
                List<Customer> trueCustomers = repository.GetAllCustomers(cdvm.FirstName, cdvm.LastName); // CustomerRepo henter Customers ud fra kriterier
                foreach (Customer c in trueCustomers)
                {
                    CustomerToViewModel customerViewModel = ctvm.CustomerToViewModelConvert(c);// De hentede Customers converteres til ViewModels
                    cdvm.CustomersToView.Add(customerViewModel); // viewModels lægges i Observable Collection i dataontexten
                }
            }
            else throw new Exception("Wrong type of parameter");
        }
    }
}
