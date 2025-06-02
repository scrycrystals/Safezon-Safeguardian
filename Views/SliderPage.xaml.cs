namespace app2.Views;
using app2.ViewModels;

public partial class SliderPage : ContentPage
{
	public SliderPage()
	{
		InitializeComponent();
        this.BindingContext = new SliderPageViewModel();

    }

    private void NextBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Dashboard1());

    }
}