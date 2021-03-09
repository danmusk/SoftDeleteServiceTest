using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

	public class ClientEntityConfiguration : IEntityTypeConfiguration<Client>
	{
		public void Configure(EntityTypeBuilder<Client> builder)
		{
			builder.HasOne(x => x.CreatedByUser).WithMany();
		}
	}
}