using Domain.UoWInterface;
using Infrastructure.CrossCutting.Data.Context;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Data.UoW
{
    public partial class UnityOfWork : IUnityOfWork
    {
        private readonly IdentityContext _context;
        private readonly DapperContext _dapperContext;

        public UnityOfWork(IdentityContext context, DapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
