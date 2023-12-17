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
    public class ProductUpdateCommandTests
    {
        [TestMethod]
        public void TestExecute()
        {
            // Arrange
            ProductTESTRepository repository = new ProductTESTRepository();
            ProductUpdateCommand puc = new ProductUpdateCommand(repository);
            ProductUpdateViewModel viewModel = new ProductUpdateViewModel();
            Product startProduct = Product.CreateProductFromDb(2, "", "", "", 0);
            repository.products.Add(startProduct);
            Product updatedProduct = Product.CreateProductFromDb(2, "Pedicure", "Cuticle Clean", "Plejende pedicure", 495);
            ProductToViewModel viewModelProduct = new ProductToViewModel();
            ProductToViewModel updatedViewModelProduct = viewModelProduct.ProductToViewModelConvert(updatedProduct);
            viewModel.SelectedProduct = updatedViewModelProduct;


            // Act
            puc.Execute(viewModel);

            //Assert
            Assert.AreEqual(repository.products[0].Id, 2);
            Assert.AreEqual(repository.products[0].Type, "Pedicure");
            Assert.AreEqual(repository.products[0].Name, "Cuticle Clean");
            Assert.AreEqual(repository.products[0].Description, "Plejende pedicure");
            Assert.AreEqual(repository.products[0].Price, 495);
        }

        [TestMethod]
        public void TestIsSelectedProductNullAfterExecute()
        {
            // Arrange
            ProductTESTRepository repository = new ProductTESTRepository();
            ProductUpdateCommand puc = new ProductUpdateCommand(repository);
            ProductUpdateViewModel viewModel = new ProductUpdateViewModel();
            Product startProduct = Product.CreateProductFromDb(2, "", "", "", 0);
            repository.products.Add(startProduct);
            Product updatedProduct = Product.CreateProductFromDb(2, "Pedicure", "Cuticle Clean", "Plejende pedicure", 495);
            ProductToViewModel viewModelProduct = new ProductToViewModel();
            ProductToViewModel updatedViewModelProduct = viewModelProduct.ProductToViewModelConvert(updatedProduct);
            viewModel.SelectedProduct = updatedViewModelProduct;


            // Act
            puc.Execute(viewModel);

            // Assert
            Assert.IsNull(viewModel.SelectedProduct);
            Assert.IsNull(viewModel.Type);
            Assert.IsNull(viewModel.Name);

        }

        [TestMethod]
        public void TestCanExecuteWithoutNullValues()
        {
            // Arrange
            ProductUpdateViewModel viewModel = new ProductUpdateViewModel();
            Product product = Product.CreateProductFromDb(2, "Pedicure", "Cuticle Clean", "Plejende pedicure", 495);
            ProductToViewModel viewModelProduct = new ProductToViewModel();
            ProductToViewModel convertedViewModelProduct = viewModelProduct.ProductToViewModelConvert(product);
            viewModel.SelectedProduct = convertedViewModelProduct;
            ProductUpdateCommand puc = new ProductUpdateCommand();


            // Act
            bool result = puc.CanExecute(viewModel);

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void TestCanExecuteWithNullValues()
        {
            // Arrange
            ProductUpdateViewModel viewModel = new ProductUpdateViewModel();
            viewModel.SelectedProduct = null;
            ProductUpdateCommand puc = new ProductUpdateCommand();


            // Act
            bool result = puc.CanExecute(viewModel);

            //Assert
            Assert.IsFalse(result);

        }



    }
}
