
namespace app2;

public partial class Register : ContentPage
{
    public Register() => InitializeComponent();

    private void Registerbtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Dashboard1());
    }
}


