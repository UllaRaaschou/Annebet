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
    public class ProductCreateCommandTests
    {
        [TestMethod]
        public void TestExecute()
        {
            // Arrange
            ProductTESTRepository repository = new ProductTESTRepository(); // Instantiereing af Test-repo
                                                                            // Test-repo bruges som parameter i vores Command-Construcor, hvorved vi skifter fra brug
                                                                            // af løsningens normale repo til vores testRepo.

            ProductCreateCommand pcc = new ProductCreateCommand(repository);
            ProductCreateViewModel viewModel = new ProductCreateViewModel();
            viewModel.Type = "SkinbetterS";
            viewModel.Name = "Balsam";
            viewModel.Description = "Forebyggende balsam";
            viewModel.Price = 1300;


            // Act
            pcc.Execute(viewModel);


            // assert
            Assert.AreEqual(repository.products[0].Type, "SkinbetterS");
            Assert.AreEqual(repository.products[0].Name, "Balsam");
            Assert.AreEqual(repository.products[0].Description, "Forebyggende balsam");
            Assert.AreEqual(repository.products[0].Price, 1300);
        }




        [TestMethod]
        public void TestViewModelValuesToNullAfterExecute()
        {
            ProductTESTRepository repository = new ProductTESTRepository(); // TestRepo instantieres
            ProductCreateCommand pcc = new ProductCreateCommand(repository);
            ProductCreateViewModel viewModel = new ProductCreateViewModel();
            viewModel.Type = "SkinbetterS";
            viewModel.Name = "Balsam";
            viewModel.Description = "Forebyggende balsam";
            viewModel.Price = 1300;

            // Act
            pcc.Execute(viewModel);

            // Assert
            Assert.IsNull(viewModel.Type);
            Assert.IsNull(viewModel.Name);
            Assert.IsNull(viewModel.Description);
            Assert.AreEqual(viewModel.Price, 0);
        }




        [TestMethod]
        public void TestCanExecuteWithoutNullValuesInViewModelProperties()
        {
            // Arrange
            ProductTESTRepository repository = new ProductTESTRepository();
            ProductCreateCommand pcc = new ProductCreateCommand(repository);
            ProductCreateViewModel viewModel = new ProductCreateViewModel();
            viewModel.Type = "SkinbetterS";
            viewModel.Name = "Balsam";
            viewModel.Description = "Forebyggende balsam";
            viewModel.Price = 1300;

            // Act
            bool result = pcc.CanExecute(viewModel);

            // Assert
            Assert.IsTrue(result);  // Hvis ViewModelProperties er udfyldt, skal execute returnere True

        }

        public void TestCanExecuteWithNullValueInViewModelProperties()
        {
            // Arrange
            ProductTESTRepository repository = new ProductTESTRepository();
            ProductCreateCommand pcc = new ProductCreateCommand(repository);
            ProductCreateViewModel viewModel = new ProductCreateViewModel();
            viewModel.Type = "SkinbetterS";
            viewModel.Name = "Balsam";
            viewModel.Description = "Forebyggende balsam";
            viewModel.Price = 1300;

            // Act
            bool result = pcc.CanExecute(viewModel);

            // Assert
            Assert.IsFalse(result);

        }

    }
}
