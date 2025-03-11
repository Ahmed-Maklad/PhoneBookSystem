using Microsoft.AspNetCore.Mvc;
using PhoneBook.Application.Service;
using PhoneBook.DTOs.ContactDTOs;
using PhoneBook.DTOs.Shared;

namespace PhoneBookSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;
    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }
    [HttpPost("Create_Contact")]
    public async Task<IActionResult> CreateContact([FromBody] CUContactDTO contactDTO)
    {

        ResultView<CUContactDTO> result = await _contactService.CreateContactAsync(contactDTO);
        return result.IsSuccess ? Ok(result) : BadRequest(result);

    }

    [HttpPut("Update_Contact")]
    public async Task<IActionResult> UpdateContact([FromBody] CUContactDTO contactDTO)
    {

        ResultView<CUContactDTO> result = await _contactService.UpdateContactAsync(contactDTO);
        return result.IsSuccess ? Ok(result) : BadRequest(result);

    }

    [HttpDelete("Delete_Contact/{id}")]
    public async Task<IActionResult> DeleteContact([FromRoute] int id)
    {

        ResultView<CUContactDTO> result = await _contactService.DeleteContactAsync(id);
        return result.IsSuccess ? Ok(result) : NotFound(result);

    }

    [HttpGet("Get_All_Contact")]
    public async Task<IActionResult> GetAllContacts([FromQuery] int? pageSize, [FromQuery] int? pageNumber)
    {

        ResultView<EntityPaginated<CUContactDTO>> result = await _contactService.GetAllPaginatedAsync(pageSize, pageNumber);
        return Ok(result);

    }

    [HttpGet("Search_For_Contact")]
    public async Task<IActionResult> SearchContacts([FromQuery] string query)
    {

        ResultView<List<CUContactDTO>> result = await _contactService.SearchOnContact(query);
        return result.IsSuccess ? Ok(result) : NotFound(result);

    }
}
