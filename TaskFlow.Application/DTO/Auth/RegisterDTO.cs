using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.DTO.Auth
{
    public class RegisterDTO
    {
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }

    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(x => x.fullName).NotNull().NotEmpty();
            RuleFor(x => x.password).MinimumLength(8).Matches("[A-Z]").Matches("[0-9]").Matches("[a-z]").NotNull().NotEmpty();
            RuleFor(x => x.confirmPassword).Equal(x => x.password).WithMessage("Password and ConfirmPassword must be same");
            RuleFor(x => x.email).EmailAddress().NotNull().NotEmpty();
        }
    }
}
