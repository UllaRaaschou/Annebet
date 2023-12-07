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

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter is CustomerUpdateViewModel cuvm)  // kontrol af, om datakonteksten er cuvm (sat som code-behind i xaml)
                                                           // og om datakontekten er kommet med som parameter
            {
               
                Customer customerWithUpdatedValues = Customer.MakeNewCustomerFromUI(  //opdateret kundeinstans laves ud fra opdaterede
                                                                                      //værdier (som User har tastet på selected item)
                                                                                      
                    cuvm.SelectedCustomer.FirstName, 
                    cuvm.SelectedCustomer.LastName,
                    cuvm.SelectedCustomer.Address, 
                    cuvm.SelectedCustomer.Phone, 
                    cuvm.SelectedCustomer.Email
                    );

                int id = cuvm.SelectedCustomer.Id; // id'et er id'et på selected item
                CustomerRepository customerRepo = new CustomerRepository();
                
                customerRepo.UpdateCustomer(customerWithUpdatedValues, id);  // den opdaterede cutomer gemmes i db under sit oprindelige id

                cuvm.SelectedCustomer.FirstName = null;
                cuvm.SelectedCustomer.LastName = null;
                cuvm.SelectedCustomer.Address = null;
                cuvm.SelectedCustomer.Phone = null;
                cuvm.SelectedCustomer.Email = null;

                cuvm.FirstName = null;
                cuvm.LastName = null;
                MessageBox.Show("Kunde opdateret");


            }
        }
    }
}
