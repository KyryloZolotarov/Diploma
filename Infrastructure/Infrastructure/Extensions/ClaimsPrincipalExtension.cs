using System.Security.Claims;
using IdentityModel;

namespace Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static CurrentUser GetClaims(this ClaimsPrincipal claims)
        {
            var user = new CurrentUser();
            var userClaims = claims.Claims.ToList();
            foreach (var claim in userClaims)
            {
                switch (claim.Type)
                {
                    case JwtClaimTypes.Subject:
                    case ClaimTypes.NameIdentifier:
                        user.Id = claim.Value;
                        break;
                    case ClaimTypes.GivenName:
                    case JwtClaimTypes.GivenName:
                        user.GivenName = claim.Value;
                        break;
                    case ClaimTypes.Name:
                    case JwtClaimTypes.Name:
                        user.Name = claim.Value;
                        break;
                    case JwtClaimTypes.FamilyName:
                    case ClaimTypes.Surname:
                        user.FamilyName = claim.Value;
                        break;
                    case JwtClaimTypes.Email:
                    case ClaimTypes.Email:
                    case ClaimTypes.Upn:
                        user.Email = claim.Value;
                        break;
                    case JwtClaimTypes.Address:
                    case ClaimTypes.StreetAddress:
                        user.Address = claim.Value;
                        break;
                }
            }

            return user;
        }
    }
}
