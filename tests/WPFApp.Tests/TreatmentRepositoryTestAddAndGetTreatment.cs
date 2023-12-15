using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class TreatmentRepositoryTestAddAndGetTreatment
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
        public void TestAddAndGetTreatment()
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


                // Act
                int id = treatmentRepo.AddTreatment(uniqueTreatment);
                List<Treatment> listOfUniqueTreatment = treatmentRepo.GetAllTreatments(uniqueTreatment.Type, uniqueTreatment.Name);


                // Assert
                Assert.AreEqual(id, listOfUniqueTreatment[0].Id);
                Assert.AreEqual(uniqueTreatment.Type, listOfUniqueTreatment[0].Type);
                Assert.AreEqual(uniqueTreatment.Name, listOfUniqueTreatment[0].Name);
                Assert.AreEqual(uniqueTreatment.Description, listOfUniqueTreatment[0].Description);
                Assert.AreEqual(uniqueTreatment.Price, listOfUniqueTreatment[0].Price);


               
            }

        }
    }
}
