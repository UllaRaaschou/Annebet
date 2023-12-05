using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    class UpdateCustomerCommand : ICommand
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
            bool result = false;

            if (parameter is CustomerViewModel cvm)
            {
                if (cvm.FirstName != null && cvm.LastName != null)
                {
                    result = true;
                }
                return result;
            }

            return false;
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
