using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Domain.Contracts;
using Finbuckle.MultiTenant;
using Infraestructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infraestructure.Persistence
{
    public abstract class BaseDbContext : MultiTenantDbContext
    {

        protected readonly ICurrentUserService _currentUser;

        protected BaseDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUserService currentUser)
            : base(currentTenant, options)
        {
            _currentUser = currentUser;
        }

        // Used by Dapper
        public IDbConnection Connection => Database.GetDbConnection();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: We want this only for development probably... maybe better make it configurable in logger.json config?
            optionsBuilder.EnableSensitiveDataLogging();

            // If you want to see the sql queries that efcore executes:

            // Uncomment the next line to see them in the output window of visual studio
            // optionsBuilder.LogTo(m => Debug.WriteLine(m), LogLevel.Information);

            // Or uncomment the next line if you want to see them in the console
            // optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            if (!string.IsNullOrWhiteSpace(TenantInfo?.ConnectionString))
            {
                optionsBuilder.UseSqlServer(TenantInfo.ConnectionString);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                foreach (var entry in ChangeTracker.Entries<AuditableBaseEntry>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.Created = DateTime.Now;
                            entry.Entity.CreatedBy = _currentUser.GetUserId() ?? 0;
                            break;
                        case EntityState.Modified:
                            entry.Entity.Modified = DateTime.Now;
                            entry.Entity.ModifiedBy = _currentUser.GetUserId();
                            break;
                    }
                }

                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    entry.State = EntityState.Detached;
                }
                throw;
            }
        }
    }
}
