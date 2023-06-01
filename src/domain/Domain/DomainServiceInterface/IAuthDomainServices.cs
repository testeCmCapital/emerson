using Domain.Models.Request;
using Domain.Models.Response;
using System.Threading.Tasks;

namespace Domain.DomainServiceInterface
{
    public interface IAuthDomainServices
    {
        Task<LoginResponseModel> LoginAsync(LoginRequest loginModel);
    }
}
