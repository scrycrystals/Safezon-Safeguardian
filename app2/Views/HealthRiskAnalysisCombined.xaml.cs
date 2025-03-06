using System.Collections.ObjectModel;
using app2.ViewModels;

namespace app2.Views;

public partial class HealthRiskAnalysisCombined : ContentPage
{
    private BraceletDataViewModel viewModel;
    public ObservableCollection<string> Images { get; set; }
    public HealthRiskAnalysisCombined()
	{
		InitializeComponent();
        BindingContext = new BraceletDataViewModel();
        Images = new ObservableCollection<string>
    {
        "logo.png",
        "hra2.png",
        "bplogo.png"
    };
        //BindingContext = this;

    }


}