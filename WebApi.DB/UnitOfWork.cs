using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Core.Interfaces;
using WebApi.DB.Repositories;

namespace WebApi.DB
{
    public class UnitOfWork : IUnitOfWork
    {
        private WebApiDbContext _context;
        private MaterialVersionRepository _materialVersionRepository;
        private MaterialRepository _materialRepository;

        public UnitOfWork(WebApiDbContext context)
        {
            this._context = context;
        }

         public IMaterialVersionRepository MaterialVersions
        {
            get
            {
                _materialVersionRepository = _materialVersionRepository
                ?? new MaterialVersionRepository(_context);
                return _materialVersionRepository;
            }
        }

         public IMaterialRepository Materials
        {
            get
            {
                _materialRepository = _materialRepository
                ?? new MaterialRepository(_context);
                return _materialRepository;
            }
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
