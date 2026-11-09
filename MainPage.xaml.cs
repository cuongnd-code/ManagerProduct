using ManagerProduct.View;

namespace ManagerProduct
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		public MainPage(AppDbContext context) : this()
		{
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			LoadStatistics();
		}

		private void LoadStatistics()
		{
			try
			{
				using var context = new AppDbContext();
				var totalProducts = context.Products.Count();
				var lowStock = context.Products.Count(p => (p.Stock ?? 0) <= 0);
				var totalCustomers = context.Customers.Count();
				var totalOrders = context.Orders.Count();
				var totalRevenue = context.Orders.Sum(o => (double?)o.TotalAmount) ?? 0;

				TotalProductsLabel.Text = totalProducts.ToString();
				LowStockLabel.Text = $"Hết hàng: {lowStock}";
				TotalCustomersLabel.Text = totalCustomers.ToString();
				TotalOrdersLabel.Text = totalOrders.ToString();
				TotalRevenueLabel.Text = $"{totalRevenue:N0} ₫";
			}
			catch
			{
				// Ignore initialization timing
			}
		}

		private async void NavigateProduct_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProductView());
		}

		private async void NavigateCustomer_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CustomerView());
		}

		private async void NavigateOrder_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new OrderView());
		}
	}
}
