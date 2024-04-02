namespace Domain.Contracts
{
    public abstract class AuditableBaseEntry
    {
        public int CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
