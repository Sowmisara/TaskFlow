using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TaskFlow.Application.DTO.Auth
{
    public class LoginDTO
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.password).MinimumLength(8).Matches("[A-Z]").Matches("[0-9]").Matches("[a-z]").NotNull().NotEmpty();
        }
    }
}
