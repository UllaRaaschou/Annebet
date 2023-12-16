using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFApp.Models;
using WPFApp.ViewModels;
using WPFApp.Commands;


namespace WPFApp.Tests
{
    [TestClass]
    public class TreatmentTestSearchCommand
    {     
            [TestMethod]
            public void TestExecuteWithCustomerUpdateViewModel()
            {
                // Arrange
                TreatmentTESTRepository repository = new TreatmentTESTRepository();
                repository.treatments.Add(Treatment.CreateTreatmentFromDb(4, "Massage", "RygMassage", "MassageAfRyggen", 25));
                repository.treatments.Add(Treatment.CreateTreatmentFromDb(7, "Massage", "RygMassage", "MassageAfRyggen", 25));
                TreatmentSearchCommand tsc = new TreatmentSearchCommand(repository);
                TreatmentUpdateViewModel tuviewModel = new TreatmentUpdateViewModel();
                tuviewModel.Type = "Massage";
                tuviewModel.Name = "RygMassage";


                // Act
                tsc.Execute(tuviewModel);


                // Assert
                Assert.AreEqual(tuviewModel.TreatmentsToView.Count(), 2);

            }

            [TestMethod]
            public void TestExecuteWithCustomerDeleteViewModel()
            {
            // Arrange
                 TreatmentTESTRepository repository = new TreatmentTESTRepository();
                 repository.treatments.Add(Treatment.CreateTreatmentFromDb(4, "Massage", "RygMassage", "MassageAfRyggen", 25));
                 repository.treatments.Add(Treatment.CreateTreatmentFromDb(7, "Massage", "MaveMassage", "MassageAfMaven", 35));
                 TreatmentSearchCommand tsc = new TreatmentSearchCommand(repository);
                 TreatmentDeleteViewModel tdviewModel = new TreatmentDeleteViewModel();
                 tdviewModel.Type = "Massage";
                 tdviewModel.Name = "RygMassage";
                
                
                 // Act
                 tsc.Execute(tdviewModel);


                // Assert
                Assert.AreEqual(tdviewModel.TreatmentsToView.Count(), 1);

            }

            [TestMethod]
            public void TestCanExecuteWithCustomerUpdateViewModelWithoutNullValues()
            {
                // Arrange
                TreatmentSearchCommand tsc = new TreatmentSearchCommand();
                TreatmentUpdateViewModel tuviewModel = new TreatmentUpdateViewModel();
                tuviewModel.Type = "Massage";
                tuviewModel.Name = "RygMassage";

                // Act
                bool result = tsc.CanExecute(tuviewModel);

                // Assert
                Assert.IsTrue(result);

            }

            [TestMethod]
            public void TestCanExecuteWithCustomerUpdateViewModelWithNullValues()
            {
                // Arrange
                TreatmentSearchCommand tsc = new TreatmentSearchCommand();
                TreatmentUpdateViewModel tuviewModel = new TreatmentUpdateViewModel();
                tuviewModel.Type = "Massage";
                tuviewModel.Name = null;

                // Act
                bool result = tsc.CanExecute(tuviewModel);

                // Assert
                Assert.IsFalse(result);
            }

            [TestMethod]
            public void TestCanExecuteWithCustomerDeleteViewModelWithoutNullValues()
            {
                // Arrange
                TreatmentSearchCommand tsc = new TreatmentSearchCommand();
                TreatmentDeleteViewModel tdviewModel = new TreatmentDeleteViewModel();
                tdviewModel.Type = "Massage";
                tdviewModel.Name = "RygMassage";

                // Act
                bool result = tsc.CanExecute(tdviewModel);

                // Assert
                Assert.IsTrue(result);

            }

            [TestMethod]
            public void TestCanExecuteWithCustomerDeleteViewModelWithNullValues()
            {
               // Arrange
               TreatmentSearchCommand tsc = new TreatmentSearchCommand();
               TreatmentDeleteViewModel tdviewModel = new TreatmentDeleteViewModel();
               tdviewModel.Type = null;
               tdviewModel.Name = "RygMassage";
              
               // Act
               bool result = tsc.CanExecute(tdviewModel);
              
               // Assert
               Assert.IsFalse(result);
               }
        }
}
