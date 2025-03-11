using PhoneBook.Model;

namespace PhoneBook.Application.Contract;
public interface IContactRepository : IGenericRepository<Contact>
{
    public Task<List<Contact>> GetContactBySearchAsync(string searchQuery);
    public Task<List<Contact>> EntityPaginatedAsync(int PageNumber, int PageSize);

}

