using ManagerProduct.Model;

namespace ManagerProduct.View;

public partial class OrderDetail : ContentPage
{
	public OrderDetail(Order order)
	{
		InitializeComponent();

		if (string.IsNullOrEmpty(order.CustomerName))
		{
			using var context = new AppDbContext();
			var customer = context.Customers.Find(order.CustomerId);
			if (customer != null)
			{
				order.CustomerName = customer.Name;
			}
		}

		BindingContext = order;
	}
}
