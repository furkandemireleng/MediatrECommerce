using ECommerce.Application.Base;

namespace ECommerce.Application.Features.Auth.Exceptions;

public class UserAlreadyExistException: BaseException
{
    public UserAlreadyExistException() : base("U002") { }
}