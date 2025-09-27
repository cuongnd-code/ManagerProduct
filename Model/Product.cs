using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManagerProduct.Model
{
	public class Product
	{
		
		public int Id { get; set; }
		
		public string? Name { get; set; }

		public string? Description { get; set; }

		public string? Category { get; set; }

		public string? Image { get; set; }

		public double Price { get; set; }

		public int? Stock { get; set; }


		//public event PropertyChangedEventHandler? PropertyChanged;
		//protected void OnPropertyChanged(string propertyName) =>
		//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	}
}
