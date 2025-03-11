using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Application.Contract;
using PhoneBook.DTOs.ContactDTOs;
using PhoneBook.DTOs.Shared;
using PhoneBook.Model;




namespace PhoneBook.Application.Service;
public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CUContactDTO> _validator;

    public ContactService(IContactRepository contactRepository, IValidator<CUContactDTO> validator, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<ResultView<CUContactDTO>> CreateContactAsync(CUContactDTO entity)
    {
        ResultView<CUContactDTO> resultView = new();
        try
        {

            var validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid) //I will make sure that the values are suitable or not before creation Using Fluent Validation 
            {
                resultView.IsSuccess = false;
                resultView.Data = null;

                // Here I Will Select Messages Based On the mistakes That Happened From The User
                resultView.Msg = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                return resultView;
            }

            bool existingContact = await _contactRepository.GetAllAsync()
                                                           .AsNoTracking()
                                                           .AnyAsync(p => p.PhoneNumber == entity.PhoneNumber);
            // If Searching For Contact By Id Is Exists
            if (existingContact)
            {
                resultView.IsSuccess = false;
                resultView.Msg = $"A Contact: ( {entity.Name} ) with the same Phone Number already exists.";
                resultView.Data = null;
                return resultView;
            }

            Contact contact = _mapper.Map<Contact>(entity);
            Contact ContactCreated = await _contactRepository.CreateAsync(contact);

            if (ContactCreated == null)
            {
                // If Contact Updated Is Faild

                resultView.IsSuccess = false;
                resultView.Msg = $"Contact: ( {entity.Name} ) Creation Is Failed Sorry.";
                resultView.Data = null;
                return resultView;
            }
            // If Contact Created Is Successfully

            await _contactRepository.SaveChangesAsync();
            resultView.IsSuccess = true;
            resultView.Msg = $"Contact: {entity.Name} Created Is Successfully.";
            resultView.Data = _mapper.Map<CUContactDTO>(ContactCreated);

            return resultView;
        }
        catch (Exception ex)
        {
            resultView.IsSuccess = false;
            resultView.Msg = $"Error Happened While Creating Contact: ( ${entity.Name} ) ${ex.Message}";
            resultView.Data = null;
            return resultView;
        }
    }
    public async Task<ResultView<CUContactDTO>> UpdateContactAsync(CUContactDTO entity)
    {
        ResultView<CUContactDTO> ResultView = new();
        try
        {
            var validationResult = await _validator.ValidateAsync(entity);
            if (!validationResult.IsValid)
            {
                ResultView.IsSuccess = false;
                ResultView.Data = null;
                ResultView.Msg = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                return ResultView;
            }
            bool existingContact = await _contactRepository.GetAllAsync()
                                                            .AsNoTracking()
                                                            .AnyAsync(p => p.Id == entity.Id);

            // If Searching For Contact By Id Return Contact Valid
            if (existingContact)
            {
                Contact contact = _mapper.Map<Contact>(entity);

                Contact ContactUpdated = await _contactRepository.UpdateAsync(contact);

                if (ContactUpdated is not null)
                {
                    // If Contact Updated Is Successfully
                    ResultView.IsSuccess = true;
                    ResultView.Data = _mapper.Map<CUContactDTO>(ContactUpdated);
                    ResultView.Msg = $"Contect: ( {entity.Name} ) Is Successfully for Updated.";

                    await _contactRepository.SaveChangesAsync();
                    return ResultView;
                }
                else
                {
                    // If Contact Updated Is Faild
                    ResultView.IsSuccess = false;
                    ResultView.Data = null;
                    ResultView.Msg = $"Contect: ( {entity.Name} ) for Updated Is Failed.";
                    return ResultView;
                }
            }
            else
            {
                ResultView.IsSuccess = false;
                ResultView.Data = null;
                ResultView.Msg = $"This Contect: ( {entity.Name} ) Not Found .";
                return ResultView;

            }

        }
        catch (Exception ex)
        {
            ResultView.IsSuccess = false;
            ResultView.Msg = $"Error Happened While Updated Contact: ( ${entity.Name} ) ${ex.Message}";
            ResultView.Data = null;
            return ResultView;
        }
    }

    public async Task<ResultView<CUContactDTO>> DeleteContactAsync(int id)
    {
        ResultView<CUContactDTO> ResultView = new();
        try
        {
            // Check If ID Not Valid Or Equel 0
            if (id == 0)
            {
                ResultView.IsSuccess = false;
                ResultView.Data = null;
                ResultView.Msg = "This Contact Id Not Valid .";
                return ResultView;
            }

            // Search For Contact By ID 
            var contact = await _contactRepository.GetAllAsync()
                                                .FirstOrDefaultAsync(p => p.Id == id);
            if (contact is not null)
            {
                // If Searching For Contact By Id Return Contact Valid
                Contact ContactDeleted = await _contactRepository
                                           .DeleteAsync(contact);

                await _contactRepository.SaveChangesAsync();
                ResultView.IsSuccess = true;
                ResultView.Data = _mapper.Map<CUContactDTO>(ContactDeleted);
                ResultView.Msg = $"Contact: ( {ContactDeleted.Name} ) Is Deleted Successfully.";

                return ResultView;

            }
            else
            {
                // If Contact Searching By Id Not Found
                ResultView.IsSuccess = false;
                ResultView.Data = null;
                ResultView.Msg = "This Contact Not Found.";
                return ResultView;
            }

        }
        catch (Exception ex)
        {
            ResultView.IsSuccess = false;
            ResultView.Data = null;
            ResultView.Msg = $"Error Happened While Hard Deleted Contact: ( ${ex.Message} )";
            return ResultView;
        }
    }


    public async Task<ResultView<EntityPaginated<CUContactDTO>>> GetAllPaginatedAsync(int? PageSize, int? PageNumber)
    {
        // Set Default Values If Page Number && Page Size Is Null Or Equel 0

        int Page = PageNumber == 0 ? 1 : (PageNumber ?? 1);
        int Size = PageSize == 0 ? 10 : (PageSize ?? 10);

        List<Contact> contacts = await _contactRepository
                                     .EntityPaginatedAsync(Page, Size) ?? new List<Contact>();

        int TotalCountContact = await _contactRepository
                                      .GetAllAsync()
                                      .AsNoTracking()
                                      .CountAsync();

        ResultView<EntityPaginated<CUContactDTO>> resultView = new()
        {
            IsSuccess = true,
            Msg = contacts.Any() ? "Contact: Fetched Successfully." : "Contacts Not Found.",
            Data = new EntityPaginated<CUContactDTO>()
            {
                Data = _mapper.Map<List<CUContactDTO>>(contacts),
                Count = TotalCountContact
            }
        };
        return resultView;
    }
    public async Task<ResultView<List<CUContactDTO>>> SearchOnContact(string searchQuery)
    {
        try
        {

            // Check On SearchQuery If Null Or String Empty
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return new ResultView<List<CUContactDTO>>
                {
                    IsSuccess = false,
                    Msg = "Search query cannot be empty.",
                    Data = null
                };
            }

            // Get All Contact By Search Query
            List<Contact> contacts = await _contactRepository.GetContactBySearchAsync(searchQuery);

            // If Not Value Match Search Query 
            if (contacts == null || !contacts.Any())
            {
                return new ResultView<List<CUContactDTO>>
                {
                    IsSuccess = false,
                    Msg = "No contacts found.",
                    Data = null
                };
            }


            List<CUContactDTO> contactDTOs = _mapper.Map<List<CUContactDTO>>(contacts);

            return new ResultView<List<CUContactDTO>>
            {
                IsSuccess = true,
                Msg = $"Contacts: found successfully.",
                Data = contactDTOs
            };
        }
        catch (Exception ex)
        {
            return new ResultView<List<CUContactDTO>>()
            {
                IsSuccess = false,
                Msg = $"Error Happened While Search On Contact: ${ex.Message}",
                Data = null
            };
        }
    }



}
