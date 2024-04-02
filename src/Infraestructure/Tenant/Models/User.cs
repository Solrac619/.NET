

namespace Infraestructure.Tenant.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime? LockoutEnd { get; set; }

    }
}
