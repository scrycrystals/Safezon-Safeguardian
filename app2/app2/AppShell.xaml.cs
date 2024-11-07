using app2.Views;
namespace app2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LoginPage", typeof(MainHealthPage));
        }
    }
}
