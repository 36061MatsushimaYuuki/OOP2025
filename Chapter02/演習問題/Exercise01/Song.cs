using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    // 2-1-1
    public class Song {

        /* プロパティ */
        public string Title { get; set; } = String.Empty;

        public string ArtistName { get; set; } = String.Empty;

        public int Length { get; set; }

        // 2-1-2
        public Song(string title, string artistName, int length) {
            this.Title = title;
            this.ArtistName = artistName;
            this.Length = length;
        }
    }
}
