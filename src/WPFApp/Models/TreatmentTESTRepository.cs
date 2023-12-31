﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public class TreatmentTESTRepository : ITreatmentRepository
    {
        public List<Treatment> treatments = new List<Treatment>();
        public int AddTreatment(Treatment treatment)
        {
            int idValue = (treatments.Count + 1);
            Treatment treatmentWithId = Treatment.CreateTreatmentFromDb(idValue, treatment.Type, treatment.Name,
                treatment.Description, treatment.Price);
            treatments.Add(treatmentWithId);
            return idValue;
        }

        public List<Treatment> GetAllTreatments(string type, string name)
        {
            List<Treatment> wantedTreatments = new List<Treatment>();

            foreach (Treatment treatment in treatments)
            {
                if (treatment.Type == type && treatment.Name == name)
                {
                    wantedTreatments.Add(treatment);
                }

            }
            return wantedTreatments;
        }

        public void UpdateTreatment(Treatment treatmentWithUpdatedValues)
        {
            Treatment treatment2 = null;

            foreach (Treatment treatment in treatments)
            {
                if (treatment.Id == treatmentWithUpdatedValues.Id)
                {
                    treatment2 = treatment;
                    break;

                }

            }
            if (treatment2 != null)
            {
                treatments.Remove(treatment2);
                treatments.Add(Treatment.CreateTreatmentFromDb(treatmentWithUpdatedValues.Id, treatmentWithUpdatedValues.Type,
                    treatmentWithUpdatedValues.Name, treatmentWithUpdatedValues.Description, treatmentWithUpdatedValues.Price
                    ));

            }
        }
        public void DeleteTreatmentById(int id)
        {
            Treatment treatment2 = null;
            foreach (Treatment treatment in treatments)
            {
                if (treatment.Id == id)
                {
                    treatment2 = treatment;
                    break;
                }
            }
            if (treatment2 != null)
            {
                treatments.Remove(treatment2);
            }
        }
    }
}
