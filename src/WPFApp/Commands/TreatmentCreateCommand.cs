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
    // Nedarvning fra interfacet ICommand
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


        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til tcvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            // Variablen sættes i første omgang til false
            bool result = false;

            // Parameter tjekkes
            if (parameter is TreatmentCreateViewModel tcvm)
            {
                // Det tjekkes, om alle nødvendige tekstbokse er udfyldt
                if (tcvm.Type != null && tcvm.Name != null && tcvm.Description != null
                    && tcvm.Price != null)
                {
                    // Så sættes variblen til true;
                    result = true;
                }
                // Variablen returneres
                return result;

            }
            // Hvis datacontext ikke er sat korrekt, returneres false
            return false;
        }

        /// <summary>
        /// Metoden, der udfører opret_behandling_funktionen og får den add'et til repository.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til tcvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            // Parameter tjekkes
            if (parameter is TreatmentCreateViewModel tcvm)
            {
                // TreatmentRepo instantiers
                TreatmentRepository treatmentRepo = new TreatmentRepository();

                // Repo add'er product i db
                treatmentRepo.AddTreatment(Treatment.CreateTreatmentFromUI(tcvm.Type, tcvm.Name, tcvm.Description, tcvm.Price));
                MessageBox.Show("Behandling oprettet");           
            }
            else throw new Exception("Wrong type of paratemer");
        }       
    }
}
