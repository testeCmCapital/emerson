using Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace Domain.DomainInteface
{
    public interface IJwtDomain
    {
        string GenerateJwt(User user, List<Claim> claims, List<string> userRoles);
        double GetTokenDurationSeconds();
        public string GetAudience();
    }
}
