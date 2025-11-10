namespace Section02 {
    internal class Program {
        static void Main(string[] args) {
            //ファイルへ出力
            var filePath = "./いろは歌.txt";
            using (var writer = new StreamWriter(filePath)) {
                writer.WriteLine("色はにほえど　散りぬるを");
                writer.WriteLine("我が世たれぞ　常ならむ");
                writer.WriteLine("有為の奥山　今日越えて");
                writer.WriteLine("浅き夢見じ　酔ひもせず");
            }
            //既存のファイルへ付け足し
            var lines = new[] { "====", "京の夢", "大坂の夢", };
            using (var writer = new StreamWriter(filePath, append: true)) {
                foreach (var line in lines)
                    writer.WriteLine(line);
            }
            //先頭に行を挿入する
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) {
                using var reader = new StreamReader(stream);
                using var writer = new StreamWriter(stream);

                string texts = reader.ReadToEnd();
                stream.Position = 0;
                writer.WriteLine("いろは歌は神");
                writer.WriteLine("");
                writer.Write(texts);
            }
        }
    }
}
