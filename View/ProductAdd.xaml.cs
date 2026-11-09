using ManagerProduct.Model;

namespace ManagerProduct.View;

public partial class ProductAdd : ContentPage
{
	public readonly AppDbContext appDbContext = new AppDbContext();

	public ProductAdd()
	{
		InitializeComponent();
	}

	private async void AddButton_Clicked(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(Name.Text))
		{
			await DisplayAlert("Lỗi", "Vui lòng nhập tên sản phẩm!", "OK");
			return;
		}

		var product = new Product
		{
			Name = Name.Text.Trim(),
			Category = Category.Text?.Trim() ?? string.Empty,
			Description = Description.Text?.Trim() ?? string.Empty,
			Price = double.TryParse(Price.Text, out var price) ? price : 0,
			Stock = int.TryParse(Stock.Text, out var stock) ? stock : 0
		};

		appDbContext.Products.Add(product);
		appDbContext.SaveChanges();
		await DisplayAlert("Thành công", "Đã thêm sản phẩm thành công!", "OK");
		await Navigation.PopAsync();
	}
}
