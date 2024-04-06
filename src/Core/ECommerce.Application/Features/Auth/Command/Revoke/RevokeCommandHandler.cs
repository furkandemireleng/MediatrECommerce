using ECommerce.Application.Base;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Interfaces.AutoMapper;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;

namespace ECommerce.Application.Features.Auth.Command.Revoke;

public class RevokeCommandHandler : BaseHandler, IRequestHandler<RevokeCommandRequest, Unit>
{
    private readonly UserManager<User> _userManager;
    private readonly AuthRules _authRules;

    public RevokeCommandHandler(UserManager<User> userManager, AuthRules authRules, IMapper mapper,
        IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork,
        httpContextAccessor)
    {
        _userManager = userManager;
        _authRules = authRules;
    }


    public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByEmailAsync(request.Email);
        await _authRules.EmailAddressShoulBeValid(user);

        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);

        return Unit.Value;
    }
}