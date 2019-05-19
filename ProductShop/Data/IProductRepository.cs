using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductShop.Data
{
    public interface IProductRepository
    {
        // General 
        Task<bool> SaveChangesAsync();

        // Products
        void AddProduct(Product product);
        void DeleteProduct(Product product);
        Task<Product[]> GetAllProductsAsync();
        Task<Product> GetProductAsync(int productId);
        Task Get(int ProductId);
        void Map(Product oldProduct, Product newProduct);

    }
}