using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coding.API.Models;
using Microsoft.EntityFrameworkCore;
using coding.API.Models.Products.ProductsRequirements;
using coding.API.Models.PostTags;
using coding.API.Models.Photos;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Data
{
    [ExcludeFromCodeCoverage]
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dbContext;

        public Repository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Localiza una entidad por su id.
        /// </summary>
        public async Task<T> GetById(Guid id)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }
        public async Task<T> GetByIdWithList(Guid id, string relatedField, string anotherField)
        {
            return await _dbContext.Set<T>().Include(relatedField).Include(anotherField).SingleOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Lista todas las entidades.
        /// </summary>
        public async Task<List<T>> ListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();

        }

        public List<T> ListAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        /// <summary>
        /// Añade un entidad al repositorio.
        /// </summary>
        public async Task<T> Add(T entity)
        {
            entity.DateCreated = DateTime.Now;
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Elimina la entidad dada.
        /// </summary>
        public async Task<bool> Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            if (await this.SaveAll())
                return true;

            return false;

        }
        public bool DeleteSync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return true;
        }

        /// <summary>
        /// Actualiza la entidad dada.
        /// </summary>
        public async Task<bool> Update(T entity)
        {
            entity.DateModified = DateTime.Now;
            _dbContext.Entry(entity).State = EntityState.Modified;

            if (await this.SaveAll())
                return true;

            return false;
        }


        /// <summary>
        /// Guarda todos los cambios
        /// </summary>
        public async Task<bool> SaveAll()
        {
            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Loads a related field to a given entity
        /// </summary>
        public async Task<List<T>> GetRelatedField(string relatedField)
        {
            return await _dbContext.Set<T>()
            .Include(relatedField).ToListAsync();

        }
        /// <summary>
        /// Loads a related field to a given entity various levels
        /// </summary>
        public async Task<List<T>> GetRelatedFields(string relatedField, string anotherField)
        {
            return await _dbContext.Set<T>().Include(relatedField).Include(anotherField).ToListAsync();



        }


        public ProductRequirement GetRelatedRowPR(Guid productId, Guid requirementId)
        {
            return _dbContext.ProductRequirements.Where(p => p.ProductId == productId && p.RequirementId == requirementId).SingleOrDefault();
        }



        public PostTag GetRelatedRowPT(Guid postId, Guid tagId)
        {
            return _dbContext.PostTags.Where(pt => pt.PostId == postId && pt.TagId == tagId).SingleOrDefault();
        }


    }
}
