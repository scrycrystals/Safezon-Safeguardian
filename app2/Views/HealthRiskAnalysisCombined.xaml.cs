using System.Collections.ObjectModel;

namespace app2.Views;

public partial class HealthRiskAnalysisCombined : ContentPage
{

    public ObservableCollection<string> Images { get; set; }
    public HealthRiskAnalysisCombined()
	{
		InitializeComponent();
        Images = new ObservableCollection<string>
    {
        "logo.png",
        "hra2.png",
        "bplogo.png"
    };
        BindingContext = this;
    }
}