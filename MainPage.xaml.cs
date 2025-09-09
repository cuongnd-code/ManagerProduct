using ManagerProduct.Model;
using ManagerProduct.View;

namespace ManagerProduct
{

	//[XamlCompilation (XamlCompilationOptions.Skip)]


	// KO THE TU BEN TRONG MA PHAI SUA TU XAML
	// internal partial class MainPage : ContentPage {}
	// = x:ClassModifier="NotPublic"

	public partial class MainPage : ContentPage
	{
		

		public readonly AppDbContext appDbContext;

		public MainPage(AppDbContext context)
		{
			InitializeComponent();
			appDbContext = context;
		}
		private async void AddButton_Clicked(object sender, EventArgs e)
		{
			var product = new Product
			{
				Name = Name.Text,
				Category = Category.Text,
				//Image = "dotnet_pot.png",
				Description = Description.Text,
				Price = double.TryParse(Price.Text, out var price) ? price : 0,
				Stock = int.TryParse(Stock.Text, out var stock) ? stock : 0
			};

			await appDbContext.Products.AddAsync(product);
			await appDbContext.SaveChangesAsync();
			await DisplayAlert("Thành công", "Đã thêm sản phẩm!", "OK");


		}

		private async void Button_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProductView());
		}

		//private async void Button_ImportImage(object sender, EventArgs e)
		//{
		//	var result = await FilePicker.PickAsync(new PickOptions
		//	{ 


		//		PickerTitle = "pick image",

		//		FileTypes = FilePickerFileType.Images,

		//	});
		//	if (result != null)
		//	{
		//			var stream = await result.OpenReadAsync();
		//			ProductImage.Source = ImageSource.FromStream(() => stream);

		//	}



		//}
	}

}
