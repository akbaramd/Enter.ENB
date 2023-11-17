using Enter.ENB.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Enter.ENB.Identity.EntityFrameworkCore;

public static class IdentityEfCoreQueryableExtensions
{
    public static IQueryable<EntIdentityUser> IncludeDetails(this IQueryable<EntIdentityUser> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Roles);
    }

    public static IQueryable<EntIdentityRole> IncludeDetails(this IQueryable<EntIdentityRole> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(x => x.Claims);
    }
}