using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using coding.API.Models;
using coding.API.Models.PostTags;
using coding.API.Models.Products.ProductsRequirements;

namespace coding.API.Data
{
    public interface IRepository<T>  where T : BaseEntity
    {
        Task<T> GetById(Guid id);
        Task<T> GetByIdWithList(Guid id, string relatedField, string anotherField);

       Task<List<T>> ListAsync();

        List<T> ListAll();

        Task<T> Add(T entity);

        Task<bool> Delete(T entity);

       bool DeleteSync(T entity);

       Task<bool> Update(T entity);

       Task<bool> SaveAll();

       Task<List<T>> GetRelatedField(string relatedField);

       Task<List<T>> GetRelatedFields(string relatedField, string anotherField);

       ProductRequirement GetRelatedRowPR(Guid productId, Guid requirementId);

       PostTag GetRelatedRowPT(Guid postId, Guid tagId);
      
    }
}