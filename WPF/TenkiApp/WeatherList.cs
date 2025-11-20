using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenkiApp {
    class WeatherList {
        public Hourly hourly { get; set; }

        public static string WeatherDescription(int code) {
            switch (code) {
                case 0:
                    return "快晴";
                case 1:
                case 2:
                case 3:
                    return "晴れ、薄曇り、曇り";
                case 45:
                case 48:
                    return "霧、着氷霧";
                case 51:
                case 53:
                case 55:
                    return "霧雨: 弱い、中程度、強い";
                case 56:
                case 57:
                    return "着氷性の霧雨: 弱い、強い";
                case 61:
                case 63:
                case 65:
                    return "雨: 小雨、中雨、大雨";
                case 66:
                case 67:
                    return "着氷性の雨: 弱い、強い";
                case 71:
                case 73:
                case 75:
                    return "雪: 小雪、中雪、大雪";
                case 77:
                    return "霙（みぞれ）";
                case 80:
                case 81:
                case 82:
                    return "にわか雨: 弱い、中程度、激しい";
                case 85:
                case 86:
                    return "にわか雪: 弱い、強い";
                case 95:
                    return "雷雨: 弱いまたは中程度";
                case 96:
                case 99:
                    return "雷雨: 小さい、大きいひょうを伴う";
                default:
                    return "不明";
            }
        }
    }
}
