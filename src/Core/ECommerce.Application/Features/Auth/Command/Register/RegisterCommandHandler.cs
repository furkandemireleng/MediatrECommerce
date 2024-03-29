using ECommerce.Application.Base;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Interfaces.AutoMapper;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Features.Auth.Command.Register;

public class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommandRequest, Unit>
{
    private readonly AuthRules _authRules;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;


    public RegisterCommandHandler(
        AuthRules authRules,
        IMapper mapper, IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager,
        RoleManager<Role> roleManager
    ) : base(mapper, unitOfWork, httpContextAccessor: httpContextAccessor)
    {
        _authRules = authRules;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        await _authRules.UserShouldBeNotExist(await _userManager.FindByEmailAsync(request.Email));

        User user = _mapper.Map<User, RegisterCommandRequest>(request);
        user.UserName = request.Email;
        user.SecurityStamp = Guid.NewGuid().ToString();

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);
        {
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("user"))
                {
                    await _roleManager.CreateAsync(new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });

                    await _userManager.AddToRoleAsync(user, "user");
                }
            }

            return Unit.Value;
        }
    }
}