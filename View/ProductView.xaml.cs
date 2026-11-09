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
		command = new Command(ProductList);
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		ProductList();
	}

	public void ProductList()
	{
		var keyword = ProductSearchBar?.Text?.Trim()?.ToLower();
		var query = appDbContext.Products.AsQueryable();
		if (!string.IsNullOrEmpty(keyword))
		{
			query = query.Where(p => (p.Name != null && p.Name.ToLower().Contains(keyword)) ||
			                         (p.Category != null && p.Category.ToLower().Contains(keyword)));
		}
		ProductCollectionView.ItemsSource = query.OrderByDescending(p => p.Id).ToList();
	}

	public void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
	{
		ProductList();
	}

	public async void Update_page(object sender, EventArgs e)
	{
		var button = (Button)sender;
		var product = (Product)button.CommandParameter;
		await Navigation.PushAsync(new ProductUpdate(product));
	}

	private async void Delete_Clicked(object sender, EventArgs e)
	{
		var button = (Button)sender;
		var id = (int)button.CommandParameter;
		var product = appDbContext.Find<Product>(id);
		if (product == null)
		{
			await DisplayAlert("Thất bại", "Không tìm thấy sản phẩm!", "OK");
			return;
		}

		bool confirm = await DisplayAlert("Xác nhận", $"Bạn có chắc muốn xóa sản phẩm '{product.Name}'?", "Xóa", "Hủy");
		if (!confirm) return;

		appDbContext.Remove(product);
		appDbContext.SaveChanges();
		await DisplayAlert("Thành công", "Đã xóa sản phẩm thành công!", "OK");
		ProductList();
	}

	private async void Add_product(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ProductAdd());
	}
}
