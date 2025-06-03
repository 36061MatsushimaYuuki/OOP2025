namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            var line = "Novelist=谷崎潤一郎;BestWork=春琴抄;Born=1886";

            var texts = line.Split(';', '=');
            foreach (var text in texts) {
                line = line.Replace(text, ToJapanese(text));
            }
            line = line.Replace("=", ": ").Replace(";", "\n");
            Console.WriteLine(line);
        }

        /// <summary>
        /// 引数の単語を日本語へ変換します
        /// </summary>
        /// <param name="key">"Novelist","BestWork","Born"</param>
        /// <returns>"「作家」,「代表作」,「誕生年」</returns>
        static string ToJapanese(string key) {
            var result = key switch {
               "Novelist" => "作家",
               "BestWork" => "代表作",
               "Born" => "誕生年",
               _ => key,
            };
            return result; //エラーをなくすためのダミー
        }
    }
}