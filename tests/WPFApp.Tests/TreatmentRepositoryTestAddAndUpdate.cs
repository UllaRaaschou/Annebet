using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class TreatmentRepositoryTestAddAndUpdate
    {
        /// <summary>
        /// Metode, hvis eneste formål er at oprette en unik customer. Viste sig at være en god ide, fordi vi sad 3 studerende og arbejdede
        /// på samme database og de customers, som vi oprettede i vores unit-tests, havde samme for- og efternavn som kunder, der 
        /// allerede var i databasen
        /// </summary>      
        private Treatment CreateUniqueTreatment()
        {
            Guid guid = Guid.NewGuid();
            return Treatment.CreateTreatmentFromUI("uniqueType" + guid, "Fodmassage", "Blid fodmasage", 850.00m);
        }


        [TestMethod]
        public void TestTreatmentRepositoryAddAndUpdate()
        {         

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                
                TreatmentRepository treatmentRepo = new TreatmentRepository();
                Treatment uniqueTreatment = CreateUniqueTreatment();


                int id = treatmentRepo.AddTreatment(uniqueTreatment);
                List<Treatment> newList = treatmentRepo.GetAllTreatments(uniqueTreatment.Type, uniqueTreatment.Name);
                Treatment uniqueUpdatedValues = CreateUniqueTreatment();
               

                // Act
                treatmentRepo.UpdateTreatment(uniqueUpdatedValues);
                List<Treatment> updatedList = treatmentRepo.GetAllTreatments(uniqueUpdatedValues.Type, uniqueUpdatedValues.Name);

              
                // Assert
                Assert.AreEqual(id, newList[0].Id);
                Assert.AreEqual(uniqueTreatment.Type, newList[0].Type);
                Assert.AreEqual(uniqueTreatment.Name, newList[0].Name);
                Assert.AreEqual(uniqueTreatment.Description, newList[0].Description);
                Assert.AreEqual(uniqueTreatment.Price, newList[0].Price);


                Assert.AreEqual(id, updatedList[0].Id);
                Assert.AreEqual(uniqueUpdatedValues.Type, updatedList[0].Type);
                Assert.AreEqual(uniqueUpdatedValues.Name, updatedList[0].Name);
                Assert.AreEqual(uniqueUpdatedValues.Description, updatedList[0].Description);
                Assert.AreEqual(uniqueUpdatedValues.Price, updatedList[0].Price);



            }


        }
    }
}
