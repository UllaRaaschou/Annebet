using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    internal class TreatmentCreateCommand : ICommand
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

        /// <summary>
        /// Metoden, der udfører opret_behandling_funktionen og får den add'et til repository.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til pcvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            if (parameter is TreatmentCreateViewModel tcvm)
            {
                var treatmentRepo = new TreatmentRepository();
                treatmentRepo.AddTreatment(Treatment.CreateTreatmentFromUI(tcvm.Type, tcvm.Name, tcvm.Description, tcvm.Price));
                MessageBox.Show("Behandling oprettet");           
            }
            else throw new Exception("Wrong type of paratemer");
        }       
    }
}
