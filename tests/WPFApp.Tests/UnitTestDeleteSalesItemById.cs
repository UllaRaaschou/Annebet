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
    public class UnitTestDeleteSalesitemById
    {
        [TestMethod]
        public void TestDeleteSalesItemById()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                Product product = Product.CreateProductFromUI("Værktøj", "Negleklipper", "Effektiv negleklipper", 200.00m);
                Treatment treatment = Treatment.CreateTreatmentFromUI("Oliebehandling", "Oliedryp", "Dryp med varm olie", 1500.00m);
                POTRepository pOTRepo = new POTRepository();
                int idProdukt = pOTRepo.AddSalesItem(product);
                int idTreatment = pOTRepo.AddSalesItem(treatment);
                List<SalesItem> list = pOTRepo.GetAllSalesItems();
                int countBeforeDeletion = list.Count;
               

                // Act
                pOTRepo.DeleteSalesItemById(idProdukt);
                List<SalesItem> list2 = pOTRepo.GetAllSalesItems();
                int countAfterDeletion = list2.Count;

                // Assert
                Assert.AreEqual((countBeforeDeletion - 1), countAfterDeletion);
            }



        }
    }
}
