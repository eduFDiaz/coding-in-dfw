using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using coding.API.Models.Interfaces;
using coding.API.Models.Entities.Products;
using coding.API.Models.Entities.Products.ProductsRequirements;
using coding.API.Models.Entities.Products.Requirements;

namespace coding.API.Data
{
    public class ProductRepo : IProductRepo
    {
        
        private readonly DataContext _context;
        public ProductRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProduct(int productid)
        {
            if (productid == 0)
                return null;

            var product = await _context.Products.FirstOrDefaultAsync(prod => prod.Id == productid);

            return product;
        }

        public async Task<Product> Create(Product product)
        {
            if (product == null)
                return null;

            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllUserProducts(int userid)
        {
           var products = await _context.Products.Where(p => p.UserId == userid).ToListAsync();
           var requirements = await _context.Requirements.Include(t => t.ProductRequirements).ToListAsync();
            
           return products;
        }

        public async Task<bool> DeleteProduct(int productid)
        {
            var productToDelete = await _context.Products.FirstOrDefaultAsync(p => p.Id == productid);

            if (productToDelete == null)
                return false;

            _context.Products.Remove(productToDelete);
            
            await _context.SaveChangesAsync();

            return true;
        }                

        public async Task<bool> EditProduct(int productid, Product product)
        {
            var productToEdit = await _context.Products.FirstOrDefaultAsync(p => p.Id == productid);

            if (productToEdit == null)
                return false;
            
            _context.Products.Update(productToEdit);

            await _context.SaveChangesAsync();
                        
            return true;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Product>> GetAll()
        {
            var allproducts = await _context.Products.ToListAsync();

            return allproducts;
        }

        public async Task<bool> addRequirementToProduct(ProductRequirement productRequirement)
        {
            await _context.ProductRequirements.AddAsync(productRequirement);

            return true;
        }

        public async Task<Requirement> CreateRequirement(Requirement requirement)
        {
                  
            var reqnew = await _context.Requirements.AddAsync(requirement);
           
            return requirement;
        }

        public async Task<Requirement> GetRequirementById(int requirementId)
        {
            return await _context.Requirements.Where(r => r.Id == requirementId).SingleOrDefaultAsync();
        }
    }
}