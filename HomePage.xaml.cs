namespace ManagerProduct;

public partial class HomePage : ContentPage
{

	string? translatedNumber;

	public HomePage()
	{
		InitializeComponent();
	}

	private void Ontranlaster(object sender, EventArgs e)
	{
		string enterNumber = PhoneNumber.Text; //1-555-NETMUI
		translatedNumber = Phoneword.ToNumber(enterNumber);

		if (translatedNumber != null)
		{
			CallButton.IsEnabled = true;
			CallButton.Text = "Call " + translatedNumber;

		}
		else
		{

			CallButton.IsEnabled = false;
			CallButton.Text = "Call";

		}
	}
	private async void OnCall(object sender, EventArgs e)
	{
		if (await this.DisplayAlert(
			"Dial a Number",
			"Would you like to call " + translatedNumber + "?",
			"Yes",
			"No"

			))
		{
			try
			{
				if (PhoneDialer.Default.IsSupported && !string.IsNullOrWhiteSpace(translatedNumber))
				{
					PhoneDialer.Default.Open(translatedNumber);
				}
			}
			catch (ArgumentNullException)
			{
				await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");

			}
			catch (Exception)
			{
				await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
			}


		}

	}
	void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
	{

		// dung arg chi lay dc gia tri da thay doi
		//valueLabel.Text = args.NewValue.ToString("F3");


		//dung sender co the lay dc min max slider.max,...
		valueLabel.Text = ((Slider)sender).Value.ToString("F3");

	}
	private async void BackHome_Clicked(object sender, EventArgs e)
	{

		// sang trang
		await Navigation.PushAsync(new HomePage());




	}
}