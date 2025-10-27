namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var price = Library.Books
                .Where(b => b.CategoryId == 1)
                .Max(b => b.Price);
            Console.WriteLine(price);

            Console.WriteLine(); //改行

            var book = Library.Books
                .Where(x => x.PublishedYear >= 2021)
                .MinBy(x => x.Price);
            Console.WriteLine(book);

            Console.WriteLine(); //改行

            var average = Library.Books
                .Average(x => x.Price);
            var aboves = Library.Books
                .Where(b => b.Price > average);
            foreach (var book1 in aboves) {
                Console.WriteLine(book1);
            }

            Console.WriteLine(); //改行

            var groups = Library.Books
                .GroupBy(b => b.PublishedYear)
                .OrderByDescending(g => g.Key);
            foreach (var group in groups) {
                Console.WriteLine($"{group.Key}年");
                foreach (var book2 in group) {
                    Console.WriteLine($"  {book2}");
                }
            }

            Console.WriteLine(); //改行

            var selected = Library.Books
                .GroupBy(b => b.PublishedYear)
                .Select(group => group.MaxBy(b => b.Price))
                .OrderBy(b => b!.PublishedYear);
            foreach (var book3 in selected) {
                //bookはnullではない
                Console.WriteLine($"{book3!.PublishedYear}年 {book3!.Title} ({book3!.Price})");
            }
        }
    }
}
