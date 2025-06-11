
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Cozy lummox gives smart squid who asks for job pen";

            Exercise1(text);
            Console.WriteLine();

            Exercise2(text);
        }

        private static void Exercise1(string text) {
            var countDict = new Dictionary<char, int>();
            for(var ch = 'A'; ch <= 'Z'; ch++) {
                countDict.Add(ch, text.ToUpper().Count(c => c == ch));
            }
            foreach (var dict in countDict.OrderBy(c => c)) {
                if (dict.Value > 0) {
                    Console.WriteLine($"{dict.Key}:{dict.Value}");
                }
            }
        }

        private static void Exercise2(string text) {
            //①ディクショナリインスタンスの生成
            var countDict = new SortedDictionary<char, int>();
            //②１文字取り出す
            //③大文字に変換
            foreach(var ch in text.ToUpper()) {
                //④アルファベットならディクショナリに登録
                if ('A' <= ch && ch <= 'Z') {
                    if (countDict.ContainsKey(ch)) {
                        //登録済み : valueをインクリメント
                        countDict[ch]++;
                    } else {
                        //未登録 : valueに１を設定
                        countDict[ch] = 1;
                    }
                }
            }
            foreach(var dict in countDict) {
                Console.WriteLine($"{dict.Key}:{dict.Value}");
            }
        }
    }
}
