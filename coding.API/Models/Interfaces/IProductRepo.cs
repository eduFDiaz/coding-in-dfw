using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using coding.API.Models.Entities.Products;

namespace coding.API.Models.Interfaces
{
    public interface IProductRepo
    {
        Task<Product> Create(Product product);
        Task<List<Product>> GetAllUserProducts(int userid);
        Task<Product> GetProduct(int productid);
        Task<bool> DeleteProduct(int productid);
        Task<bool> SaveAll();
        Task<List<Product>> GetAll();
    }
}