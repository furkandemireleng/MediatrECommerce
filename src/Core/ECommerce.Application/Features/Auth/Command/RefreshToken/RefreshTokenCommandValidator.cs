using FluentValidation;

namespace ECommerce.Application.Features.Auth.Command.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandRequest>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty().NotNull();
        RuleFor(x => x.RefresToken).NotEmpty().NotNull();
    }
}