

using ManagerProduct.Model;


namespace ManagerProduct.View;

public partial class CustomerUpdate : ContentPage
{
    AppDbContext appDbContext;

	public CustomerUpdate(Model.Customer customer)
    {
        InitializeComponent();
        BirthDate.MaximumDate = DateTime.Today;
        appDbContext = new AppDbContext();
        BindingContext = customer;
    }

    private async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        int id = int.Parse(Id.Text);
        var customer = appDbContext.Find<Customer>(id);

        if (customer == null)
        {
            await DisplayAlert("Thất bại", "Không tìm thấy khách hàng!", "OK");
            return;
        }

        // Gán giá trị từ form vào object
        customer.Name = Name.Text;
        customer.BirthDate = BirthDate.Date;
        customer.Address = Address.Text;
        customer.PhoneNumber = PhoneNumber.Text;
        customer.Email = Email.Text;

        // Update vào DB
        appDbContext.Update<Customer>(customer);
        appDbContext.SaveChanges();

        await DisplayAlert("Thành công", "Đã cập nhật khách hàng!", "OK");

    }
   
}