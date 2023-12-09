using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models;

namespace WPFApp.Tests
{

    [TestClass]
    public class UnitTestGetAllSalesItems
    {
        [TestMethod]
        public void TestAddSalesItem()
        {
            // using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Arrange
                POTRepository pOTRepo = new POTRepository();

                List<SalesItem> allSalesItems = pOTRepo.GetAllSalesItems();
                int firstCount = allSalesItems.Count();

                pOTRepo.AddSalesItem(Product.CreateProductFromUI("SuperSerien", "SuperCreme", "En super creme", 1200.00m));
                pOTRepo.AddSalesItem(Treatment.CreateTreatmentFromUI("BehandlingsType", "BehandlingsNavn", "Fint behandling", 1000.00m));               

                
                // Act
                List<SalesItem> allSalesItems2 = pOTRepo.GetAllSalesItems();
                int secondCount = allSalesItems2.Count();


                // Assert
                Assert.AreEqual((firstCount + 2), secondCount);
            }

        }
    }
}
