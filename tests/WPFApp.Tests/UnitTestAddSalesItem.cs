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
    public class UnitTestAddSalesItem
    {
        [TestMethod]
        public void TestAddSalesItem()
        {
           // using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                POTRepository pOTRepo = new POTRepository();

                pOTRepo.AddSalesItem(Product.CreateProductFromUI("SuperSerien", "SuperCreme", "En super creme", 1200.00m));

                List<SalesItem> allSalesItems = pOTRepo.GetAllSalesItems();
                int firstCount = allSalesItems.Count();

                Treatment treatment = Treatment.CreateTreatmentFromUI("BehandlingsType", "BehandlingsNavn", "Fint behandling", 1000.00m);


                // Act
                pOTRepo.AddSalesItem(treatment);
                List<SalesItem> allSalesItems2 = pOTRepo.GetAllSalesItems();
                int secondCount = allSalesItems2.Count();


                // Assert
                Assert.AreEqual((firstCount + 1), secondCount);
            }

        }
    }
}
