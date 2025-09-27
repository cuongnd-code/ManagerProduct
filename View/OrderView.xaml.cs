using ManagerProduct.Model;
using Microsoft.EntityFrameworkCore;

namespace ManagerProduct.View;

public partial class OrderView : ContentPage
{
	public OrderView()
	{
		InitializeComponent();
		OrderCollectionView.ItemsSource = new AppDbContext().Orders
			.Include(c => c.OrderDetails)
			.ToList();
	}

	private async void OrderDetaiView(object sender, EventArgs e)
	{

		var button = (Button)sender;
		var order = (Order)button.CommandParameter; // lấy ra object Order được bind

		await Navigation.PushAsync(new OrderDetail(order));
    }

	private async void AddOrderButton_Clicked(object sender, EventArgs e)
	{

		await Navigation.PushAsync(new OrderAdd());
	}
}