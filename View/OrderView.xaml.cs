using ManagerProduct.Model;
using Microsoft.EntityFrameworkCore;

namespace ManagerProduct.View;

public partial class OrderView : ContentPage
{
	public OrderView()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		LoadOrders();
	}

	private void LoadOrders()
	{
		using var context = new AppDbContext();
		var customers = context.Customers.ToDictionary(c => c.Id, c => c.Name);
		var orders = context.Orders.Include(c => c.OrderDetails).OrderByDescending(o => o.OrderId).ToList();

		foreach (var order in orders)
		{
			if (string.IsNullOrEmpty(order.CustomerName) && customers.TryGetValue(order.CustomerId, out var name))
			{
				order.CustomerName = name;
			}
		}

		OrderCollectionView.ItemsSource = orders;
	}

	private async void OrderDetaiView(object sender, EventArgs e)
	{
		var button = (Button)sender;
		var order = (Order)button.CommandParameter;
		await Navigation.PushAsync(new OrderDetail(order));
	}

	private async void AddOrderButton_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new OrderAdd());
	}
}
