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
                SliderText = "Welcome to SafeZone! We're here to keep you safe and connected—anytime, anywhere, with just a tap."
            });

            SliderScreens.Add(new SliderPageModel
            {
                SliderImage = "slider2",
                SliderText = "Your safety is our priority. Stay connected with SafeZone in any emergency."
            });

            SliderScreens.Add(new SliderPageModel
            {
                SliderImage = "slider3",
                SliderText = "Find help nearby with ease. SafeZone is your companion in times of need."
            });

            SliderScreens.Add(new SliderPageModel
            {
                SliderImage = "slider4",
                SliderText = "Join SafeZone and experience peace of mind, knowing help is just a tap away."
            });
        }
    }
}
