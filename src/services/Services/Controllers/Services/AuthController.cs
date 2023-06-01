using Domain.DomainServiceInterface;
using Domain.Models.Request;
using Infrastructure.CrossCutting.Helpers.Messages;
using Microsoft.AspNetCore.Mvc;
using Services.Controllers.Base;
using System;
using System.Threading.Tasks;

namespace Services.Controllers.Services
{
    [Route("api/Auth")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthDomainServices _authDomainServices;

        public AuthController(IAuthDomainServices authDomainServices)
        {
            _authDomainServices = authDomainServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginModel)
        {
            if (!ModelState.IsValid)
                return CreateInsuccessModelStateResponse(ModelState);

            try
            {
                return CreateAnyResponse(await _authDomainServices.LoginAsync(loginModel), Messages._default);
            }
            catch (ApplicationException ae)
            {
                Console.WriteLine(ae.Message);
                return CreateInsuccessResponse(ae.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CreateErroResponse(e.Message);
            }
        }

    }
}
