using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Core.Interfaces;
using WebApi.Core.Models;

namespace WebApi.DB.Repositories
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(DbContext context) : base(context)
        {
        }

        private WebApiDbContext WebApiDbContext
        {
            get { return Context as WebApiDbContext; }
        }

        public async Task<IEnumerable<Material>> FilterMaterialsByDate()
        {
            return await WebApiDbContext.Materials
                .OrderByDescending(m => m.MaterialDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Material>> FilterMatreerialsByType(int catId)
        {
            return await WebApiDbContext.Materials
                .OrderByDescending(m => m.MatCategoryId == catId)
                .ToListAsync();
        }

        public async Task<Material> GetMaterialById(Guid mId)
        {
            return await WebApiDbContext.Materials.FindAsync();
        }
    }
}
