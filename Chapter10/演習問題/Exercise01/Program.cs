using System.Text;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var filePath = "./demo.cs";
            Code1(filePath);
            Console.WriteLine();
            Code2(filePath);
            Console.WriteLine();
            Code3(filePath);
        }

        private static void Code1(string filePath) {
            var count = 0;
            using (var reader = new StreamReader(filePath, Encoding.UTF8)) {
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    if (line.Contains(" class ")) {
                        count++;
                    }
                }
                Console.WriteLine(count);
            }
        }

        private static void Code2(string filePath) {
            var count = File.ReadAllLines(filePath)
                .Count(s => s.Contains(" class "));
            Console.WriteLine(count);
        }

        private static void Code3(string filePath) {
            var count = 0;
            var lines = File.ReadLines(filePath)
                .Count(s => s.Contains(" class "));
            Console.WriteLine(count);
        }
    }
}
