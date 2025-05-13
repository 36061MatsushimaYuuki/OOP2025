namespace Exercise01 {
    public class Program {
        static void Main(string[] args) {
            // 2-1-3
            var songs = new Song[] {
                new Song("Let it be", "The Beatles", 243),
                new Song("Bridge Over Troubled Water", "Simon & Garfunel", 293),
                new Song("Close To You", "Carpenters", 276),
                new Song("Honesty", "Billy Joel", 231),
                new Song("I Will Always Love You", "Whitney Houston", 273),
            };

            PrintSongs(songs);
        }

        // 2-1-4
        private static void PrintSongs(Song[] songs) {
#if false
            foreach(var song in songs) {
                var minutes = song.Length / 60;
                var seconds = song.Length % 60;
                Console.WriteLine($"{song.Title}, {song.ArtistName} {minutes}:{seconds:00}");
            }
#else
            // TimeSpan構造体を使った場合
            foreach(var song in songs) {
                var timespan = TimeSpan.FromSeconds(song.Length);
                Console.WriteLine($"{song.Title}, {song.ArtistName} {timespan.ToString(@"m\:ss")}");
            }
#endif
        }
    }
}
