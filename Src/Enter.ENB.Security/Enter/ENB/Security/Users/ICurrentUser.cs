using System.Security.Claims;
using JetBrains.Annotations;

namespace Enter.ENB.Security.Users;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }

    [CanBeNull]
    Guid? Id { get; }

    string? UserName { get; }

    string? Name { get; }

    string? SurName { get; }

    string? PhoneNumber { get; }

    bool PhoneNumberVerified { get; }

    string? Email { get; }

    bool EmailVerified { get; }

    Guid? TenantId { get; }


    string[] Roles { get; }

    Claim? FindClaim(string claimType);

    Claim[] FindClaims(string claimType);

    Claim[] GetAllClaims();

    bool IsInRole(string roleName);
}