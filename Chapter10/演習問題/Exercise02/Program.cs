using System.Formats.Asn1;
using System.Text;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            var filePath = "./10_2.txt";
            var newfilePath = "./10_2_2.txt";
            using (var reader = new StreamReader(filePath, Encoding.UTF8)) {
                var count = 1;
                using (var writer = new StreamWriter(newfilePath, append: false)) {
                    while (!reader.EndOfStream) {
                        var line = reader.ReadLine();
                        writer.WriteLine($"{count} {line}");
                        count++;
                    }
                }
            }
        }
    }
}
