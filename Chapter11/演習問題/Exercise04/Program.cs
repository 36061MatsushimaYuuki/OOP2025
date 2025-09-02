using System.Text.RegularExpressions;

namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            var lines = File.ReadAllLines("sample.txt");
            // 問題11.4
            var newlines = lines.Select(s => Regex.Replace(s, @"\b([vV]ersion)\s*=\s*""v4\.0""", @"$1=""v5.0"""));

            File.WriteAllLines("sampleChange.txt", newlines);

            // これ以降は確認用
            var text = File.ReadAllText("sampleChange.txt");
            Console.WriteLine(text);
        }
    }
}
