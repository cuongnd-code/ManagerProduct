using ManagerProduct.Model;

namespace ManagerProduct.View;

public partial class ProductAdd : ContentPage
{
	public readonly AppDbContext appDbContext = new AppDbContext();
	public ProductAdd()
	{
		InitializeComponent();

	}


	private void AddButton_Clicked(object sender, EventArgs e)
	{
		var product = new Product
		{
			Name = Name.Text,
			Category = Category.Text,
			//Image = "dotnet_pot.png",
			Description = Description.Text,
			Price = double.TryParse(Price.Text, out var price) ? price : 0,
			Stock = int.TryParse(Stock.Text, out var stock) ? stock : 0
		};

		appDbContext.Products.Add(product);
		appDbContext.SaveChanges();
		DisplayAlert("Thành công", "Đã thêm sản phẩm!", "OK");
	}
}