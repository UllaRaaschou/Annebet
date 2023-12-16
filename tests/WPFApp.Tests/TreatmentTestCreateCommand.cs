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
    public class TreatmentTestCreateCommand
    {
        [TestMethod]
        public void TestExecute()
        {
            // Arrange
            TreatmentTESTRepository repository = new TreatmentTESTRepository(); // Instantiereing af Test-repo
                                                                                // Test-repo bruges som parameter i vores Command-Construcor, hvorved vi skifter fra brug
                                                                                // af løsningens normale repo til vores testRepo.

            TreatmentCreateCommand tcc = new TreatmentCreateCommand(repository);
            TreatmentCreateViewModel tcviewModel = new TreatmentCreateViewModel();
            tcviewModel.Type = "Massage";
            tcviewModel.Name = "RygMassage";
            tcviewModel.Description = "MassageAfRyggen";
            tcviewModel.Price = 25m;

            // Act
            tcc.Execute(tcviewModel);

            // assert
            Assert.AreEqual(repository.treatments[0].Type, "Massage");
            Assert.AreEqual(repository.treatments[0].Name, "RygMassage");
            Assert.AreEqual(repository.treatments[0].Description, "MassageAfRyggen");
            Assert.AreEqual(repository.treatments[0].Price, 25m);
        }


        [TestMethod]
        public void TestViewModelValuesToNullAfterExecute()
        {
            TreatmentTESTRepository repository = new TreatmentTESTRepository(); // TestRepo instantieres
            TreatmentCreateCommand tcc = new TreatmentCreateCommand(repository);
            TreatmentCreateViewModel tcviewModel = new TreatmentCreateViewModel();
            tcviewModel.Type = "Massage";
            tcviewModel.Name = "RygMassage";
            tcviewModel.Description = "MassageAfRyggen";
            tcviewModel.Price = 25m;

            // Act
            tcc.Execute(tcviewModel);

            // Assert
            Assert.IsNull(tcviewModel.Type);
            Assert.IsNull(tcviewModel.Name);
            Assert.IsNull(tcviewModel.Description);
            Assert.AreEqual(tcviewModel.Price, 0m);
        }


        [TestMethod]
        public void TestCanExecuteWithoutNullValuesInViewModelProperties()
        {
            // Arrange
            TreatmentTESTRepository repository = new TreatmentTESTRepository(); // TestRepo instantieres
            TreatmentCreateCommand tcc = new TreatmentCreateCommand(repository);
            TreatmentCreateViewModel tcviewModel = new TreatmentCreateViewModel();
            tcviewModel.Type = "Massage";
            tcviewModel.Name = "RygMassage";
            tcviewModel.Description = "MassageAfRyggen";
            tcviewModel.Price = 25m;

            // Act
            bool result = tcc.CanExecute(tcviewModel);

            // Assert
            Assert.IsTrue(result);  // Hvis ViewModelProperties er udfyldt, skal execute returnere True

        }

        [TestMethod]
        public void TestCanExecuteWithNullValueInViewModelProperties()
        {
            // Arrange
            TreatmentTESTRepository repository = new TreatmentTESTRepository(); // TestRepo instantieres
            TreatmentCreateCommand tcc = new TreatmentCreateCommand(repository);
            TreatmentCreateViewModel tcviewModel = new TreatmentCreateViewModel();
            tcviewModel.Type = "Massage";
            tcviewModel.Name = "RygMassage";
            tcviewModel.Description = null;
            tcviewModel.Price = 25m;

            // Act
            bool result = tcc.CanExecute(tcviewModel);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
