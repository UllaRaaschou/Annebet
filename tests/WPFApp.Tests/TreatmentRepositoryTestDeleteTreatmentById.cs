using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    public class TreatmentRepositoryTestDeleteTreatmentById
    {
        [TestMethod]
        public void TestDeleteTreatmentById()
        {
            // "TranscationScope": Laver testen til et scope, hvor alle operationer udføres i een transaktion
            //                     Enten udføres alle - eller også rulles alle tilbage. Vi ruller alle tilbage.
            // "TransactionScopeOption.Required": Hvis der ikke allerede ER et scope, oprettes der ét.
            // "new TransactionOptions(){}": Instantiering af klasse, der angiver flere detaljer for transaktionen
            // "Isolationlevel.ReadUncommitted": Giver mulighed for at læse un-committet data
            // "Scope.Complete(): Hvis vi havde kaldt denne metode, ville transaktion blive comittet. Vi comitter ikke!

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
