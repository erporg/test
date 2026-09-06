using ERP.ControlPlane.Tenants.Application.Data;
using ERP.ControlPlane.Tenants.Domain.AggregateRoots;
using ERP.ControlPlane.Tenants.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.ControlPlane.Tenants.Infrastructure.Repositories;

public sealed class TenantRepository(ApplicationDbContext applicationDbContext) : ITenantRepository
{
    public async Task<Tenant> GetTenantByIdAsync(Guid tenantId, CancellationToken cancellationToken)
    {
        return await applicationDbContext.Tenants.SingleOrDefaultAsync(x => x.Id == tenantId, cancellationToken);
    }
}