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
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til cvm.
        /// </summary>

        public bool CanExecute(object? parameter)
        {
            
            bool result = false;

            if (parameter is CustomerDeleteViewModel cdvm)
            {
                if(cdvm.SelectedCustomer != null) 
                {
                    if (cdvm.SelectedCustomer.FirstName != null && cdvm.SelectedCustomer.LastName != null && cdvm.SelectedCustomer.Address != null && cdvm.SelectedCustomer.Phone != null && cdvm.SelectedCustomer.Email != null)
                    {
                        result = true;
                    }
                }
                
                return result;

            }

            return false;

        }

        /// <summary>
        /// 
        /// </summary>

        public void Execute(object? parameter)
        {
            if (parameter is CustomerDeleteViewModel cdvm) //det tjekkes, om datakontekst (sat i xaml.cs) er kommet med
                                                            // som parameter
            {
                int id = cdvm.SelectedCustomer.Id;  // selected item har et id
                CustomerRepository customerRepo = new CustomerRepository();
                customerRepo.DeleteCustomerById(id);
                
                cdvm.SelectedCustomer.FirstName = null;
                cdvm.SelectedCustomer.LastName = null;
                cdvm.SelectedCustomer.Address = null;
                cdvm.SelectedCustomer.Phone = null;
                cdvm.SelectedCustomer.Email = null;

                cdvm.FirstName = null;
                cdvm.LastName = null;
            }
            else throw new Exception("Wrong type of paratemer");

        }
    }
}
