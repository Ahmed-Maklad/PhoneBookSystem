using Microsoft.EntityFrameworkCore;
using PhoneBook.Application.Contract;
using PhoneBook.Context;

namespace PhoneBook.Infrastructure;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private PhoneBookContext context;
    protected readonly DbSet<TEntity> dbset;
    public GenericRepository(PhoneBookContext _context)
    {
        context = _context;
        dbset = context.Set<TEntity>();
    }
    public async Task<TEntity> CreateAsync(TEntity Entity) => (await dbset.AddAsync(Entity)).Entity;

    public Task<TEntity> UpdateAsync(TEntity Entity) => Task.FromResult(dbset.Update(Entity).Entity);
    public Task<TEntity> DeleteAsync(TEntity Entity) => Task.FromResult(dbset.Remove(Entity).Entity);

    public IQueryable<TEntity> GetAllAsync() => dbset;


    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();

}
