namespace Section01 {
    internal class Program {
        static void Main(string[] args) {

            var books = Books.GetBooks();

            //本の平均金額を表示
            Console.WriteLine("---本の平均金額---");
            Console.WriteLine((int)books.Average(b => b.Price));
            Console.WriteLine();

            //本のページ合計を表示
            Console.WriteLine("---本のページ合計---");
            Console.WriteLine(books.Sum(b => b.Pages));
            Console.WriteLine();

            //金額の安い書籍名と金額を表示
            Console.WriteLine("---1番安い書籍名と金額---");
            //var minPriceBook = books.MinBy(b => b.Price); // 1件だけ
            //Console.WriteLine($"タイトル: {minPriceBook.Title} 金額: {minPriceBook.Price}");
            //Console.WriteLine();
            var minPriceBooks = books.Where(b => b.Price == books.Min(x => x.Price));
            foreach(var book in minPriceBooks) {
                Console.WriteLine($"{book.Title} 金額: {book.Price}");
            }
            Console.WriteLine();

            //ページが多い書籍名とページ数を表示
            Console.WriteLine("---1番ページが多い書籍名とページ数---");
            //var maxPagesBook = books.MaxBy(b => b.Pages); // 1件だけ
            //Console.WriteLine($"タイトル: {maxPagesBook.Title} ページ数: {maxPagesBook.Pages}");
            //Console.WriteLine();
            var maxPagesBooks = books.Where(b => b.Pages == books.Max(x => x.Pages));
            foreach(var book in maxPagesBooks) {
                Console.WriteLine($"{book.Title} ページ数: {book.Pages}");
            }
            Console.WriteLine();

            //タイトルに物語が含まれている書籍名をすべて表示
            Console.WriteLine("---タイトルに物語が含まれている書籍名---");
            var selected = books.Where(b => b.Title.Contains("物語"));
            foreach(var book in selected) {
                Console.WriteLine(book.Title);
            }
        }
    }
}
