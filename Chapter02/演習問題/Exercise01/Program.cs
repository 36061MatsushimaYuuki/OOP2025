using System;

namespace Exercise01 {
    public class Program {
        static void Main(string[] args) {
            // 2-1-3
            var songs = new List<Song>();

            Console.WriteLine("***** 曲の登録 *****\n");

            while (true) {

                // 曲名の設定
                Console.Write("曲名：");
                string? title = Console.ReadLine();
                // 大文字小文字を区別しない
                if (title.Equals("") || title.Equals("end", StringComparison.OrdinalIgnoreCase)) {
                    Console.WriteLine();
                    break; // ループを抜ける
                    /* return では処理が行われない */
                }

                // 二回目に入ったか判定する
                Boolean isLoop = false;

                // アーティスト名の設定
                string? artistName;
                do {
                    if (isLoop) {
                        Console.WriteLine("エラー：アーティスト名が入力されていません\n");
                    }
                    Console.Write("アーティスト名：");
                    artistName = Console.ReadLine();
                    isLoop = true; // 二回目判定用
                } while (artistName.Equals(""));

                isLoop = false;

                // 演奏時間の設定
                string? stringLength;
                int length = 0;
                do {
                    if (isLoop) {
                        Console.WriteLine("エラー：正しい数値を入力してください\n");
                    }
                    Console.Write("演奏時間（秒）：");
                    stringLength = Console.ReadLine();
                    isLoop = true; // 二回目判定用
                } while (!int.TryParse(stringLength, out length));

                isLoop = false;

                // リストに追加
                var song = new Song(title, artistName, length);

                /*
                var song = new Song() {
                    Title = title,
                    ArtistName = artistName,
                    Length = length
                };
                */

                songs.Add(song);
                Console.WriteLine();
            }

            if (songs.Count == 0) {
                Console.WriteLine("曲が登録されていません");
            } else {
                Console.WriteLine("***** 登録曲一覧 *****");
                PrintSongs(songs);
            }
        }

        // 2-1-4
        private static void PrintSongs(IEnumerable<Song> songs) {
#if false
            foreach(var song in songs) {
                var minutes = song.Length / 60;
                var seconds = song.Length % 60;
                Console.WriteLine($"{song.Title}, {song.ArtistName} {minutes}:{seconds:00}");
            }
#else
            // TimeSpan構造体を使った場合
            var sumTimespan = TimeSpan.Zero;
            foreach (var song in songs) {
                var timespan = TimeSpan.FromSeconds(song.Length);
                sumTimespan += timespan;
                Console.WriteLine($"{song.Title}, {song.ArtistName} {timespan.ToString(@"m\:ss")}");
            }
            Console.WriteLine();
            Console.WriteLine($"計：{songs.Count}件 合計時間：{sumTimespan.ToString(@"m\:ss")}");
#endif
        }
    }
}
