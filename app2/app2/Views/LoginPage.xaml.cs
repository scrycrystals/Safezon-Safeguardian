namespace app2.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private void LoginBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Register());

    }
}