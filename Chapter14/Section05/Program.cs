namespace Section05 {
    internal class Program {
        static void Main(string[] args) {
            var selected = Library.Books
                .AsParallel()
                .AsOrdered()
                .Where(b => b.Price > 500 && b.Price < 2000)
                .Select(b => new { b.Title });

            foreach(var item in selected) {
                Console.WriteLine(item.Title);
            }
        }
    }
}
