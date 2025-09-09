using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerProduct.Model;
using Microsoft.EntityFrameworkCore;

namespace ManagerProduct
{
	public class AppDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }

		public static string DbPath { get; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "products.db");
		public AppDbContext() {
			//EnsureDeleted()
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
