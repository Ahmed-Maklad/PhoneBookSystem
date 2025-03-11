using FluentValidation;
using PhoneBook.DTOs.ContactDTOs;

namespace PhoneBook.Application.Validator;
public class ContactValidator : AbstractValidator<CUContactDTO>
{
    public ContactValidator()
    {
        
    RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

    RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Invalid phone number format.");

    RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
