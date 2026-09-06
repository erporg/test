using ERP.ControlPlane.Tenants.Domain.AggregateRoots;
using Microsoft.EntityFrameworkCore;

namespace ERP.ControlPlane.Tenants.Infrastructure.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Tenant> Tenants => Set<Tenant>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSnakeCaseNamingConvention();
}
