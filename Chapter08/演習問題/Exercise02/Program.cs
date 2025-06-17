namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            // コンストラクタの呼び出し
            var abbrs = new Abbreviations();

            // Addメソッドの呼び出し例
            abbrs.Add("IOC", "国際オリンピック委員会");
            abbrs.Add("NPT", "核兵器不拡散条約");

            // Getメソッドの利用例
            var names = new[] { "WHO", "FIFA", "NPT", };
            foreach (var name in names) {
                var fullname = abbrs.Get(name);
                if (fullname is null) {
                    Console.WriteLine($"{name}は見つかりません");
                } else {
                    Console.WriteLine($"{name}={fullname}");
                }
            }
            Console.WriteLine();

            // ToAbbreviationメソッドの利用例
            var japanese = "東南アジア諸国連合";
            var abbreviation = abbrs.ToAbbreviation(japanese);
            if (abbreviation is null) {
                Console.WriteLine($"{japanese} は見つかりません");
            } else {
                Console.WriteLine($"「{japanese}」の略語は {abbreviation} です");
            }
            Console.WriteLine();

            // FindAllメソッドの利用例
            foreach (var (key, value) in abbrs.FindAll("国際")) {
                Console.WriteLine($"{key}={value}");
            }
            Console.WriteLine();

            // 8.2.3
            // Countプロパティの利用例
            Console.WriteLine($"登録件数：{abbrs.Count}");

            // Removeメソッドの利用例
            Console.WriteLine(abbrs.Remove("WHO") ? "削除が完了しました" : "削除失敗");
            Console.WriteLine();

            // 8.2.4
            foreach (var (key, value) in abbrs.FindLength(3)) {
                Console.WriteLine($"{key}={value}");
            }
        }
    }
}
