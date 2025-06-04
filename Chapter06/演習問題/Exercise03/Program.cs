
using System.Diagnostics.Tracing;
using System.Security;
using System.Text;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Jackdaws love my big sphinx of quartz";
            #region
            Console.WriteLine("6.3.1");
            Exercise1(text);

            Console.WriteLine("6.3.2");
            Exercise2(text);

            Console.WriteLine("6.3.3");
            Exercise3(text);

            Console.WriteLine("6.3.4");
            Exercise4(text);

            Console.WriteLine("6.3.5");
            Exercise5(text);

            Console.WriteLine("6.3.99");
            Exercise6(text);
            #endregion
        }

        //空白の数を数えて表示
        private static void Exercise1(string text) {
            var spaces = text.Count(c => c == ' ');
            //var spaces = text.Count(char.IsWhiteSpace);//別解
            Console.WriteLine("空白数:{0}", spaces);
        }

        //bigをsmallに置き換えて表示
        private static void Exercise2(string text) {
            var replaced = text.Replace("big", "small");
            Console.WriteLine(replaced);
        }

        //一旦単語別に分解し、StringBuilderで文字列を連結して表示
        private static void Exercise3(string text) {
            var array = text.Split(' ');
            var sb = new StringBuilder(array[0]);
            foreach (var word in array.Skip(1)) {
                sb.Append(" ");
                sb.Append(word);
            }
            //末尾はピリオド（.）で終わる
            Console.WriteLine(sb + ".");
        }

        //文字列を単語に分けて単語数を表示
        private static void Exercise4(string text) {
            var count = text.Split(' ').Length;
            Console.WriteLine("単語数:{0}", count);
        }

        //4文字以下の単語を表示
        private static void Exercise5(string text) {
            var words = text.Split(' ').Where(s => s.Length <= 4);

            foreach (var word in words)
                Console.WriteLine(word);
        }

        //アルファベットの数をカウントして表示する
        private static void Exercise6(string text) {
            for (var alphabet = 'a'; alphabet <= 'z'; alphabet++) {
                Console.WriteLine($"{alphabet}: {text.Count(c => c == alphabet)}");
            }

            #region
            //var words = Enumerable.Range('a', 26).Select(c => (char)c);
            //foreach(var word in words) {
            //    Console.WriteLine($"{word}: {text.Count(c => c == word)}");
            //}
            #endregion
        }
    }
}
