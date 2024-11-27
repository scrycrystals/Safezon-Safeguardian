namespace app2.Views;

public partial class MainHealthPage : ContentPage
{
	public MainHealthPage()
	{
		InitializeComponent();
	}

    private void HealthChartBtn_Clicked(object sender, EventArgs e)
    {
    }

    private void HealthRiskAnalysis_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new HealthRiskAnalysisCombined());
    }

    private void HealthAlerts_Clicked(object sender, EventArgs e)
    {

    }

    private void HealthScore_Clicked(object sender, EventArgs e)
    {

    }
}