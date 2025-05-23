
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            List<string> langs = [
                "C#", "Java", "Ruby", "PHP", "Python", "TypeScript",
                "JavaScript", "Swift", "Go",
            ];

            Exercise1(langs);
            Console.WriteLine("---");
            Exercise2(langs);
            Console.WriteLine("---");
            Exercise3(langs);

        }

        private static void Exercise1(List<string> langs) {
            // foreach
            foreach (var lang in langs) {
                if (lang.Contains('S'))
                    Console.WriteLine(lang);
            }
            Console.WriteLine();

            // for
            for (var cnt = 0; cnt < langs.Count; cnt++) {
                if (langs[cnt].Contains('S')) {
                    Console.WriteLine(langs[cnt]);
                }
            }
            Console.WriteLine();

            // while
            var cnt2 = 0;
            while (cnt2 < langs.Count) {
                if (langs[cnt2].Contains('S')) {
                    Console.WriteLine(langs[cnt2]);
                }
                cnt2++;
            }
        }

        private static void Exercise2(List<string> langs) {
            langs.FindAll(s => s.Contains('S')).ForEach(s => Console.WriteLine(s));
        }

        private static void Exercise3(List<string> langs) {
            var lang = langs.Find(s => s.Length == 10) ?? "unknown";
            Console.WriteLine(lang);
        }
    }
}
