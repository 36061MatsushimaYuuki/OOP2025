using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenkiApp {
    public class WeatherList {
        public Hourly hourly { get; set; }

        public static string WeatherDescription(int code) {
            switch (code) {
                case 0:
                    return "☀";
                case 1:
                    return "🌤";
                case 2:
                    return "🌥";
                case 3:
                    return "☁";
                case 45:
                    return "🌫";
                case 48:
                    return "🌫❄️";
                case 51:
                case 53:
                case 55:
                    return "🌂";
                case 56:
                case 57:
                    return "🌂";
                case 61:
                case 63:
                    return "☂";
                case 65:
                    return "☔";
                case 66:
                case 67:
                    return "🌧❄️";
                case 71:
                case 73:
                    return "🌨️";
                case 75:
                    return "☃️";
                case 77:
                    return "❄️☂";
                case 80:
                case 81:
                case 82:
                    return "🌦";
                case 85:
                case 86:
                    return "🌨";
                case 95:
                    return "🌩";
                case 96:
                case 99:
                    return "⛈";
                default:
                    return "不明";
            }
        }
    }
}
