using System.Xml.Linq;
using System;
using ManagerProduct.Model;

namespace ManagerProduct.View;

public partial class ProductUpdate : ContentPage
{
	
	 AppDbContext appDbContext;
	public ProductUpdate(Model.Product product)
	{
		InitializeComponent();
		appDbContext = new AppDbContext();
		BindingContext = product; // binding trực tiếp vào product


	}

	private void AddButton_Clicked(object sender, EventArgs e)
	{

	}

	private async void UpdateButton_Clicked(object sender, EventArgs e)
	{

		int id = int.Parse(Id.Text);
		var products = appDbContext.Find<Product>(id);
		if (products == null)
		{
		await DisplayAlert("thất bại", "Không tìm thấy sản phẩm!", "OK");
			return;
		}
			products.Name = Name.Text;
			products.Category = Category.Text;
			products.Price = int.Parse(Price.Text);
			products.Stock = int.Parse(Stock.Text);
			products.Description = Description.Text;

		
		appDbContext.Update<Product>(products);
		appDbContext.SaveChanges();
		await DisplayAlert("Thành công", "Đã cập nhập sản phẩm!", "OK");

	}
	

}