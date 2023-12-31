﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Commands
{
    // Nedarvning fra interfacet ICommand
    public class TreatmentSearchCommand : ICommand
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



        public TreatmentSearchCommand() // Constructor, der som default vil blive aktiveret og som sætter repo-feltet til det almindelige TreatmentReposity
        {
            this.repository = new TreatmentRepository();
        }

        public TreatmentSearchCommand(ITreatmentRepository repository) // Constuctor, der kan bruges, når vi i unit-test bruger Test-repo som parameter,
        {                                                              // og vi dermed sætter repo-feltet til test-Repo
            this.repository = repository;
        }



        /// <summary>
        /// Metode, der undersøger, om Execute skal afvikles.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til tuvm eller tdvm.
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            // Variablen sættes i første omgang til false
            bool result = false;

            // Parameter tjekkes
            if (parameter is TreatmentUpdateViewModel tuvm)
            {
                // Det tjekkes, om alle nødvendige tekstbokse er udfyldt
                if (tuvm.Type != null && tuvm.Name != null) // kontrol for, om tekstbokse til søgning er udfyldt
                {
                    // Så sættes variblen til true;
                    result = true;
                }
                // Variablen returneres
                return result;
            }

            if (parameter is TreatmentDeleteViewModel tdvm) // Hvis datecontext er pdvm
            {
                // Parameter tjekkes
                if (tdvm.Type != null && tdvm.Name != null)
                {
                    // Så sættes variblen til true;
                    result = true;
                }
                // Variablen returneres
                return result;
            }
            return false;
        }


        /// <summary>
        /// Metoden, der udfører søg_behandling_funktionen og får den add'et til database.
        /// Parameteren er i xaml-koden sat som "CommandParameter = Binding", og datakontekst er i code behind sat til tuvm eller tdvm.
        /// </summary>
        public void Execute(object? parameter)
        {
            // Parameter tjekkes
            if (parameter is TreatmentUpdateViewModel tuvm)
            {
                tuvm.TreatmentsToView.Clear(); // Observable Collection tømmes for eventuelle items

                // Dens repo henter ønskede produkter
                List<Treatment> trueTreatments = repository.GetAllTreatments(tuvm.Type, tuvm.Name);

                // Treatment-to-Viewodel-Converter instantieres
                var ttvm = new TreatmentToViewModel();

                foreach (Treatment t in trueTreatments)
                {
                    // De hentede Treatments converteres til ViewModels
                    TreatmentToViewModel treatmentViewModel = ttvm.TreatmentToViewModelConvert(t);

                    // ViewModels lægger i Observable Collection i tuvm
                    tuvm.TreatmentsToView.Add(treatmentViewModel);
                }
            }
            // Parameter tjekkes
            else if (parameter is TreatmentDeleteViewModel tdvm)
            {
                tdvm.TreatmentsToView.Clear(); // Observable Collection tømmes for eventuelle items

                // Treatment-to-Viewodel-Converter instantieres
                TreatmentToViewModel ttvm = new TreatmentToViewModel();

                // Dens repo henter ønskede treatments
                List<Treatment> trueTreatments = repository.GetAllTreatments(tdvm.Type, tdvm.Name);

                foreach (Treatment t in trueTreatments)
                {
                    // De hentede treatments omdannes til ViewModels
                    TreatmentToViewModel treatmentViewModel = ttvm.TreatmentToViewModelConvert(t);

                    // ViewModels lægger i Observable Collection i pdvm
                    tdvm.TreatmentsToView.Add(treatmentViewModel);
                }
            }
            
        }
    }
}