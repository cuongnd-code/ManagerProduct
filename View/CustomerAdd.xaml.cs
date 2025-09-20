
using ManagerProduct.Model;
using ManagerProduct.View;


namespace ManagerProduct.View
{

    public partial class CustomerAdd : ContentPage
    {

        public readonly AppDbContext appDbContext;

       public CustomerAdd(AppDbContext context)
        {
            InitializeComponent();
            appDbContext = context;
            BirthDate.MaximumDate = DateTime.Today;
        }

        private void AddButton_Clicked1(object sender, EventArgs e)
        {
            var customer = new Customer
            {
                Name = Name.Text,
                BirthDate = BirthDate.Date,       // DatePicker.Date là DateTime
                Address = Address.Text,
                PhoneNumber = PhoneNumber.Text,
                Email = Email.Text
            };
            // Thêm vào DbContext
            appDbContext.Customers.Add(customer);

            // Lưu thay đổi vào database
            appDbContext.SaveChanges();

            // Hiển thị thông báo
            DisplayAlert("Thành công", "Đã thêm khách hàng!", "OK");
        }
    }
}