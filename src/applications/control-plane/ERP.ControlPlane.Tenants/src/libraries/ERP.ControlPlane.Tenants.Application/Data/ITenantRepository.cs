using ERP.ControlPlane.Tenants.Domain.AggregateRoots;

namespace ERP.ControlPlane.Tenants.Application.Data;

public interface ITenantRepository
{
    Task<Tenant> GetTenantByIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
}
