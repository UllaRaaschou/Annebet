using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class UnitTestCreateserviceFromUI
    {
        [TestMethod]
        public void TestCreateServiceFromUI()
        {
            // Arrange           
            string type = "Klassiske Ansigtsbehandlinger";
            string name = "Stressless";
            string description = "Regelmæssige ansigtsbehandlinger samt den korrekte daglige pleje, giver din hud fornyet energi";
            decimal price = 1300.00m;

            // Act
            Service service = Service.CreateServiceFromUI(type, name, description, price);

            // Assert
            Assert.AreEqual("Klassiske Ansigtsbehandlinger", service.Type);
            Assert.AreEqual("Stressless", service.Name);
            Assert.AreEqual("Regelmæssige ansigtsbehandlinger samt den korrekte daglige pleje, giver din hud fornyet energi", service.Description);
            Assert.AreEqual(1300.00m, service.Price);


        }
    }
}
