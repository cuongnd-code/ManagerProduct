using ManagerProduct.Model;
using ManagerProduct.ViewModel;

namespace ManagerProduct.View;

public partial class OrderAdd : ContentPage
{
	private readonly AppDbContext _context = new AppDbContext();
	private List<ProductSelectable> productSelectable = new List<ProductSelectable>();

	public OrderAdd()
	{
		InitializeComponent();
		LoadData();
	}

	private void LoadData()
	{
		var customers = _context.Customers.OrderBy(c => c.Name).ToList();
		CustomerPicker.ItemsSource = customers;

		var productList = _context.Products.Where(p => (p.Stock ?? 0) > 0).OrderBy(p => p.Name).ToList();
		productSelectable = productList.Select(p => new ProductSelectable
		{
			product = p,
			productID = p.Id,
			Quantity = 1,
			Price = p.Price,
			IsSelected = false
		}).ToList();

		ProductPicker.ItemsSource = productSelectable;
	}

	private async void AddOrderButton_Clicked(object sender, EventArgs e)
	{
		var selectedCustomer = (Customer)CustomerPicker.SelectedItem;
		if (selectedCustomer == null)
		{
			await DisplayAlert("Lỗi", "Vui lòng chọn khách hàng!", "OK");
			return;
		}

		var selectedProducts = productSelectable.Where(p => p.IsSelected).ToList();
		if (!selectedProducts.Any())
		{
			await DisplayAlert("Lỗi", "Vui lòng tích chọn ít nhất 1 sản phẩm!", "OK");
			return;
		}

		foreach (var sp in selectedProducts)
		{
			if (sp.Quantity <= 0)
			{
				await DisplayAlert("Lỗi", $"Số lượng cho sản phẩm '{sp.product.Name}' phải lớn hơn 0!", "OK");
				return;
			}

			if (sp.Quantity > (sp.product.Stock ?? 0))
			{
				await DisplayAlert("Lỗi", $"Sản phẩm '{sp.product.Name}' chỉ còn {sp.product.Stock ?? 0} trong kho!", "OK");
				return;
			}
		}

		double totalOrder = 0;
		var orderDetails = new List<OrderDetailModel>();

		foreach (var sp in selectedProducts)
		{
			var lineTotal = sp.Quantity * sp.Price;
			orderDetails.Add(new OrderDetailModel
			{
				ProductId = sp.productID,
				ProductName = sp.product.Name,
				Quantity = sp.Quantity,
				UnitPrice = sp.Price,
				TotalPrice = lineTotal
			});

			totalOrder += lineTotal;

			// Trừ tồn kho trong DB
			var dbProduct = _context.Products.Find(sp.productID);
			if (dbProduct != null)
			{
				dbProduct.Stock = Math.Max(0, (dbProduct.Stock ?? 0) - sp.Quantity);
				_context.Products.Update(dbProduct);
			}
		}

		var newOrder = new Order
		{
			CustomerId = selectedCustomer.Id,
			CustomerName = selectedCustomer.Name,
			OrderDate = DateTime.Now,
			OrderDetails = orderDetails,
			TotalAmount = totalOrder
		};

		_context.Orders.Add(newOrder);
		_context.SaveChanges();

		await DisplayAlert("Thành công", "Đã tạo hóa đơn thành công!", "OK");
		await Navigation.PopAsync();
	}
}
