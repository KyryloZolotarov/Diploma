using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services;

public class IdentityParser : IIdentityParser<ApplicationUser>
{
    public ApplicationUser Parse(IPrincipal principal)
    {
        // Pattern matching 'is' expression
        // assigns "claims" if "principal" is a "ClaimsPrincipal"
        if (principal is ClaimsPrincipal claims)
        {
            return new ApplicationUser
            {

                Name = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "" + claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value ?? "",
            };
        }
        throw new ArgumentException(message: "The principal must be a ClaimsPrincipal", paramName: nameof(principal));
    }
}