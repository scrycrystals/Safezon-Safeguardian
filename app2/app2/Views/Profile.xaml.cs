namespace app2;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ChangePasswordPage("YourOldPassword"));
    }

    private void EditProfile(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EditProfilePage());


    }
}