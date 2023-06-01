using System;
using System.Threading.Tasks;

namespace Domain.UoWInterface
{
    public interface IUnityOfWork : IDisposable
    {
        bool Commit();
        Task<bool> CommitAsync();

    }
}
