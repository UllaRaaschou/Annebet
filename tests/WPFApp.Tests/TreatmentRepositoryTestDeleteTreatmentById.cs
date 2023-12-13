using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    public class TreatmentRepositoryTestDeleteTreatmentById
    {
        [TestMethod]
        public void TestDeleteTreatmentById()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                TreatmentRepository treatmentRepo = new TreatmentRepository(); 
                int idTreatment = treatmentRepo.AddTreatment(Treatment.CreateTreatmentFromUI("Oliebehandling", "Oliedryp", "Dryp med varm olie", 1500.00m));
                List<Treatment> treatments = treatmentRepo.GetAllTreatments("Oliebehandling", "Oliedryp");
                int count1 = treatments.Count();
                
                // Act
               treatmentRepo.DeleteTreatmentById(idTreatment);
                List<Treatment> treatments2 = treatmentRepo.GetAllTreatments("Oliebehandling", "Oliedryp");
                int count2 = treatments.Count();

                // Assert
                Assert.AreEqual((count1 - 1),count2);
            }



        }
    }
}
