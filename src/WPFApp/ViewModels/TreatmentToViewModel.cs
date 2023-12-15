using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models;

namespace WPFApp.ViewModels
{
    public class TreatmentToViewModel
    {
        // treatmentRepo deklareres
        public TreatmentRepository treatmentRepo;


        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }


        // parameterløs constructor, der instantierer productRepo
        public TreatmentToViewModel()
        {
            treatmentRepo = new TreatmentRepository();
        }

        // constructor, der tager Treatment som parameter
        public TreatmentToViewModel(Treatment treatment)
        {
            Id = treatment.Id;
            Type = treatment.Type;
            Name = treatment.Name;
            Description = treatment.Description;
            Price = treatment.Price;
        }


        /// Metode, der converter et object af typen Treatment til et object af typen TreatmentToViewModel
        public TreatmentToViewModel TreatmentToViewModelConvert(Treatment treatment)
        {
            int id = treatment.Id;
            string type = treatment.Type;
            string name = treatment.Name;
            string description = treatment.Description;
            decimal price = treatment.Price;
            Treatment createdTreatment = Treatment.CreateTreatmentFromDb(id, type, name, description, price);
            TreatmentToViewModel model = new TreatmentToViewModel(createdTreatment);
            return model;
        }


        /// Metode, der via repo henter ønskede treatments i db
        public List<Treatment> SearchForTreatment(string type, string name)
        {
            List<Treatment> treatments = treatmentRepo.GetAllTreatments(type, name);
            return treatments;
        }
    }
}
