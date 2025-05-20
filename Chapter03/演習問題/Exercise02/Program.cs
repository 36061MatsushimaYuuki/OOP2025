
using System.Text.RegularExpressions;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            var cities = new List<string> {
                "Tokyo", "New Delhi", "Bangkok", "London",
                "Paris", "Berlin", "Canberra", "Hong Kong",
            };

            Console.WriteLine("***** 3.2.1 *****");
            Exercise2_1(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.2 *****");
            Exercise2_2(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.3 *****");
            Exercise2_3(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.4 *****");
            Exercise2_4(cities);
            Console.WriteLine();

        }

        private static void Exercise2_1(List<string> names) {
            Console.WriteLine("都市名を入力。");
            while (true) {
                // コンソールから入力
                var name = Console.ReadLine();

                // NULLか空の場合ループから出る
                if (string.IsNullOrEmpty(name)) {
                    break;
                }

                // リストから入力値を探してIndexを返す
                var index = names.FindIndex(s => s.Equals(name));
                Console.WriteLine(index);
            }
        }

        private static void Exercise2_2(List<string> names) {
            var cnt = names.Count(s => s.Contains('o'));
            Console.WriteLine(cnt);
        }

        private static void Exercise2_3(List<string> names) {
            var array = names.Where(s => s.Contains('o'))
                             .ToArray();

            foreach(var name in array) {
                Console.WriteLine(name);
            }
        }

        private static void Exercise2_4(List<string> names) {
            var selected = names.Where(s => s.StartsWith('B'))
                                .Select(s => s.Length);

            foreach (var num in selected) {
                Console.WriteLine(num);
            }
        }
    }
}
