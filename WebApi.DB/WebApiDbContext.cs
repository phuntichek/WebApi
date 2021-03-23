using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Core.Models;

namespace WebApi.DB
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {
        }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialVersion> MaterialVersions { get; set; }
    }
}
