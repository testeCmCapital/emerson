using Domain.Auth;
using Microsoft.AspNetCore.Http;
using System;

namespace Services.Extensions
{
    public class UserLogged : IUserLogged
    {
        private readonly IHttpContextAccessor _accessor;

        public UserLogged(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

#if !DEBUG
        public bool IsAuthenticated => _accessor.HttpContext.User.Identity.IsAuthenticated;
        public string Email => IsAuthenticated ? _accessor.HttpContext.User.GetUserEmail() : "";
        public Guid? GuidID => IsAuthenticated ? _accessor.HttpContext.User.GetUserGuidID() : null;
        public int? ID => IsAuthenticated ? _accessor.HttpContext.User.GetUserID() : null;        
        public int? CompanyID => IsAuthenticated ? _accessor.HttpContext.User.GetCompanyID() : null;
#endif

#if DEBUG
        public bool IsAuthenticated => true;
        public string Email => "emerson.dsantos@outlook.com";
        public Guid? GuidID => Guid.Parse("A08388EC-A7BE-4468-8543-713A8EF1ED59");
        public int? ID => 1;
        public int? CompanyID => 1;
#endif

    }
}
