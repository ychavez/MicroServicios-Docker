namespace Ordering.Domain.Common
{
    public interface IMultiTenant
    {
        public Guid TenantId { get; set; }
    }
}
