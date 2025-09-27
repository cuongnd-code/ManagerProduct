using ManagerProduct.Model;

namespace ManagerProduct.View;

public partial class OrderDetail : ContentPage
{
	public OrderDetail(Order order)
	{
		InitializeComponent();

		BindingContext = order;

	}
}