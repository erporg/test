using ERP.ControlPlane.Tenants.Domain.AggregateRoots;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.ControlPlane.Tenants.Infrastructure.Data.Configuration;

public sealed class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);
    }
}