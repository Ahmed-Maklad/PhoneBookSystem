namespace PhoneBook.Application.Contract
{
    public interface IGenericRepository<TEntity>
    {
        public Task<TEntity> CreateAsync(TEntity Entity);
        public Task<TEntity> UpdateAsync(TEntity Entity);

        public Task<TEntity> DeleteAsync(TEntity Entity);
        public IQueryable<TEntity> GetAllAsync();
        public Task<int> SaveChangesAsync();
    }
}
