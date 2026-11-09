using ManagerProduct.Model;

namespace ManagerProduct.View;

public partial class CustomerAdd : ContentPage
{
    public readonly AppDbContext appDbContext;

    public CustomerAdd(AppDbContext context)
    {
        InitializeComponent();
        appDbContext = context;
        BirthDate.MaximumDate = DateTime.Today;
        BirthDate.Date = new DateTime(2000, 1, 1);
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Name.Text))
        {
            await DisplayAlert("Lỗi", "Vui lòng nhập họ tên khách hàng!", "OK");
            return;
        }

        var customer = new Customer
        {
            Name = Name.Text.Trim(),
            BirthDate = BirthDate.Date,
            Address = Address.Text?.Trim() ?? string.Empty,
            PhoneNumber = PhoneNumber.Text?.Trim() ?? string.Empty,
            Email = Email.Text?.Trim() ?? string.Empty
        };

        appDbContext.Customers.Add(customer);
        appDbContext.SaveChanges();

        await DisplayAlert("Thành công", "Đã thêm khách hàng thành công!", "OK");
        await Navigation.PopAsync();
    }
}
