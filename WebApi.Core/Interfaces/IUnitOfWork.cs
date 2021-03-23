using System;
using System.Threading.Tasks;

namespace WebApi.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMaterialVersionRepository MaterialVersions { get; }
        IMaterialRepository Materials { get; }
        Task<int> CommitAsync();
    }
}
