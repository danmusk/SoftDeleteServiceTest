namespace SoftDeleteOneToOne.Infrastructure
{
    public interface ISingleSoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}