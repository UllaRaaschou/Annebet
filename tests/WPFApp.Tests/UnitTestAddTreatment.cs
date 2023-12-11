using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class UnitTestAddTreatment
    {
        [TestMethod]
        public void TestAddTreatment()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                TreatmentRepository treatmentRepo = new TreatmentRepository();


                // Act
                int treatmentId =treatmentRepo.AddTreatment(Treatment.CreateTreatmentFromUI("Massage", "Rygmassage", "Dybdegående massage", 1000.00m));
                List<Treatment> addetTreatment = treatmentRepo.GetAllTreatmentsFromCategoryAndTypeAndName("Massage", "Rygmassage");

                // Assert
                Assert.AreEqual((addetTreatment[0].Id), treatmentId);
                Assert.AreEqual((addetTreatment[0].Description), "Dybdegående massage");
                Assert.AreEqual((addetTreatment[0].Price), 1000.00m);

            }

        }
    }
}
