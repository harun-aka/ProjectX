using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserRegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Password).MinimumLength(8);
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
        }
    }
}
