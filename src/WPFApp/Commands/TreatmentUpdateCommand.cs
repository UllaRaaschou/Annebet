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


        private ITreatmentRepository repository; // simpel deklarering af repo. Dette kan skiftes afhængigt af den anvendte konstructor
        public TreatmentUpdateCommand()  // Constructor, der som default vil blive aktiveret og som sætter repo-feltet til det almindelige CustomerReposity
        {
            this.repository = new TreatmentRepository();
        }
        public TreatmentUpdateCommand(ITreatmentRepository repository)  // Constuctor, der kan bruges, når vi i unit-test bruger Test-repo som parameter,
                                                                        // og vi dermed sætter repo-feltet til test-Repo
        {
            this.repository = repository;
        }


        public bool CanExecute(object? parameter)
        {
            bool result = false;  // parameteren sættes i første omgang til false

            if (parameter is TreatmentUpdateViewModel tuvm) // tjek af parameter som datekontext
            {
                if (tuvm.SelectedTreatment != null) // tjek af, at der er valgt et selected item
                {
                    if (tuvm.SelectedTreatment.Type != null && tuvm.SelectedTreatment.Name != null // tjek af, om selected items properties alle er sat
                        && tuvm.SelectedTreatment.Description != null && tuvm.SelectedTreatment.Price != null)
                    {
                        result = true; // hvis ja, sættes parameter til true
                    }
                }
                return result; // parameter returneres
            }
            return false; // hvis parametertjek fejler, returneres false
        }


        /// <summary>
        /// Metoden, der udfører opdater_behandling_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til tuvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            // Parameter tjekkes
            if (parameter is TreatmentUpdateViewModel tuvm)
            {
                // Den statiske Treatment-metode laver en updated Treatment
                Treatment updatedTreatment = Treatment.CreateTreatmentFromDb(tuvm.SelectedTreatment.Id, tuvm.SelectedTreatment.Type, tuvm.SelectedTreatment.Name,
                    tuvm.SelectedTreatment.Description, tuvm.SelectedTreatment.Price);

                // Repo opdaterer Treatment
                repository.UpdateTreatment(updatedTreatment);
                tuvm.SelectedTreatment.Id = 0;
                tuvm.SelectedTreatment.Type = null!;
                tuvm.SelectedTreatment.Name = null!;
                tuvm.SelectedTreatment.Description = null!;
                tuvm.SelectedTreatment.Price = 0m;

                MessageBox.Show("Behandling er opdateret");
            }
            else throw new Exception("Error");
        }
    }
}
