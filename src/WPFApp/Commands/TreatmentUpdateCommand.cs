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
    public class TreatmentUpdateCommand : ICommand
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
                var updatedTreatment = Treatment.CreateTreatmentFromDb(tuvm.SelectedTreatment.Id, tuvm.SelectedTreatment.Type, tuvm.SelectedTreatment.Name,
                    tuvm.SelectedTreatment.Description, tuvm.SelectedTreatment.Price);
                var treatmentRepo = new TreatmentRepository();
                treatmentRepo.UpdateTreatment(updatedTreatment);
                MessageBox.Show("Behandling er opdateret");
            }
            else throw new Exception("Error");
        }
    }
}
