using ManagerProduct.Model;

namespace ManagerProduct.View;

public partial class CustomerUpdate : ContentPage
{
    private readonly AppDbContext appDbContext;

    public CustomerUpdate(Customer customer)
    {
        InitializeComponent();
        BirthDate.MaximumDate = DateTime.Today;
        appDbContext = new AppDbContext();
        BindingContext = customer;
    }

    private async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Name.Text))
        {
            await DisplayAlert("Lỗi", "Họ tên khách hàng không được để trống!", "OK");
            return;
        }

        if (!int.TryParse(CustomerIdEntry.Text, out var id))
        {
            await DisplayAlert("Lỗi", "Mã khách hàng không hợp lệ!", "OK");
            return;
        }

        var customer = appDbContext.Find<Customer>(id);
        if (customer == null)
        {
            await DisplayAlert("Thất bại", "Không tìm thấy khách hàng!", "OK");
            return;
        }

        customer.Name = Name.Text.Trim();
        customer.BirthDate = BirthDate.Date;
        customer.Address = Address.Text?.Trim() ?? string.Empty;
        customer.PhoneNumber = PhoneNumber.Text?.Trim() ?? string.Empty;
        customer.Email = Email.Text?.Trim() ?? string.Empty;

        appDbContext.Update(customer);
        appDbContext.SaveChanges();

        await DisplayAlert("Thành công", "Đã cập nhật khách hàng thành công!", "OK");
        await Navigation.PopAsync();
    }
}
