using app2.ViewModels;
namespace app2.Views;

public partial class BraceletDataPage : ContentPage
{
    private BraceletDataViewModel viewModel;

    public BraceletDataPage()
	{
		InitializeComponent();
        BindingContext = new BraceletDataViewModel();
    }
}