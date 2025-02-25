namespace app2.Views;
using Map = Microsoft.Maui.Controls.Maps.Map;


public partial class Gmap : ContentPage
{
	public Gmap()
	{
		InitializeComponent();
        Map map = new Map();
        Content = map;
    }
}