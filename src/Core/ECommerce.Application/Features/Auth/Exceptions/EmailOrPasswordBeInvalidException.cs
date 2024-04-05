using ECommerce.Application.Base;

namespace ECommerce.Application.Features.Auth.Exceptions;

public class EmailOrPasswordBeInvalidException : BaseException
{
    public EmailOrPasswordBeInvalidException() : base("U003")
    {
    }
}