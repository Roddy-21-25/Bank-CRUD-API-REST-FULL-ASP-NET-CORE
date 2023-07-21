using AplicationDomain.Layer___Bank_Api.Entities;
using AplicationDomain.Layer___Bank_Api.Interfacez;
using Infraestructure.Layer___Bank_Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Infraestructure.Layer___Bank_Api.Repository
{
    public class BaseCrudRepository<T> : ICrudRepository<T> where T : BaseIdEntity
    {
        protected readonly BANKContext _bANKContext;
        protected readonly DbSet<T> _dbSet;
        public BaseCrudRepository(BANKContext bANKContext)
        {
            _bANKContext = bANKContext;
            _dbSet = bANKContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Insert(T entity)
        {
            _dbSet.Add(entity);
            await _bANKContext.SaveChangesAsync(); 
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _bANKContext.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            T entity = await GetById(id);
            _dbSet.Remove(entity);
            await _bANKContext.SaveChangesAsync();
            return true;
        }
    }
}
