using System;
using System.Collections.Generic;

namespace Domain.Models.Response
{
    public class UserTokenViewModel
    {        
        public IEnumerable<ClaimViewModel> Claims { get; set; }
        public UserInfo User { get; set; }
    }

    public class LoginResponseModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public Guid RefreshToken { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }

    public class ClaimViewModel
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

    }
}
