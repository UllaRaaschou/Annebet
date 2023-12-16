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
    public class TreatmentDeleteCommandTests
    {
        [TestMethod]
        public void TestExecute()
        {
            // Arrange
            TreatmentTESTRepository repository = new TreatmentTESTRepository();   // Instantiereing af Test-repo
                                                                              //Test - repo bruges som parameter i vores Command-Construcor, hvorved vi skifter fra brug
                                                                              // af løsningens normale repo til vores testRepo.

            Treatment treatment = Treatment.CreateTreatmentFromDb(1, "Ryg", "Rygmassage", "MassageAfRyggen", 25);
            repository.treatments.Add(treatment);

            TreatmentDeleteCommand tdc = new TreatmentDeleteCommand(repository);
            TreatmentDeleteViewModel tdviewModel = new TreatmentDeleteViewModel();

            TreatmentToViewModel tdviewModelTreatment = new TreatmentToViewModel();
            tdviewModel.SelectedTreatment = tdviewModelTreatment.TreatmentToViewModelConvert(treatment);




            // Act
            tdc.Execute(tdviewModel);



            // assert
            Assert.IsTrue(repository.treatments.Count() == 0);

        }



        [TestMethod]
        public void TestViewModelValuesToNullAfterExecute()
        {
            // Arrange
            TreatmentTESTRepository repository = new TreatmentTESTRepository();           
            Treatment treatment = Treatment.CreateTreatmentFromDb(1, "Massage", "RygMassage", "MassageAfRyggen", 25);
            repository.treatments.Add(treatment);
            TreatmentDeleteCommand tdc = new TreatmentDeleteCommand(repository);           
            TreatmentDeleteViewModel tdviewModel = new TreatmentDeleteViewModel();
            TreatmentToViewModel tdviewModelTreatment = new TreatmentToViewModel();
            tdviewModel.SelectedTreatment = tdviewModelTreatment.TreatmentToViewModelConvert(treatment);

            // Act
            tdc.Execute(tdviewModel);

            // Assert
            Assert.IsNull(tdviewModel.Type);
            Assert.IsNull(tdviewModel.Name);
        }


        [TestMethod]
        public void TestCanExecuteWithoutNullValuesInViewModelProperties()
        {
            // Arrange
            TreatmentTESTRepository repository = new TreatmentTESTRepository();
            TreatmentDeleteCommand tdc = new TreatmentDeleteCommand(repository);
            Treatment treatment = Treatment.CreateTreatmentFromDb(1, "Massage", "RygMassage", "MassageAfRyggen", 25);
            repository.treatments.Add(treatment);
            TreatmentDeleteViewModel tdviewModel = new TreatmentDeleteViewModel();
            TreatmentToViewModel tdviewModelTreatment = new TreatmentToViewModel();
            tdviewModel.SelectedTreatment = tdviewModelTreatment.TreatmentToViewModelConvert(treatment);

            // Act
            bool result = tdc.CanExecute(tdviewModel);

            // Assert
            Assert.IsTrue(result); // Hvis ViewModelProperties er udfyldt, skal execute returnere True

        }

        [TestMethod]
        public void TestCanExecuteWithNullValueInViewModelProperties()
        {
            // Arrange
            TreatmentTESTRepository repository = new TreatmentTESTRepository();
            TreatmentDeleteCommand tdc = new TreatmentDeleteCommand(repository);
            Treatment treatment = Treatment.CreateTreatmentFromDb(1, "Massage", "RygMassage", "MassageAfRyggen", 25);
            repository.treatments.Add(treatment);
            TreatmentDeleteViewModel tdviewModel = new TreatmentDeleteViewModel();
            tdviewModel.SelectedTreatment = null;

            // Act
            bool result = tdc.CanExecute(tdviewModel);

            // Assert
            Assert.IsFalse(result);
        }

    }
}