using ManagerProduct.Model;
using System.Windows.Input;

namespace ManagerProduct.View;

public partial class CustomerView : ContentPage
{
    public readonly AppDbContext appDbContext;
    public ICommand command { get; }

    public CustomerView()
    {
        InitializeComponent();
        appDbContext = new AppDbContext();
        command = new Command(CustomerList);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CustomerList();
    }

    public void CustomerList()
    {
        var keyword = CustomerSearchBar?.Text?.Trim()?.ToLower();
        var query = appDbContext.Customers.AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(c => (c.Name != null && c.Name.ToLower().Contains(keyword)) ||
                                     (c.PhoneNumber != null && c.PhoneNumber.Contains(keyword)));
        }
        CustomerCollectionView.ItemsSource = query.OrderByDescending(c => c.Id).ToList();
    }

    private void CustomerSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        CustomerList();
    }

    private async void Customer_Add(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CustomerAdd(appDbContext));
    }

    public async void Update_Customer(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var customer = (Customer)button.CommandParameter;
        await Navigation.PushAsync(new CustomerUpdate(customer));
    }

    private async void Delete_Customer(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var id = (int)button.CommandParameter;
        var customer = appDbContext.Find<Customer>(id);
        if (customer == null)
        {
            await DisplayAlert("Thất bại", "Không tìm thấy khách hàng!", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Xác nhận", $"Bạn có chắc muốn xóa khách hàng '{customer.Name}'?", "Xóa", "Hủy");
        if (!confirm) return;

        appDbContext.Remove(customer);
        appDbContext.SaveChanges();
        await DisplayAlert("Thành công", "Đã xóa khách hàng thành công!", "OK");
        CustomerList();
    }
}
