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
    public class UnitTestUpdateSalesItem
    {
        [TestMethod]
        public void TestUpdateSalesItem()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                Treatment treatment = Treatment.CreateTreatmentFromUI("Relax", "Relax Treatment", "Får dig til at slappe af", 1500.00m);
                POTRepository pOTRepo = new POTRepository();
                int id = pOTRepo.AddSalesItem(treatment);
                List<SalesItem> list = pOTRepo.GetAllSalesItemsFromCategoryAndTypeAndName(EnumCategory.Treatment, "Relax", "Relax Treatment");
                Treatment treatmentBeforeUpdate = (Treatment)list[0];
                Treatment  updatedTreatment = Treatment.CreateTreatmentFromUI("Relax", "Relax Treatment", "Får dig til at slappe af", 1750.00m);


                // Act
                pOTRepo.UpdateSalesItem(updatedTreatment);


                // Assert
                Assert.AreEqual(treatmentBeforeUpdate.Type, "Relax");
                Assert.AreEqual(treatmentBeforeUpdate.Name, "Relax Treatment");
                Assert.AreEqual(treatmentBeforeUpdate.Description, "Får dig til at slappe af");
                Assert.AreEqual(treatmentBeforeUpdate.Price, 1500.00m);

                Assert.AreEqual(updatedTreatment.Type, "Relax");
                Assert.AreEqual(updatedTreatment.Name, "Relax Treatment");
                Assert.AreEqual(updatedTreatment.Description, "Får dig til at slappe af");
                Assert.AreEqual(updatedTreatment.Price, 1750.00m);


            }


        }
    }
}
