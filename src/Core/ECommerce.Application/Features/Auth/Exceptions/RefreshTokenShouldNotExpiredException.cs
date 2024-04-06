using ECommerce.Application.Base;

namespace ECommerce.Application.Features.Auth.Exceptions;

public class RefreshTokenShouldNotExpiredException : BaseException
{
    public RefreshTokenShouldNotExpiredException() : base("U003")
    {
    }
}