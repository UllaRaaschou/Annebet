using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp.Models
{
    public class TreatmentRepository : SalesItemRepository
    {
        public int AddTreatment(Treatment treatment) 
        {
            return base.AddSalesItem(treatment, EnumCategory.Treatment);
        }

        public void UpdateTreatment(Treatment treatment) 
        {
            base.UpdateSalesItem(treatment, EnumCategory.Treatment);
        }

        public List<Treatment> GetAllTreatmentsFromCategoryAndTypeAndName(string type, string name)
        {
            List<Treatment> wantedTreatments = new List<Treatment>();
            SqlDataReader reader = base.GetAllSalesItemsFromCategoryAndTypeAndName(EnumCategory.Treatment, type, name);
            // Her tjekker vi ikke for, om reader != 0, da den abstrakte metode altid returnerer et readerObjekt.
            while (reader.Read())
            {
                int id = (int)reader.GetInt32(0);
                string description = reader.GetString(1);
                decimal price = reader.GetDecimal(2);

                Treatment treatment = Treatment.CreateTreatmentFromDb(id, type, name, description, price);
                wantedTreatments.Add(treatment);
            }

            return wantedTreatments;
        }
    }
}
