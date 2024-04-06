using ECommerce.Application.Base;
using ECommerce.Application.Features.Auth.Exceptions;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Auth.Rules;

public class AuthRules :  BaseRules
{

    public Task UserShouldBeNotExist(User? user)
    {
        if (user is not null) throw new UserAlreadyExistException();
        {
            return Task.CompletedTask;
        }
    }

    public Task EmailOrPasswordBeInvalid(User? user, bool chechkPassword)
    {
        if (user is null || !chechkPassword)
        {
            throw new EmailOrPasswordBeInvalidException();
        }

        return Task.CompletedTask;
    }
    
    public Task RefreshTokenShouldNotExpired(DateTime? expriDateTime)
    {
        if (expriDateTime <= DateTime.Now)
        {
            throw new RefreshTokenShouldNotExpiredException();
        }

        return Task.CompletedTask;
    }
    
    
}