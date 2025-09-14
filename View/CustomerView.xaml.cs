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
        CustomerList();
        command = new Command(CustomerList);
    }

    public void CustomerList()
    {
        var customers = appDbContext.Customers.ToList();
       CustomerCollectionView.ItemsSource = customers;
    }

    public void RefreshView_Refreshiing(object sender, EventArgs e)
    {
        CustomerList();
        ((RefreshView)sender).IsRefreshing = false;
    }
}