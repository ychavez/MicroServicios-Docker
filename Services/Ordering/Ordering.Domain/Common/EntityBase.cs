namespace Ordering.Domain.Common
{
    public class EntityBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string? lastModifiedBy { get; set; }
        public DateTime? lastModifiedDate { get; set; }
    }
}
