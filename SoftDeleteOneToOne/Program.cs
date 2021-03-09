using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoftDeleteOneToOne.Infrastructure;
using SoftDeleteOneToOne.Models;
using SoftDeleteServices.Concrete;

namespace SoftDeleteOneToOne
{
	class Program
	{
		static void Main()
		{
			int clientId;

			using (var dbContext = new MyDbContext())
			{
				dbContext.Database.Migrate();
				var client = new Client()
				{
					Name = Guid.NewGuid().ToString(),
					CreatedByUserId = 1 // Seeded in DbContext
				};
				dbContext.Add(client);
				dbContext.SaveChanges();

				clientId = client.Id;
			}

			using (var dbContext = new MyDbContext())
			{
				var dbClient = dbContext.Clients.First(x => x.Id == clientId);
				var softDeleteConfiguration = new ConfigSoftDeleted(dbContext);
				var deleteService = new SingleSoftDeleteService<ISingleSoftDelete>(softDeleteConfiguration);

				try
				{
					deleteService.SetSoftDelete(dbClient);
					var deletedDbClient = dbContext.Clients.SingleOrDefault(x => x.Id == clientId);
					if (deletedDbClient == null)
						Console.WriteLine("*** Soft-Deleted Client success ***");
					else
						Console.WriteLine("*** Soft-Deleted Failed ***");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"*** Soft-Deleted Client failed: {ex.Message}  ***");
				}
			}
		}
	}


}
