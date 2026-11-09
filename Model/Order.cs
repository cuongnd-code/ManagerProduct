using System;
using System.Collections.Generic;

namespace ManagerProduct.Model
{
	public class Order
	{
		public int OrderId { get; set; }
		public DateTime OrderDate { get; set; }
		public int CustomerId { get; set; }
		public string? CustomerName { get; set; }
		public List<OrderDetailModel>? OrderDetails { get; set; }
		public double TotalAmount { get; set; } = 0;
	}
}
