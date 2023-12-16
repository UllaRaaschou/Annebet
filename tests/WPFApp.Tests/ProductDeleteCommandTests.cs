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
    public class ProductDeleteCommandTests
    {
        [TestMethod]
        public void TestExecute()
        {
            // Arrange
            ProductTESTRepository repository = new ProductTESTRepository();   // Instantiereing af Test-repo
                                                                              //Test - repo bruges som parameter i vores Command-Construcor, hvorved vi skifter fra brug
                                                                              // af løsningens normale repo til vores testRepo.

            Product product = Product.CreateProductFromDb(5, "Ryg", "Rygmassage", "Afslappende rygmassage", 52627);
            repository.products.Add(product);

            ProductDeleteCommand pdc = new ProductDeleteCommand(repository);
            ProductDeleteViewModel viewModel = new ProductDeleteViewModel();

            ProductToViewModel viewModelProduct = new ProductToViewModel();
            viewModel.SelectedProduct = viewModelProduct.ProductToViewModelConvert(product);




            // Act
            pdc.Execute(viewModel);



            // assert
            Assert.IsTrue(repository.products.Count() == 0);

        }




        [TestMethod]
        public void TestViewModelValuesToNullAfterExecute()
        {
            ProductTESTRepository repository = new ProductTESTRepository();

            Product product = Product.CreateProductFromDb(5, "Ryg", "Rygmassage", "Afslappende rygmassage", 52627);
            repository.products.Add(product);

            ProductDeleteCommand cdc = new ProductDeleteCommand(repository);
            ProductDeleteViewModel viewModel = new ProductDeleteViewModel();

            ProductToViewModel viewModelProduct = new ProductToViewModel();
            viewModel.SelectedProduct = viewModelProduct.ProductToViewModelConvert(product);


            // Act
            cdc.Execute(viewModel);


            // Assert
            Assert.IsNull(viewModel.Type);
            Assert.IsNull(viewModel.SelectedProduct);
        }




        [TestMethod]
        public void TestCanExecuteWithoutNullValuesInViewModelProperties()
        {
            // Arrange
            ProductTESTRepository repository = new ProductTESTRepository();
            ProductDeleteCommand pdc = new ProductDeleteCommand(repository);
            ProductDeleteViewModel viewModel = new ProductDeleteViewModel();
            Product product = Product.CreateProductFromUI("Ryg", "Rygmassage", "Afslappende rygmassage", 52627);
            ProductToViewModel productToViewModel = new ProductToViewModel();
            ProductToViewModel viewModelProduct = new ProductToViewModel();

            viewModel.SelectedProduct = viewModelProduct.ProductToViewModelConvert(product);
            viewModel.SelectedProduct.Type = "Ryg";
            viewModel.SelectedProduct.Name = "Rygmassage";
            viewModel.SelectedProduct.Description = "Afslappende rygmassage";
            viewModel.SelectedProduct.Price = 52627;


            // Act
            bool result = pdc.CanExecute(viewModel);

            // Assert
            Assert.IsTrue(result);  // Hvis ViewModelProperties er udfyldt, skal execute returnere True

        }

        [TestMethod]
        public void TestCanExecuteWithNullValueInViewModelProperties()
        {
            // Arrange
            ProductTESTRepository repository = new ProductTESTRepository();
            ProductDeleteCommand pdc = new ProductDeleteCommand(repository);
            ProductDeleteViewModel viewModel = new ProductDeleteViewModel();
            Product product = Product.CreateProductFromUI("Ryg", "Rygmassage", "Afslappende rygmassage", 52627);
            ProductToViewModel productToViewModel = new ProductToViewModel();
            ProductToViewModel viewModelProduct = new ProductToViewModel();

            viewModel.SelectedProduct = viewModelProduct.ProductToViewModelConvert(product);
            viewModel.SelectedProduct.Type = null;
            viewModel.SelectedProduct.Name = "Rygmassage";
            viewModel.SelectedProduct.Description = "Afslappende rygmassage";
            viewModel.SelectedProduct.Price = 52627;


            // Act
            bool result = pdc.CanExecute(viewModel);

            // Assert
            Assert.IsFalse(result);

        }

    }
}
