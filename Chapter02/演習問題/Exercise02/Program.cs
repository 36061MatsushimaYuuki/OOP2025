using System.Text;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

#if false
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
#else
            Console.WriteLine("***　変換アプリ　***");
            Console.WriteLine("1：ヤードからメートル");
            Console.WriteLine("2：メートルからヤード");
            Console.Write("＞");
            // モード選択
            int mode = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (mode == 1) { // ヤードからメートル
                Console.Write("変換前(ヤード): ");
                int yard = int.Parse(Console.ReadLine());
                PrintYardToMeter(yard);
            } else if (mode == 2) { // メートルからヤード
                Console.Write("変換前(メートル): ");
                int meter = int.Parse(Console.ReadLine());
                PrintMeterToYard(meter);
            } else {
                Console.WriteLine("エラー：値が不正です");
            }
#endif
        }

        // ヤードからメートルへ変換
        static void PrintYardToMeter(int yard) {
            double meter = YardConverter.ToMeter(yard);
            Console.WriteLine($"変換後(メートル): {meter:0.000}");
        }

        // メートルからヤードへ変換
        static void PrintMeterToYard(int meter) {
            double yard = YardConverter.ToYard(meter);
            Console.WriteLine($"変換後(ヤード): {yard:0.000}");
        }

        static int[] SetStartEnd() {
            int[] result = { 1, 1 };
            for (int i = 0; i < 2; i++) {
                if (i % 2 == 0) {
                    Console.Write("はじめ：");
                } else {
                    Console.Write("おわり：");
                }
                int num = int.Parse(Console.ReadLine());
                result[i] = num;
            }
            if (result[0] > result[1]) { // 大, 小の関係のとき入替
                int work = result[0];
                result[0] = result[1];
                result[1] = work;
            }
            return result;
        }

        // インチからメートルへの対応表を出力
        static void PrintInchToMeterList(int start, int end) {
            for (int inch = start; inch <= end; inch++) {
                double meter = InchConverter.ToMeter(inch);
                Console.WriteLine($"{inch} inch = {meter:0.0000} m");
            }
        }

        // メートルからインチへの対応表を出力
        static void PrintMeterToInchList(int start, int end) {
            for (int meter = start; meter <= end; meter++) {
                double inch = InchConverter.ToInch(meter);
                Console.WriteLine($"{meter} m = {inch:0.0000} inch");
            }
        }
    }
}
