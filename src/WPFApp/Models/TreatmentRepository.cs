using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public class TreatmentRepository : SalesItemRepository, ITreatmentRepository  // Nedarver fra SalesItemRepo og implementerer  ITreatmentRepo
    {
        public int AddTreatment(Treatment treatment)
        {
            return base.AddSalesItem(treatment, EnumCategory.Treatment); // Kalder den abstrakte add-metode i parent-class med en treatments værdier
        }

        public List<Treatment> GetAllTreatments(string type, string name)
        {
            List<Treatment> wantedTreatments = new List<Treatment>(); // instantiering af en tom liste med treatments

            using SqlDataReader reader = base.GetAllSalesItems(EnumCategory.Treatment, type, name); //  Kalder den abstrakte getAll-metode i parent-class med en treatments værdier.
            // Her tjekker vi ikke for, om reader != 0, da den abstrakte metode altid returnerer et readerObjekt.
            try 
            {
                while (reader.Read())
                {
                    int id = (int)reader.GetInt32(0);
                    string description = reader.GetString(1);
                    decimal price = reader.GetDecimal(2);

                    Treatment treatment = Treatment.CreateTreatmentFromDb(id, type, name, description, price); // Et treatment skabes ud fra de læste værdier
                    wantedTreatments.Add(treatment); // treatmentet lægges i den instantierede liste
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("Produkter med specificerede type og navn kunne ikke fremsøges", ex);
            }

            return wantedTreatments; // listen returneres
            // using lukker readeren
        }


        public void UpdateTreatment(Treatment treatment) 
        {
            base.UpdateSalesItem(treatment, EnumCategory.Treatment); // kalder Update-metoden i parent-class med en treatments værdier
        }

        public void DeleteTreatmentById(int id) 
        {
            base.DeleteSalesItemById(id);  // kalder Delete-metoden i parent-class med et products værdier
        }
    }
      
}
