using Application.Users;
using FluentValidation;

namespace Application.Validators.User;

public class CreateUserCommandValidator : BaseValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.TelegramId)
            .GreaterThan(MinIdValue)
            .WithMessage("TelegramId must be greater than 0");
    }
}