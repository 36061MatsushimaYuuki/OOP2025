
using System.Net.WebSockets;
using System.Security.Cryptography;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1_2();
            Console.WriteLine();
            Exercise1_3();
            Console.WriteLine();
            Exercise1_4();
            Console.WriteLine();
            Exercise1_5();
            Console.WriteLine();
            Exercise1_6();
            Console.WriteLine();
            Exercise1_7();
            Console.WriteLine();
            Exercise1_8();

            Console.ReadLine();
        }

        private static void Exercise1_2() {
            Console.WriteLine("[2] -");
            var maxPrice = Library.Books
                .MaxBy(x => x.Price);
            Console.WriteLine(maxPrice);
        }

        private static void Exercise1_3() {
            Console.WriteLine("[3] -");
            var groups = Library.Books
                .GroupBy(b => b.PublishedYear);
            foreach (var group in groups) {
                Console.WriteLine($"{group.Key}: {group.Count()}");
            }
        }

        private static void Exercise1_4() {
            Console.WriteLine("[4] -");
            var yearPriceOrder = Library.Books
                .OrderByDescending(b => b.PublishedYear)
                .ThenByDescending(b => b.Price);
            foreach (var book in yearPriceOrder) {
                Console.WriteLine($"{book.PublishedYear}年 {book.Price}円 {book.Title}");
            }
        }

        private static void Exercise1_5() {
            Console.WriteLine("[5] -");
            var categories = Library.Books
                .Where(b => b.PublishedYear == 2022)
                .Join(Library.Categories,
                    book => book.CategoryId,
                    category => category.Id,
                    (book, category) => category.Name)
                .Distinct();
            foreach (var name in categories) {
                Console.WriteLine(name);
            }
        }

        private static void Exercise1_6() {
            Console.WriteLine("[6] -");
            var groups = Library.Books
                .Join(Library.Categories,
                    book => book.CategoryId,
                    category => category.Id,
                    (book, category) => new {
                        Category = category.Name,
                        book.Title
                    }
                )
                .GroupBy(b => b.Category)
                .OrderBy(g => g.Key);
            foreach (var group in groups) {
                Console.WriteLine($"# {group.Key}");
                foreach (var book in group) {
                    Console.WriteLine($"   {book.Title}");
                }
            }
        }

        private static void Exercise1_7() {
            Console.WriteLine("[7] -");
            var groups = Library.Books
                .Join(Library.Categories,
                    b => b.CategoryId,
                    c => c.Id,
                    (b, c) => new {
                        Category = c.Name,
                        b.PublishedYear,
                        b.Title
                    }
                )
                .Where(b => b.Category.Equals("Development"))
                .GroupBy(b => b.PublishedYear);
           foreach (var group in groups) {
                Console.WriteLine($"# {group.Key}");
                foreach (var book in group) {
                    Console.WriteLine($"   {book.Title}");
                }
           }
        }

        private static void Exercise1_8() {
            Console.WriteLine("[8] -");
            var groups = Library.Categories
                .GroupJoin(Library.Books,
                    c => c.Id,
                    b => b.CategoryId,
                    (c, books) => new {
                        Category = c.Name,
                        Count = books.Count(),
                    }
                )
                .Where(c => c.Count >= 4);
            foreach (var obj in groups) {
                Console.WriteLine($"{obj.Category}");
            }
        }
    }
}
