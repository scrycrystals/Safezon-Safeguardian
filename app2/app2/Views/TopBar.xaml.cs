namespace app2.Views;

public partial class TopBar : ContentView
{
	public TopBar()
	{
		InitializeComponent();
	}

    private void OnMenuClicked(object sender, EventArgs e)
    {
        // Toggle the visibility of the dropdown menu
        //DropdownMenu.IsVisible = !DropdownMenu.IsVisible;
    }
}