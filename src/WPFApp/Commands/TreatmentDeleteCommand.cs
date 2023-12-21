using System;
using System.Windows;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    // Nedarvning fra interfacet ICommand
    public class TreatmentDeleteCommand : ICommand
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



        private ITreatmentRepository repository; // Simpel deklarering af repo. Dette kan skiftes afhængigt af den anvendte konstructor



        public TreatmentDeleteCommand()  // Constructor, der som default vil blive aktiveret og som sætter repo-feltet til det almindelige CustomerReposity
        {
            this.repository = new TreatmentRepository();
        }
 
        public TreatmentDeleteCommand(ITreatmentRepository repository)  // Constuctor, der kan bruges, når vi i unit-test bruger Test-repo som parameter,
                                                                        // og vi dermed sætter repo-feltet til test-Repo
        {
            this.repository = repository;
        }



        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til tdvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            // Variablen sættes i første omgang til false
            bool result = false;

            // Tjek af, om datakontekst er den rette
            if (parameter is TreatmentDeleteViewModel tdvm)
            {
                // Tjek af, at der er valgt en customer i listbox
                if (tdvm.SelectedTreatment != null)
                {
                    // Tjek af, at alle selected Customers properties er udfyldt
                    if (tdvm.SelectedTreatment.Type != null && tdvm.SelectedTreatment.Name != null &&
                        tdvm.SelectedTreatment.Description != null && tdvm.SelectedTreatment.Price != null)
                    {
                        // Hvis ok, sættes variabel til  true
                        result = true;
                    }
                }
                //Variabel returneres
                return result;
            }
            // Hvis parameter test fejler, returenes false
            return false;
        }


        /// <summary>
        /// Metoden, der udfører slet_behandling_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til tdvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            if (ConfirmTreatmentDeletion() == MessageBoxResult.Yes)
            {
                // Tjek af parameter
                if (parameter is TreatmentDeleteViewModel tdvm)
                {
                    // Repo deleter selected Product fra db
                    repository.DeleteTreatmentById(tdvm.SelectedTreatment.Id);
                    tdvm.Type = null;
                    tdvm.Name = null;

                    tdvm.TreatmentsToView.Clear();

                    if (repository is TreatmentRepository)
                    {
                        MessageBox.Show("Behandling slettet");
                    }
                }
                else throw new Exception("Wrong type of parameter");
            }
        }

        /// <summary>
        /// Metode, der returnerer yes, når kunden har bekræftet sletning.
        /// I unittests returneres altid yes
        /// </summary
        private MessageBoxResult ConfirmTreatmentDeletion()
        {
            if (repository is TreatmentRepository)
            {
                return MessageBox.Show("Er du sikker?", "Bekræft", MessageBoxButton.YesNo, MessageBoxImage.Question);
            }

            return MessageBoxResult.Yes;
        }
    }
}
