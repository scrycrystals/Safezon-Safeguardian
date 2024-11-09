using app2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace app2.ViewModels
{
    public class SliderPageViewModel
    {
        public ObservableCollection<SliderPageModel> SliderScreens { get; set; } = new ObservableCollection<SliderPageModel>();

        public SliderPageViewModel()
        {
            SliderScreens.Add(new SliderPageModel
            {
                SliderImage = "slider1",
                SliderText = "\"Welcome to SafeZone! We're here to keep you safe and connected—anytime, anywhere, with just a tap.\""
            });

            SliderScreens.Add(new SliderPageModel
            {
                SliderImage = "slider2",
                SliderText = "\"Stay one step ahead with instant emergency alerts—just a tap, and help is on the way!\""
            });

            SliderScreens.Add(new SliderPageModel
            {
                SliderImage = "slider3",
                SliderText = "\"Keep your well-being in check—track your health stats effortlessly and stay on top of your wellness!\""
            });

            SliderScreens.Add(new SliderPageModel
            {
                SliderImage = "slider4",
                SliderText = "\"Know your way, stay safe—track your location in real-time and let your loved ones follow your journey securely!\""
            });
        }
    }
}
