using ManagerProduct.Model;

namespace ManagerProduct.View;

public partial class ProductUpdate : ContentPage
{
	private readonly AppDbContext appDbContext;

	public ProductUpdate(Model.Product product)
	{
		InitializeComponent();
		appDbContext = new AppDbContext();
		BindingContext = product;
	}

	private async void UpdateButton_Clicked(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(Name.Text))
		{
			await DisplayAlert("Lỗi", "Tên sản phẩm không được để trống!", "OK");
			return;
		}

		if (!int.TryParse(ProductIdEntry.Text, out var id))
		{
			await DisplayAlert("Lỗi", "Mã sản phẩm không hợp lệ!", "OK");
			return;
		}

		var product = appDbContext.Find<Product>(id);
		if (product == null)
		{
			await DisplayAlert("Thất bại", "Không tìm thấy sản phẩm trong cơ sở dữ liệu!", "OK");
			return;
		}

		product.Name = Name.Text.Trim();
		product.Category = Category.Text?.Trim() ?? string.Empty;
		product.Price = double.TryParse(Price.Text, out var price) ? price : 0;
		product.Stock = int.TryParse(Stock.Text, out var stock) ? stock : 0;
		product.Description = Description.Text?.Trim() ?? string.Empty;

		appDbContext.Update(product);
		appDbContext.SaveChanges();
		await DisplayAlert("Thành công", "Đã cập nhật sản phẩm thành công!", "OK");
		await Navigation.PopAsync();
	}
}
