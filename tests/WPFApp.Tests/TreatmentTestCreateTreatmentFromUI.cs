﻿using WPFApp.Models;

namespace WPFApp.Tests
{
    [TestClass]
    public class TreatmentTestCreateTreatmentFromUi
    {
        [TestMethod]
        public void TestTreatmentCreateTreatmentFromUI()
        {
            // Arrange           
            string type = "Klassiske Ansigtsbehandlinger";
            string name = "Stressless";
            string description = "Regelmæssige ansigtsbehandlinger samt den korrekte daglige pleje, giver din hud fornyet energi";
            decimal price = 1300.00m;

            // Act
            Treatment treatment = Treatment.CreateTreatmentFromUI(type, name, description, price);
            // Assert
            Assert.AreEqual("Klassiske Ansigtsbehandlinger", treatment.Type);
            Assert.AreEqual("Stressless", treatment.Name);
            Assert.AreEqual("Regelmæssige ansigtsbehandlinger samt den korrekte daglige pleje, giver din hud fornyet energi", treatment.Description);
            Assert.AreEqual(1300.00m, treatment.Price);


        }
    }
}
