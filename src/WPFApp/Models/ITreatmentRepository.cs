using System.Collections.Generic;

namespace WPFApp.Models
{
    public interface ITreatmentRepository
    {
        int AddTreatment(Treatment treatment);
        List<Treatment> GetAllTreatments(string type, string name);
        void UpdateTreatment(Treatment treatment);

        void DeleteTreatmentById(int id);
    }
}