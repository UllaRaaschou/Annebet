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
    public class TreatmentSearchCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is TreatmentUpdateViewModel tuvm)
            {
                var ttvm = new TreatmentToViewModel();
                List<Treatment> trueTreatments = ttvm.treatmentRepo.GetAllTreatments(tuvm.Type, tuvm.Name);
                foreach (Treatment t in trueTreatments)
                {
                    var treatmentViewModel = ttvm.TreatmentToViewModelConvert(t);
                    tuvm.TreatmentsToView.Add(treatmentViewModel);
                }
            }
            else if (parameter is TreatmentDeleteViewModel tdvm)
            {
                var ttvm = new TreatmentToViewModel();
                List<Treatment> trueTreatments = ttvm.treatmentRepo.GetAllTreatments(tdvm.Type, tdvm.Name);
                foreach (Treatment t in trueTreatments)
                {
                    var treatmentViewModel = ttvm.TreatmentToViewModelConvert(t);
                    tdvm.TreatmentsToView.Add(treatmentViewModel);
                }
            }
            else throw new Exception("Error");
        }
    }
}
