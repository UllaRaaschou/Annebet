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
    // Nedarvning fra interfacet ICommand
    public class TreatmentUpdateCommand : ICommand
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
        /// Metoden, der udfører opdater_behandling_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til utvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            // Parameter tjekkes
            if (parameter is TreatmentUpdateViewModel tuvm)
            {
                // Den statiske Treatment-metode laver en updated Treatment
                Treatment updatedTreatment = Treatment.CreateTreatmentFromDb(tuvm.SelectedTreatment.Id, tuvm.SelectedTreatment.Type, tuvm.SelectedTreatment.Name,
                    tuvm.SelectedTreatment.Description, tuvm.SelectedTreatment.Price);

                // TreatmentRepo instantieres
                TreatmentRepository treatmentRepo = new TreatmentRepository();

                // Repo opdaterer Treatment
                treatmentRepo.UpdateTreatment(updatedTreatment);
                MessageBox.Show("Behandling er opdateret");
            }
            else throw new Exception("Error");
        }
    }
}
