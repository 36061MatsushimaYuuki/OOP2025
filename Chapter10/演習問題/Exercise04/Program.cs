namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            var di = new DirectoryInfo("./");
            var files = di.EnumerateFiles("*", SearchOption.AllDirectories)
                .Where(f => f.Length >= 1048576);
            foreach (var file in files) {
                Console.WriteLine(file.Name);
            }
        }
    }
}
