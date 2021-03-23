using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Core.Models;

namespace WebApi.Core.Interfaces
{
    public interface IMaterialVersionRepository : IRepository<MaterialVersion>
    {
        Task<IEnumerable<MaterialVersion>> GetFilterVersionsByDate(Guid mId);
        Task<IEnumerable<MaterialVersion>> GetFilterVersionsBySize(Guid mId);
    }
}
