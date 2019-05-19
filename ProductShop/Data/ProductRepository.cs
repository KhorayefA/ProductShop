using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShop.Data
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _context;

        public ProductRepository()
        {
            _context = new ProductContext();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product[]> GetAllProductsAsync()
        {
            var query = _context.Products
              .OrderBy(t => t.ProductId);

            return await query.ToArrayAsync();
        }

        public async Task<Product> GetProductAsync(int productId)
        {

           return await _context.Products.FirstOrDefaultAsync(o => o.ProductId == productId);           
        }
        public Task Get(int ProductId)
        {
            return _context.Set<Task>().Find(ProductId);
        }

        public void Map (Product oldProduct, Product newProduct)
        {
            oldProduct.ProductId = newProduct.ProductId;
            oldProduct.Name = newProduct.Name;
            oldProduct.Price = newProduct.Price;
            oldProduct.Description = newProduct.Description;
            oldProduct.Category = newProduct.Category;
        } 
    }
}

