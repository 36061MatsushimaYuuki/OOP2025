using System.Text.RegularExpressions;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            string filePath = "sample.txt";
            Pickup3DigitNumber(filePath);
            Console.WriteLine("------------");
            PickupAlphabet(filePath);
        }

        private static void Pickup3DigitNumber(string filePath) {
            foreach(var line in File.ReadLines(filePath)) {
                var matches = Regex.Matches(line, @"\b\d{3,}\b");
                foreach(Match m in matches) {
                    //結果を出力
                    Console.WriteLine(m.Value);
                }
            }
        }

        private static void PickupAlphabet(string filePath) {
            foreach (var line in File.ReadLines(filePath)) {
                var matches = Regex.Matches(line, @"\b[a-zA-Z]+\b");
                foreach (Match m in matches) {
                    //結果を出力
                    Console.WriteLine(m.Value);
                }
            }
        }
    }
}
