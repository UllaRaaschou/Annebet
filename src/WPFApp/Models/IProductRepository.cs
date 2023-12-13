using System.Collections.Generic;

namespace WPFApp.Models
{
    public interface IProductRepository
    {
        int AddProduct(Product product);
        List<Product> GetAllProducts(string type, string name);
        void UpdateProduct(Product productWithUpdatedValues);

        void DeleteProductById(int id);
    }
}