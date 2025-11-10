using System.Text;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("ファイル1のパスを入力:");
            var filePath1 = Console.ReadLine();
            Console.WriteLine("ファイル2のパスを入力:");
            var filePath2 = Console.ReadLine();
            if(!File.Exists(filePath1) || !File.Exists(filePath2)) {
                return;
            }

            using (var writer = new StreamWriter(filePath1, append: true)) {
                using (var reader = new StreamReader(filePath2, Encoding.UTF8)) {
                    var texts = reader.ReadToEnd();
                    writer.WriteLine();
                    writer.WriteLine(texts);
                }
            }
        }
    }
}
