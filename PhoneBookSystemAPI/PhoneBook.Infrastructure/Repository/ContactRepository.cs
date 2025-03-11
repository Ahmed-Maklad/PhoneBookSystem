using Microsoft.EntityFrameworkCore;
using PhoneBook.Application.Contract;
using PhoneBook.Context;
using PhoneBook.Model;


namespace PhoneBook.Infrastructure
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(PhoneBookContext _context) : base(_context)
        {
        }
        public async Task<List<Contact>> GetContactBySearchAsync(string searchQuery)
        {
            // Convert SearchQuery To Lower-Case To Ignore Case-sensitive
            string normalizeSearchQuery = searchQuery.ToLower();

            // Use AsNoTracking Because Not Track Change By Change Tracker 
            var contacts = await dbset.Where(c =>
                          EF.Functions.ILike(c.Name, $"%{searchQuery}%") ||
                          EF.Functions.ILike(c.PhoneNumber, $"%{searchQuery}%") ||
                          EF.Functions.ILike(c.Email, $"%{searchQuery}%")).AsNoTracking()
                          .ToListAsync();

            return contacts;
        }

        public async Task<List<Contact>> EntityPaginatedAsync(int PageNumber, int PageSize)
        {

            return await dbset
                 .Skip((PageNumber - 1) * PageSize)
                 .Take(PageSize)
                 .AsNoTracking()
                 .ToListAsync();
        }

    }
}
