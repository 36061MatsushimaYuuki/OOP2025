using TextFileProcessor;
using WordCounter;

namespace PublicCounter {
    internal class Program {
        static void Main(string[] args) {
            string? filePath = "";
            while (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) {
                Console.Write("ファイルパスを入力: ");
                filePath = Console.ReadLine();
            }
            string? word = "";
            while (string.IsNullOrWhiteSpace(word)) {
                Console.Write("カウントする単語を入力: ");
                word = Console.ReadLine();
            }
            WordCounterProcessor._word = word;
            TextProcessor.Run<WordCounterProcessor>(filePath);
        }
    }
}
