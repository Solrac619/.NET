using Finbuckle.MultiTenant.Stores;
using Infraestructure.Tenant.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Tenant
{
    public class TenantContext : EFCoreStoreDbContext<TenantInfo>
    {
        public TenantContext(DbContextOptions<TenantContext> options)
            : base(options)
        {

        }

        public DbSet<User> User => Set<User>();
        public DbSet<UserRoles> UserRoles => Set<UserRoles>();
        public DbSet<RoleClaims> RoleClaims => Set<RoleClaims>();
        public DbSet<UserClaims> UserClaims => Set<UserClaims>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TenantInfo>().ToTable("Company");
            modelBuilder.Entity<UserRoles>().HasNoKey();
        }
    }

}
