using Domain.DomainInteface;
using Domain.DomainServiceInterface;
using Domain.Entities;
using Domain.Models.Request;
using Domain.Models.Response;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Domain.DomainService
{
    public class AuthDomainServices : IAuthDomainServices
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IJwtDomain _jwtDomain;

        public AuthDomainServices(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IJwtDomain jwtDomain)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtDomain = jwtDomain;
        }

        public async Task<LoginResponseModel> LoginAsync(LoginRequest loginModel)
        {           
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            return GenereateLoginResponseModel(user, claims, userRoles);
        }


        private LoginResponseModel GenereateLoginResponseModel(User user, IList<Claim> claims, IList<string> userRoles)
        {
            var encodedToken = _jwtDomain.GenerateJwt(user, claims.ToList(), userRoles.ToList());

            return new LoginResponseModel
            {
                AccessToken = encodedToken,
                ExpiresIn = _jwtDomain.GetTokenDurationSeconds(),
                UserToken = new UserTokenViewModel
                {
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value }),
                    User = new UserInfo
                    {
                        Email = user.Email,
                        Id = user.Id,
                        Name = user.Name,
                    }
                }
            };
        }

    }
}
