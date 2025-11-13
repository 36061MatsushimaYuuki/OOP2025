using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextFileProcessor;

namespace WordCounter {
    public class WordCounterProcessor : TextProcessor {
        private int _count = 0;
        public static string _word = "public";

        protected override void Initialize(string fname) =>
            _count = 0;

        protected override void Execute(string line) {
            var matches = Regex.Matches(line, _word, RegexOptions.IgnoreCase);
            foreach (var match in matches) {
                _count++;
            }
        }

        protected override void Terminate() =>
            Console.WriteLine($"{_word}は {_count} 個含まれています");
    }
}
