namespace EmpTrack.Domain.Auditing
{
    public abstract class FullAuditableEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
