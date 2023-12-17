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
    public class ProductSearchCommandTests
    {
        [TestMethod]
        public void TestexecuteWithProductUpdateViewModel()
        {
            // Arrange
            ProductTESTRepository repository = new ProductTESTRepository();
            repository.products.Add(Product.CreateProductFromDb(4, "Ansigtsbehandling", "Priori Pensling", "Exfolierende behandling", 1195));
            repository.products.Add(Product.CreateProductFromDb(7, "Ansigtsbehandling", "Priori Pensling", "Dybdegående", 895));
            ProductSearchCommand psc = new ProductSearchCommand(repository);
            ProductUpdateViewModel viewModel = new ProductUpdateViewModel();
            viewModel.Type = "Ansigtsbehandling";
            viewModel.Name = "Priori Pensling";


            // Act
            psc.Execute(viewModel);


            // Assert
            Assert.AreEqual(viewModel.ProductsToView.Count(), 2);

        }

        [TestMethod]
        public void TestexecuteWithProductDeleteViewModel()
        {
            // Arrange
            ProductTESTRepository repository = new ProductTESTRepository();
            repository.products.Add(Product.CreateProductFromDb(4, "Ansigtsbehandling", "Priori Pensling", "Exfolierende behandling", 1195));
            repository.products.Add(Product.CreateProductFromDb(7, "Ansigtsbehandling", "Priori Pensling", "Dybdegående", 895));
            ProductSearchCommand psc = new ProductSearchCommand(repository);
            ProductDeleteViewModel viewModel = new ProductDeleteViewModel();
            viewModel.Type = "Ansigtsbehandling";
            viewModel.Name = "Priori Pensling";


            // Act
            psc.Execute(viewModel);


            // Assert
            Assert.AreEqual(viewModel.ProductsToView.Count(), 2);

        }

        [TestMethod]
        public void TestCanExecuteWithProductUpdateViewModelWithoutNullValues()
        {
            // Arrange
            ProductSearchCommand psc = new ProductSearchCommand();
            ProductUpdateViewModel viewModel = new ProductUpdateViewModel();
            viewModel.Type = "Ansigtsbehandling";
            viewModel.Name = "Priori Pensling";

            // Act
            bool result = psc.CanExecute(viewModel);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void TestCanExecuteWithProductUpdateViewModelWithNullValues()
        {
            // Arrange
            ProductSearchCommand psc = new ProductSearchCommand();
            ProductUpdateViewModel viewModel = new ProductUpdateViewModel();
            viewModel.Type = "Ansigtsbehandling";
            viewModel.Name = null;

            // Act
            bool result = psc.CanExecute(viewModel);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCanExecuteWithCustomerDeleteViewModelWithoutNullValues()
        {
            // Arrange
            CustomerSearchCommand csc = new CustomerSearchCommand();
            CustomerDeleteViewModel viewModel = new CustomerDeleteViewModel();
            viewModel.FirstName = "Lotte";
            viewModel.LastName = "Hansen";

            // Act
            bool result = csc.CanExecute(viewModel);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void TestCanExecuteWithCustomerDeleteViewModelWithNullValues()
        {
            // Arrange
            CustomerSearchCommand csc = new CustomerSearchCommand();
            CustomerDeleteViewModel viewModel = new CustomerDeleteViewModel();
            viewModel.FirstName = "Lotte";
            viewModel.LastName = null;

            // Act
            bool result = csc.CanExecute(viewModel);

            // Assert
            Assert.IsFalse(result);
        }


    }
}
