using System.IO;
using System.Text;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            //コンソールの表示形式をUTF-8に設定
            Console.OutputEncoding = Encoding.UTF8;
            Code10_1();
            Console.WriteLine();
            Code10_2();
            Console.WriteLine();
            Code10_3();
        }

        //1行ずつ
        private static void Code10_1() {
            Console.WriteLine("10.1 -----");
            var filePath = "./Greeting.txt";
            if (File.Exists(filePath)) {
                using var reader = new StreamReader(filePath, Encoding.UTF8);
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    Console.WriteLine(line);
                }
            }
        }

        //一気に
        private static void Code10_2() {
            Console.WriteLine("10.2 -----");
            var filePath = "./Greeting.txt";
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines) {
                Console.WriteLine(line);
            }
        }

        //Ienumerable<string>として扱う
        private static void Code10_3() {
            Console.WriteLine("10.3 -----");
            var filePath = "./Greeting.txt";
            var lines = File.ReadLines(filePath);
            foreach (var line in lines) {
                Console.WriteLine(line);
            }
        }
    }
}
