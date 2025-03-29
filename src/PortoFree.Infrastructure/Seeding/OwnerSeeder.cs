using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortoFree.Application.Interfaces.Seeding;
using PortoFree.Domain.Constants;
using PortoFree.Domain.Entities;

namespace PortoFree.Infrastructure.Seeding;

internal class OwnerSeeder : IOwnerSeeder
{
    private readonly OwnerSettings _ownerSettings;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    
    public OwnerSeeder( IOptions<OwnerSettings> ownerSettingsOptions,
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager)
    {
        _ownerSettings = ownerSettingsOptions.Value;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task SeedAsync()
    {
        if (!_roleManager.Roles.Any())
        {
            var roles = GetRoles();
            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }
        }
        if (!_userManager.Users.Any())
        {
            var ownerInfo = GetOwnerInformation();
            var seedOwnerResult = await _userManager.CreateAsync(ownerInfo, _ownerSettings.Password);

            if (seedOwnerResult.Succeeded && _roleManager.Roles.Any())
            {
                var roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
                await _userManager.AddToRolesAsync(ownerInfo, roleNames!);
            }
        }
    }

    private static IEnumerable<IdentityRole<int>> GetRoles()
    {
        return
        [
            new IdentityRole<int>(UserRoles.Owner),
            new IdentityRole<int>(UserRoles.Admin),
            new IdentityRole<int>(UserRoles.User),
        ];
    }

    private User GetOwnerInformation()
    {
        return new User
        {
            Email = _ownerSettings.Email,
            UserName = _ownerSettings.Username,
        };
    }
}