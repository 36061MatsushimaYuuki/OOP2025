using System.Text;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine("***　変換アプリ　***");
            Console.WriteLine("1：インチからメートル");
            Console.WriteLine("2：メートルからインチ");
            Console.Write("＞");
            // モード選択
            int mode = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if(mode == 1) { // インチからメートル
                int[] getParams = SetStartEnd();
                PrintInchToMeterList(getParams[0], getParams[1]);
            } else if (mode == 2) { // メートルからインチ
                int[] getParams = SetStartEnd();
                PrintMeterToInchList(getParams[0], getParams[1]);
            } else {
                Console.WriteLine("エラー：値が不正です");
            }
        }

        static int[] SetStartEnd() {
            int[] result = {1, 1};
            for (int i = 0; i < 2; i++) {
                if(i % 2 == 0) {
                    Console.Write("はじめ：");
                } else {
                    Console.Write("おわり：");
                }
                int num = int.Parse(Console.ReadLine());
                result[i] = num;
            }
            return result;
        }

        // メートルからインチへの対応表を出力
        static void PrintMeterToInchList(int start, int end) {
            for (int meter = start; meter <= end; meter++) {
                double inch = InchConverter.ToInch(meter);
                Console.WriteLine($"{meter} m = {inch:0.0000} inch");
            }
        }

        // インチからメートルへの対応表を出力
        static void PrintInchToMeterList(int start, int end) {
            for (int inch = start; inch <= end; inch++) {
                double meter = InchConverter.ToMeter(inch);
                Console.WriteLine($"{inch} inch = {meter:0.0000} m");
            }
        }
    }
}
