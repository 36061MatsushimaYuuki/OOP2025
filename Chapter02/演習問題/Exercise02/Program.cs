﻿namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

            PrintInchToMeterList(1, 10);
        }

        // インチからメートルへの対応表を出力
        static void PrintInchToMeterList(int start, int end) {
            for (int inch = start; inch <= end; inch++) {
                double meter = InchConverter.ToMeter(inch);
                Console.WriteLine($"{inch} in = {meter:0.0000} m");
            }
        }
    }
}
