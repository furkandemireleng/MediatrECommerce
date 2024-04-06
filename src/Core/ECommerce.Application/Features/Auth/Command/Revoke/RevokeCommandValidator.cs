using FluentValidation;

namespace ECommerce.Application.Features.Auth.Command.Revoke;

public class RevokeCommandValidator : AbstractValidator<RevokeCommandRequest>
{
    public RevokeCommandValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
    }
}