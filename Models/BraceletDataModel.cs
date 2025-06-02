using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app2.Models
{
    public class GPSData
    {
        public double f_latitude { get; set; }
        public double f_longitude { get; set; }
    }

    public class BraceletDataModel
    {
        public int BPM { get; set; }
        public int Button { get; set; }
        public GPSData GPS { get; set; } = new GPSData();
        public int Temp { get; set; }
        public int X_Angl { get; set; }
        public int Y_Angl { get; set; }
    }
}
