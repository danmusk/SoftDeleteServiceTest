using Microsoft.EntityFrameworkCore.Design;

namespace SoftDeleteOneToOne
{
	public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
	{
		public MyDbContext CreateDbContext(string[] args)
		{
			return new MyDbContext();
		}
	}
}