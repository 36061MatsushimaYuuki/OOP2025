using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenkiApp {
    public class Hourly {
        public List<string> time { get; set; }
        public List<int> weather_code { get; set; }
        public List<double> temperature_2m { get; set; }
        public List<double> relative_humidity_2m { get; set; }
        public List<double> wind_speed_10m { get; set; }
    }
}
