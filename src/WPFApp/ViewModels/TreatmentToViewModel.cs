using WPFApp.Models;

namespace WPFApp.ViewModels
{
    public class TreatmentToViewModel
    {       
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }


        // parameterløs constructor
        public TreatmentToViewModel() { }
       

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
    }
}
