using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Core.Models;

namespace WebApi.Core.Interfaces
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<IEnumerable<Material>> FilterMaterialsByDate();
        Task<IEnumerable<Material>> FilterMatreerialsByType(int catId);
        Task<Material> GetMaterialById(Guid mId);
    }
}
