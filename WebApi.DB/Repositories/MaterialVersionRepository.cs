using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Core.Interfaces;
using WebApi.Core.Models;

namespace WebApi.DB.Repositories
{
    public class MaterialVersionRepository : Repository<MaterialVersion>, IMaterialVersionRepository
    {
        public MaterialVersionRepository(DbContext context) : base(context)
        {
        }

        private WebApiDbContext WebApiDbContext
        {
            get { return Context as WebApiDbContext; }
        }

        public async Task<IEnumerable<MaterialVersion>> GetFilterVersionsByDate(Guid mId)
        {
            return await WebApiDbContext.MaterialVersions.Where(m => m.Material.Id == mId)
                .OrderByDescending(m => m.FileDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<MaterialVersion>> GetFilterVersionsBySize(Guid mId)
        {
            return await WebApiDbContext.MaterialVersions.Where(m => m.Material.Id == mId)
                .OrderByDescending(m => m.Size)
                .ToListAsync();
        }
    }
}
