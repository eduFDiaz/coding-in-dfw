using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coding.API.Models;
using Microsoft.EntityFrameworkCore;
using coding.API.Models.Entities.Posts;
using coding.API.Models.Entities;

namespace coding.API.Data
{
    public class GenericRepo<T> where T : BaseEntity
    {
        private readonly DataContext _dbContext;

        public GenericRepo(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Localiza una entidad por su id.
        /// </summary>
        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Lista todas las entidades.
        /// </summary>
        public async Task<List<T>> ListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Añade un entidad al repositorio.
        /// </summary>
        public async Task<T> Add(T entity)
        {
            
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Elimina la entidad dada.
        /// </summary>
        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            // await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza la entidad dada.
        /// </summary>
        public async Task<bool> Update(T entity)
        {
            
            _dbContext.Entry(entity).State = EntityState.Modified;
            
            return true;
        }

         /// <summary>
        /// Guarda todos los cambios
        /// </summary>
        public async Task<bool> SaveAll()
        {
            if (await _dbContext.SaveChangesAsync() > 0) {
                return true;
            }
            return false;
        }

         
    }
}
