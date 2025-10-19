using CoPilotDemo_VS.Data;
using CoPilotDemo_VS.Models;
using Microsoft.EntityFrameworkCore;

namespace CoPilotDemo_VS.Service
{
    public class ProductRepository
    {
        //async crud operations for Product entity
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Product.ToListAsync();
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Product.FindAsync(id);
        }
        public async Task AddProductAsync(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
