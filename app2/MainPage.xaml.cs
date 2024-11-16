namespace app2
{
    public partial class MainPage : ContentPage
    {
       // int count = 0;

        public MainPage()
        {
            InitializeComponent();
            StartVideoAndNavigate();

        }


        private async void StartVideoAndNavigate()
        {
            await Task.Delay(4500);

            await Shell.Current.GoToAsync("LoginPage");
        }

        private void SplashVideo_MediaOpened(object sender, EventArgs e)
        {
            Console.WriteLine("Video started playing.");
        }

        private void SplashVideo_MediaFailed(object sender, CommunityToolkit.Maui.Core.Primitives.MediaFailedEventArgs e)
        {
            Console.WriteLine("Video failed to play.");
        }
    }

}
