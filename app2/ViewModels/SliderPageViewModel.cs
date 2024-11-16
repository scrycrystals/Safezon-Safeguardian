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
                SliderImage = "slider1"
            });

            SliderScreens.Add(new SliderPageModel
            {
                SliderImage = "slider2"
            });

            SliderScreens.Add(new SliderPageModel
            {
                SliderImage = "slider3"
            });

            SliderScreens.Add(new SliderPageModel
            {
                SliderImage = "slider4"
            });
        }
    }
}
