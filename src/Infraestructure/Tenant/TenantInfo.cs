using Finbuckle.MultiTenant;

namespace Infraestructure.Tenant
{
    public class TenantInfo : ITenantInfo
    {
        public TenantInfo()
        {
        }

        public TenantInfo(string id, string name, string? connectionString)
        {
            Id = id;
            Identifier = id;
            Name = name;
            ConnectionString = connectionString ?? string.Empty;
            Active = true;
        }

        /// <summary>
        /// The actual TenantId, which is also used in the TenantId shadow property on the multitenant entities.
        /// </summary>
        public string Id { get; set; } = default!;
        public string Identifier { get; set; } = default!;
        /// <summary>
        /// The identifier that is used in headers/routes/querystrings. This is set to the same as Id to avoid confusion.
        /// </summary>

        public string Name { get; set; } = default!;
        public string ConnectionString { get; set; } = default!;

        public bool Active { get; private set; }

        string? ITenantInfo.Id { get => Id; set => Id = value.ToString() ?? throw new InvalidOperationException("Identifier can't be null."); }
        string? ITenantInfo.Identifier { get => Identifier; set => Identifier = value ?? throw new InvalidOperationException("Identifier can't be null."); }
        string? ITenantInfo.Name { get => Name; set => Name = value ?? throw new InvalidOperationException("Name can't be null."); }
        string? ITenantInfo.ConnectionString { get => ConnectionString; set => ConnectionString = value ?? throw new InvalidOperationException("ConnectionString can't be null."); }
    }
}
