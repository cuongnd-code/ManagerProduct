using ManagerProduct.Model;
using ManagerProduct.ViewModel;

namespace ManagerProduct.View;

public partial class OrderAdd : ContentPage
{

	private AppDbContext _context = new AppDbContext();
	private List<Customer> customer;
	private List<Product> productList;
	private List<ProductSelectable> productSelectable = new List<ProductSelectable>();
	private List<OrderDetailModel> orderDetails =new List<OrderDetailModel>();

	public OrderAdd()
	{
		InitializeComponent();
		customer = _context.Customers.ToList();

		productList = _context.Products.ToList();
		if (productList != null)
		{
            foreach (var p in productList)
            {
                productSelectable.Add(new ProductSelectable
                {
                    product = p,
                    productID = p.Id,
                    Quantity = 0,
                    Price = p.Price,
                    IsSelected = false,
                });


            }
        }
		
		
        ProductPicker.ItemsSource = productSelectable;
        CustomerPicker.ItemsSource = customer;

    }

	private async void SaveOrderButton_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	private List<OrderDetailModel> GetOrderDetails()
	{
		return orderDetails;
	}

	private void AddOrderButton_Clicked(object sender, EventArgs e)
	{
		double totalOrder = 0;
		var selectedCustomer = (Customer)CustomerPicker.SelectedItem; // Customer là class chứa Id, Name
		if (selectedCustomer == null)
		{
			DisplayAlert("Error", "Please select a customer and at least one product with quantity.", "OK");
			return;
		}
		var selectedProducts = productSelectable.Where(p => p.IsSelected && p.Quantity > 0).ToList();

		if (selectedCustomer == null || !selectedProducts.Any())
		{
			DisplayAlert("Error", "Please select a Product and at least one product with quantity.", "OK");
			return;
		}

		foreach (var sp in selectedProducts)
		{

			if (sp.Quantity <= 0) 
			{
				DisplayAlert("Error", "Please enter a valid quantity for selected products.", "OK");
				return;
			}


			orderDetails.Add(new OrderDetailModel
			{
				ProductId = sp.Id,
				ProductName = sp.product.Name,
				Quantity = sp.Quantity,
				UnitPrice = sp.Price,
				TotalPrice = sp.Quantity * sp.Price
			});

			totalOrder += sp.Quantity * sp.Price;
		}


			var newOrder = new Order
		{
			CustomerId = selectedCustomer.Id,
			OrderDate = DateTime.Now,
			OrderDetails = orderDetails,
			TotalAmount = totalOrder
			};

		

		_context.Orders.Add(newOrder);
		_context.SaveChanges();

		DisplayAlert("Success", "Order added successfully!", "OK");
	}

	
}