using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Commands;
using WPFApp.Models;
using WPFApp.ViewModels;

namespace WPFApp.Tests
{
    [TestClass]
    public class CustomerUpdateCommandTests
    {
        [TestMethod]
        public void TestExecute()
        {
            // Arrange
            TreatmentTESTRepository repository = new TreatmentTESTRepository();
            TreatmentUpdateCommand tuc = new TreatmentUpdateCommand(repository);
            TreatmentUpdateViewModel tuviewModel = new TreatmentUpdateViewModel();
            Treatment startTreatment = Treatment.CreateTreatmentFromDb(2, "", "", "", 0);
            repository.treatments.Add(startTreatment);
            Treatment updatedTreatment = Treatment.CreateTreatmentFromDb(2, "Massage", "RygMassage", "MassageAfRyggen", 25m);
            TreatmentToViewModel viewModelTreatment = new TreatmentToViewModel();
            TreatmentToViewModel updatedViewModelTreatment = viewModelTreatment.TreatmentToViewModelConvert(updatedTreatment);
            tuviewModel.SelectedTreatment = updatedViewModelTreatment;


            // Act
            tuc.Execute(tuviewModel);


            //Assert
            Assert.AreEqual(repository.treatments[0].Id, 2);
            Assert.AreEqual(repository.treatments[0].Type, "Massage");
            Assert.AreEqual(repository.treatments[0].Name, "RygMassage");
            Assert.AreEqual(repository.treatments[0].Description, "MassageAfRyggen");
            Assert.AreEqual(repository.treatments[0].Price, 25m);

        }

        [TestMethod]
        public void TestIsSelectedCustomerNullAfterExecute()
        {
            // Arrange
            TreatmentTESTRepository repository = new TreatmentTESTRepository();
            TreatmentUpdateCommand tuc = new TreatmentUpdateCommand(repository);
            TreatmentUpdateViewModel tuviewModel = new TreatmentUpdateViewModel();
            Treatment startTreatment = Treatment.CreateTreatmentFromDb(2, "", "", "", 0);
            repository.treatments.Add(startTreatment);
            Treatment updatedTreatment = Treatment.CreateTreatmentFromDb(2, "Massage", "RygMassage", "MassageAfRyggen", 25m);
            TreatmentToViewModel viewModelTreatment = new TreatmentToViewModel();
            TreatmentToViewModel updatedViewModelTreatment = viewModelTreatment.TreatmentToViewModelConvert(updatedTreatment);
            tuviewModel.SelectedTreatment = updatedViewModelTreatment;


            // Act
            tuc.Execute(tuviewModel);

            // Assert
            Assert.IsNull(tuviewModel.SelectedTreatment);
            Assert.IsNull(tuviewModel.Type);
            Assert.IsNull(tuviewModel.Name);

        }

        [TestMethod]
        public void TestcanExecuteWithoutNullValues()
        {
            // Arrange
            TreatmentUpdateViewModel tuviewModel = new TreatmentUpdateViewModel();
            Treatment updatedTreatment = Treatment.CreateTreatmentFromDb(2, "Massage", "RygMassage", "MassageAfRyggen", 25m);
            TreatmentToViewModel viewModelTreatment = new TreatmentToViewModel();
            TreatmentToViewModel updatedViewModelTreatment = viewModelTreatment.TreatmentToViewModelConvert(updatedTreatment);
            tuviewModel.SelectedTreatment = updatedViewModelTreatment;
            TreatmentUpdateCommand tuc = new TreatmentUpdateCommand();


            // Act
            bool result = tuc.CanExecute(tuviewModel);

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void TestcanExecuteWithNullValues()
        {
            // Arrange
            TreatmentUpdateViewModel tuviewModel = new TreatmentUpdateViewModel();
            tuviewModel.SelectedTreatment = null;
            TreatmentUpdateCommand tuc = new TreatmentUpdateCommand();


            // Act
            bool result = tuc.CanExecute(tuviewModel);

            //Assert
            Assert.IsFalse(result);

        }



    }
}