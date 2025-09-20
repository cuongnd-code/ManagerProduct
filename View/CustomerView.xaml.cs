using ManagerProduct.Model;
using Microsoft.EntityFrameworkCore;
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
        CustomerList();
        command = new Command(CustomerList);
    }

    public void CustomerList()
    {
        var customers = appDbContext.Customers.ToList();
       CustomerCollectionView.ItemsSource = customers;
    }

    public void RefreshView_Refreshing(object sender, EventArgs e)
    {
        CustomerList();
        ((RefreshView)sender).IsRefreshing = false;
    }

    private void Customer_Add(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new CustomerAdd(appDbContext));
    }

    public void Update_Customer(Object sender, EventArgs e)
    {
        var button = (Button)sender;
        var customer = (Customer)button.CommandParameter;
        Navigation.PushAsync(new CustomerUpdate(customer));
    }

    private void Delete_Customer(object sender, EventArgs e)
    {

        var button = (Button)sender;
        var id = (int)button.CommandParameter;
        var customers = appDbContext.Find<Customer>(id);
        if (customers == null)
        {
            DisplayAlert("thất bại", "Không tìm thấy sản phẩm!", "OK");
            return;
        }
        appDbContext.Remove<Customer> (customers);
        appDbContext.SaveChanges();
        DisplayAlert("Thành công", "Đã xóa sản phẩm!", "OK");
        CustomerList();
    }

    //public  void RefreshView_Refreshiing(object sender, EventArgs e)
    //{
    //    CustomerList();
    //    ((RefreshView)sender).IsRefreshing = false;
    //}
}