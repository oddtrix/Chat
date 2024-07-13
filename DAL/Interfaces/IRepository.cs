using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Base
    {
        TEntity Update(TEntity entity);

        Task CreateAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(Guid id);
        
        void Delete(TEntity entityToDelete);

        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
