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
    public class UnitTestGetSalesItemById
    {
        [TestMethod]
        public void TestGetSalesItemById()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                Product product = Product.CreateProductFromUI("Rense-gel", "SuperRens", "renser alt", 900.00m);
                Treatment treatment = Treatment.CreateTreatmentFromUI("Massage", "Dybde-massage", "Massage i dybden", 1100.00m);
                POTRepository pOTRepo= new POTRepository();
                int idProduct = pOTRepo.AddSalesItem(product);
                int idTreatment = pOTRepo.AddSalesItem(treatment);


                // Act
                SalesItem chosenTreatment = pOTRepo.GetSalesItemById(idTreatment);
                Treatment treatmentWithId = (Treatment)chosenTreatment;

                // Assert
                Assert.AreEqual(treatmentWithId.Id, idTreatment);
                Assert.AreEqual(treatmentWithId.Type, "Massage");
                Assert.AreEqual(treatmentWithId.Name, "Dybde-massage");
                Assert.AreEqual(treatmentWithId.Description, "Massage i dybden");
                Assert.AreEqual(treatmentWithId.Price, 1100.00m);

            }




        }
    }
}
