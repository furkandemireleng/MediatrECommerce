using ECommerce.Application.Base;
using ECommerce.Application.Interfaces.AutoMapper;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Features.Auth.Command.Login;

public class LoginCommandHandler: BaseHandler , IRequestHandler<LoginCommandRequest,LoginCommandResponse>
{
    private readonly UserManager<User> _userManager;
    
    public LoginCommandHandler(UserManager<User> userManager,IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
    {
        _userManager = userManager;
    }
    

    public Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}