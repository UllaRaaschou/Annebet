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


        /// <summary>
        /// Guid-metoden genbruges, med id fra første Guid som parameter, for at skabe en unit, opdateret version.
        /// </summary
        private Treatment CreateUniqueTreatment2(int id)
        {
            Guid guid = Guid.NewGuid();
            return Treatment.CreateTreatmentFromDb(id, "uniqueType" + guid, "Ankelmassage", "Blid ankelmasage", 700.00m);
        }




        [TestMethod]
        public void TestTreatmentRepositoryAddAndUpdate()
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
                Treatment uniqueTreatment = CreateUniqueTreatment();


                int id = treatmentRepo.AddTreatment(uniqueTreatment);
                List<Treatment> newList = treatmentRepo.GetAllTreatments(uniqueTreatment.Type, uniqueTreatment.Name);
                Treatment uniqueUpdatedValues = CreateUniqueTreatment2(id);
               

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
