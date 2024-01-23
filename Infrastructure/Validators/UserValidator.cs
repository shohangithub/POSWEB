using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    internal class UserValidator : AbstractValidator<UserRequest>
    {
        public UserValidator()
        {
            RuleFor(cmd => cmd.UserName).NotNull().MinimumLength(3);
        }
    }
}