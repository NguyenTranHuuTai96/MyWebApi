using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AccountModel
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;    
        public string? Key { get; set; }
      

    }
    public class AccountModelValidator : AbstractValidator<AccountModel>
    {
     

        public AccountModelValidator()
        {


            RuleFor(m => m.UserName)
                .NotEmpty()
                .WithMessage("{PropertyName} is invalid");

            RuleFor(m => m.Password)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20)
                .WithMessage("Password is greater 3 and less than 20")
                .Must((password) =>
                {
                    if (password.Length < 3) { return false; }

                    return true;
                });
        }
    }
}
