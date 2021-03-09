using SoftDeleteOneToOne.Infrastructure;

namespace SoftDeleteOneToOne.Models
{
    public class Client : ISingleSoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ApplicationUser CreatedByUser { get; set; } = null!;
        public int CreatedByUserId { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}