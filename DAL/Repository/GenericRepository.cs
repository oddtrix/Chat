using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using DAL.Contexts;

namespace DAL.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : Base
    {
        private readonly DomainContext context;

        public GenericRepository(DomainContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await this.context.AddAsync(entity);
        }

        public void Delete(TEntity entityToDelete)
        {
            this.context.Remove(entityToDelete);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await this.context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public TEntity Update(TEntity entity)
        {
            this.context.Update(entity);
            return entity;
        }
    }
}
