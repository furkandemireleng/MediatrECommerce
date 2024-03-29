using FluentValidation;

namespace ECommerce.Application.Features.Auth.Command.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(55)
            .WithName("Full Name")
            .NotNull();

        RuleFor(x => x.Email)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(55)
            .EmailAddress()
            .WithName("email")
            .NotNull();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(55)
            .WithName("password")
            .NotNull();

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(55)
            .WithName("ConfirmPassword")
            .NotNull();

        RuleFor(x => x.ConfirmPassword)
            .Equal(ops => ops.Password);
    }
}