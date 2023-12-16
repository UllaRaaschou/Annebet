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

        private ICustomerRepository repository;

        public CustomerUpdateCommand() 
        {
            this.repository = new CustomerRepository();
        }

        public CustomerUpdateCommand(ICustomerRepository repository) 
        {
            this.repository = repository;
        }



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
                if (repository is CustomerRepository)
                {
                    MessageBox.Show("Kunde opdateret");
                }


            }
        }
    }
}
