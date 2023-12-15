using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    // Nedarver fra SalesItemRepository og implementerer interfacet ITreatmentRepository
    public class TreatmentRepository : SalesItemRepository, ITreatmentRepository
    {
        /// Kalder den abstrakte add-metode i parent-class med en treatments værdier
        public int AddTreatment(Treatment treatment)
        {
            return base.AddSalesItem(treatment, EnumCategory.Treatment); 
        }

        public List<Treatment> GetAllTreatments(string type, string name)
        {
            // Instantiering af en tom liste med treatments
            List<Treatment> wantedTreatments = new List<Treatment>();

            //  Kalder den abstrakte getAll-metode i parent-class med en treatments værdier.
            using SqlDataReader reader = base.GetAllSalesItems(EnumCategory.Treatment, type, name);

            // Her tjekker vi ikke for, om reader != 0, da den abstrakte metode altid returnerer et readerObjekt.
            // Metoden returnerer et reader-object, som derefter skal læses
            while (reader.Read())
            {
                // Hvis reader læser
                int id = (int)reader.GetInt32(0);
                string description = reader.GetString(1);
                decimal price = reader.GetDecimal(2);

                // Et treatment skabes ud fra de læste værdier
                Treatment treatment = Treatment.CreateTreatmentFromDb(id, type, name, description, price);
                
                // Treatmentet lægges i den instantierede liste
                wantedTreatments.Add(treatment);
            }
            // Listen returneres
            return wantedTreatments;
        }

        /// kalder Update-metoden i parent-class med en treatments værdier
        public void UpdateTreatment(Treatment treatment) 
        {
            base.UpdateSalesItem(treatment, EnumCategory.Treatment);
        }

        /// kalder Delete-metoden i parent-class med et products værdier
        public void DeleteTreatmentById(int id) 
        {
            base.DeleteSalesItemById(id); 
        }
    }     
}
