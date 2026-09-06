namespace ERP.ControlPlane.Tenants.Domain.AggregateRoots;

public sealed class Tenant
{
    private Tenant()
    {
    }

    private Tenant(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; init; }
    
    public string Name { get; private set; } = null!;

    public static Tenant Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new Tenant(Guid.CreateVersion7(), name);
    }
}