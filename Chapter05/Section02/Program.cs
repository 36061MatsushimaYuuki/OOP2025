namespace Section02 {
    internal class Program {
        static void Main(string[] args) {

            var appVer1 = new AppVersion(5, 1);
            var appVer2 = new AppVersion(5, 1);

            if(appVer1 == appVer2) {
                Console.WriteLine("等しい");
            } else {
                Console.WriteLine("等しくない");
            }
        }
    }

    // オプション引数を使った定義
    public record AppVersion(int major, int minor, int build = 0, int revision = 0) {
        // プライマリーコンストラクタを使った定義
        public int Major { get; init; } = major;
        public int Minor { get; init; } = minor;
        public int Build { get; init; } = build;
        public int Revision { get; init; } = revision;

        public override string ToString() =>
            $"{Major}.{Minor}.{Build}.{Revision}";

    }
}
