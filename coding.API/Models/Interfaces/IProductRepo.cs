using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using coding.API.Models.Entities.Products;
using coding.API.Models.Entities.Products.ProductsRequirements;
using coding.API.Models.Entities.Products.Requirements;

namespace coding.API.Models.Interfaces
{
    public interface IProductRepo
    {
        Task<Product> Create(Product product);
        Task<Requirement> CreateRequirement(Requirement requirement);
        Task<List<Product>> GetAllUserProducts(int userid);
        Task<Product> GetProduct(int productid);
        Task<bool> DeleteProduct(int productid);
        Task<bool> SaveAll();
        Task<List<Product>> GetAll();
        Task<bool> addRequirementToProduct(ProductRequirement requirement);

        Task<Requirement> GetRequirementById(int requirementId);
    }
}