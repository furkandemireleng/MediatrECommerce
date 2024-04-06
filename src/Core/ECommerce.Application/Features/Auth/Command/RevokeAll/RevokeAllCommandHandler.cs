using ECommerce.Application.Base;
using ECommerce.Application.Interfaces.AutoMapper;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Auth.Command.RevokeAll;

public class RevokeAllCommandHandler : BaseHandler, IRequestHandler<RevokeAllCommandRequest, Unit>
{
    private readonly UserManager<User> _userManager;

    public RevokeAllCommandHandler(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) :
        base(mapper, unitOfWork, httpContextAccessor)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(RevokeAllCommandRequest request, CancellationToken cancellationToken)
    {
        List<User> users = await _userManager.Users.ToListAsync(cancellationToken);
        foreach (var user in users)
        {
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        return Unit.Value;
    }
}