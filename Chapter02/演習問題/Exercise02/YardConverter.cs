using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    public static class YardConverter {

        // 定数
        private const double ratio = 1.094;

        // ヤードからメートルを求める
        public static double ToMeter(double yard) {
            return yard / ratio;
        }

        // メートルからヤードを求める
        public static double ToYard(double meter) {
            return meter * ratio;
        }
    }
}
