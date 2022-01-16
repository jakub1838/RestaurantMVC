

using FluentValidation;
using RestaurantMVC.Data;
using System.Linq;

namespace RestaurantMVC.Models.Validators
{
    public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
    {
        public RegistrationDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(32).Custom((value, context) =>
                {
                    var usernameInUse = dbContext.Users.Any(u => u.Username == value);
                    if (usernameInUse)
                    {
                        context.AddFailure("Username", "Username is taken");
                    }
                });

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .MaximumLength(32);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "Email is taken");
                }
            });
        }
    }
}
