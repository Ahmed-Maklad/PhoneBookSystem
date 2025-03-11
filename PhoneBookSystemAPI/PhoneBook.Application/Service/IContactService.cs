using PhoneBook.DTOs.ContactDTOs;
using PhoneBook.DTOs.Shared;

namespace PhoneBook.Application.Service;
public interface IContactService
{
    public Task<ResultView<CUContactDTO>> CreateContactAsync(CUContactDTO entity);

    public Task<ResultView<CUContactDTO>> DeleteContactAsync(int id);

    public Task<ResultView<CUContactDTO>> UpdateContactAsync(CUContactDTO entity);

    public Task<ResultView<EntityPaginated<CUContactDTO>>> GetAllPaginatedAsync(int? PageSize, int? PageNumber);
    public Task<ResultView<List<CUContactDTO>>> SearchOnContact(string searchQuery);


}
