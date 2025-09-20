
using ManagerProduct.Model;
using Microsoft.EntityFrameworkCore;

namespace ManagerProduct
{
	public class AppDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public static string DbPath { get; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "products.db");
		
		public AppDbContext() {
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}
		public AppDbContext(string path) { }


		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptions) 
		{
			const string FileName = "products.db";
			dbContextOptions.UseSqlite($"Filename={System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName)}");


		}
       

    }
}
