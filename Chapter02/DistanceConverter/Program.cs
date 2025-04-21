namespace DistanceConverter {
    internal class Program {
        static void Main(string[] args) {

            // デフォルト用の値を設定
            int def_start = 1;
            int def_end = 10;
            // 引数が渡されていない場合、配列から参照しない
            // 例：-tom 1 10 => true / -tom => false
            int start = args.Length > 1 ? int.Parse(args[1]) : def_start;
            int end = args.Length > 2 ? int.Parse(args[2]) : def_end;
            // startがendより大きい場合入れ替え
            int[] result_nums = ChangeMinMax(start, end);
            start = result_nums[0];
            end = result_nums[1];

            if (args.Length > 0 && args[0].Equals("-tom")) {
                PrintFeetToMeterList(start, end);
            } else {
                PrintMeterToFeetList(start, end);
            }
        }

        static int[] ChangeMinMax(int min, int max) {
            int[] result = {min, max};
            if (min > max) {
                int work = min;
                min = max;
                max = work;
                result[0] = min;
                result[1] = max;
            }
            return result;
        }

        // フィートからメートルへの対応表を出力
        static void PrintFeetToMeterList(int start, int end) {
            for (int feet = start; feet <= end; feet++) {
                double meter = FeetToMeter(feet);
                Console.WriteLine($"{feet} ft = {meter:0.0000} m");
            }
        }

        // メートルからフィートへの対応表を出力
        static void PrintMeterToFeetList(int start, int end) {
            for (int meter = start; meter <= end; meter++) {
                double feet = MeterToFeet(meter);
                Console.WriteLine($"{meter} m = {feet:0.0000} ft");
            }
        }

        // フィートからメートルへの変換
        static double FeetToMeter(int feet) {
            return feet * 0.3048;
        }

        // メートルからフィートへの変換
        static double MeterToFeet(int meter) {
            return meter / 0.3048;
        }
    }
}
