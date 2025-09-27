using System.Windows.Input;
using ManagerProduct.Model;

namespace ManagerProduct.View;


public partial class ProductView : ContentPage
{

	public readonly AppDbContext appDbContext;
	public ICommand command { get; }

	public ProductView()
	{
		InitializeComponent();
		appDbContext = new AppDbContext();
		ProductList();
		command = new Command(ProductList);
	}

	public void ProductList()
	{
		var products = appDbContext.Products.ToList();
		ProductCollectionView.ItemsSource = products;
	}

	public void RefreshView_Refreshing(object sender, EventArgs e)
	{
		ProductList();
		((RefreshView)sender).IsRefreshing = false;
	}

	public void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
	{
		//var keyword = e.NewTextValue;
		//var products = appDbContext.Products
		//	.Where(p => p.Name.Contains(keyword) || p.Category.Contains(keyword))
		//	.ToList();
		//ProductCollectionView.ItemsSource = products;
	}

	public void Update_page(object sender, EventArgs e)
	{
		var button = (Button)sender;
		var product = (Product)button.CommandParameter;
		Navigation.PushAsync(new ProductUpdate(product));
	}

	private void Delete_Clicked(object sender, EventArgs e)
	{

		var button = (Button)sender;
		var id = (int)button.CommandParameter;
		var products = appDbContext.Find<Product>(id);
		if (products == null)
		{
			DisplayAlert("thất bại", "Không tìm thấy sản phẩm!", "OK");
			return;
		}
		appDbContext.Remove<Product>(products);
		appDbContext.SaveChanges();
		DisplayAlert("Thành công", "Đã xóa sản phẩm!", "OK");
		ProductList();
	}

	private async void Add_product(object sender, EventArgs e)
	{

		await Navigation.PushAsync(new ProductAdd());
    }

	private void OnSearch(object sender, EventArgs e)
	{

    }

	private void OnFilterClicked(object sender, EventArgs e)
	{

	}
}