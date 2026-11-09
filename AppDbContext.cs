using ManagerProduct.Model;
using Microsoft.EntityFrameworkCore;

namespace ManagerProduct
{
	public class AppDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetailModel> OrderDetails { get; set; }

		public AppDbContext()
		{
			Database.EnsureCreated();
		}

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptions)
		{
			if (!dbContextOptions.IsConfigured)
			{
				const string fileName = "products.db";
				var dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, fileName);
				dbContextOptions.UseSqlite($"Filename={dbPath}");
			}
		}
	}
}
