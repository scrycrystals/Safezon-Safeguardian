using app2.ViewModels;
using app2.Services;
namespace app2.Views;


public partial class AddRiskyLocation : ContentPage
{
    private static AddRiskyLocationViewModel viewModel = new AddRiskyLocationViewModel(new LocationService());
    public AddRiskyLocation()
	{
		InitializeComponent();

        BindingContext = new AddRiskyLocationViewModel(new LocationService());

        // MessagingCenter listens for ViewModel event when location is added successfully
        MessagingCenter.Subscribe<AddRiskyLocationViewModel>(this, "LocationAdded", async (sender) =>
        {
            await DisplayAlert("Success", "Risky location added successfully!", "OK");
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Always unsubscribe to avoid memory leaks
        MessagingCenter.Unsubscribe<AddRiskyLocationViewModel>(this, "LocationAdded");
    }

}