using System;

namespace Domain.Auth
{
    public interface IUserLogged
    {
        Guid? GuidID { get; }
        int? ID { get; }
        int? CompanyID { get; }
        string Email { get; }
        bool IsAuthenticated { get; }
    }
}
