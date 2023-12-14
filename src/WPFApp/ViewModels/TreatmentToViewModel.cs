﻿using System;
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
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public TreatmentRepository treatmentRepo;

        public TreatmentToViewModel()
        {
            treatmentRepo = new TreatmentRepository();
        }
        public TreatmentToViewModel(Treatment treatment)
        {
            Id = treatment.Id;
            Type = treatment.Type;
            Name = treatment.Name;
            Description = treatment.Description;
            Price = treatment.Price;
        }

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

        public List<Treatment> SearchForTreatment(string type, string name)
        {
            List<Treatment> treatments = treatmentRepo.GetAllTreatments(type, name);
            return treatments;
        }
    }
}
