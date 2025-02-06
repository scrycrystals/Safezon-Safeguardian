using app2.ViewModels;
using Microsoft.Maui.Controls;

namespace app2.Views;

public partial class Profile : ContentPage
{
	public Profile()
	{   InitializeComponent();
        BindingContext = new ProfileViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Load the profile data when the page is about to appear
        await ((ProfileViewModel)BindingContext).LoadProfileData();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ChangePasswordPage("Binding Password"));
    }

    private void EditProfile(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EditProfilePage());


    }
}