namespace app2;

public partial class Dashboard1 : ContentPage
{
	public Dashboard1()
	{
		InitializeComponent();
	}

    private async void OnMenuClicked(object sender, EventArgs e)
    {
        var action = await DisplayActionSheet("Menu", "Cancel", null, "Option 1", "Option 2", "Option 3");

        // Handle menu options
        switch (action)
        {
            case "Option 1":
                // Do something for Option 1
                break;
            case "Option 2":
                // Do something for Option 2
                break;
            case "Option 3":
                // Do something for Option 3
                break;
            default:
                break;
        }
    }
    public void OnCallPoliceClicked(object sender, EventArgs e)
    {
        // Your code to call police
    }

    public void OnHospitalTapped(object sender, EventArgs e)
    {
        // Your code to handle the hospital tap
    }

    public void OnCallHospitalClicked(object sender, EventArgs e)
    {
        // Your code to call hospital
    }

    public void OnCallButtonClicked(object sender, EventArgs e)
    {
        // Your code to handle call button click
    }

    public void OnClosePopupClicked(object sender, EventArgs e)
    {
        // Your code to handle closing the popup
    }

    private void OnHospitalTapped(object sender, TappedEventArgs e)
    {

    }

    private void OnPoliceStationTapped(object sender, TappedEventArgs e)
    {

    }
}