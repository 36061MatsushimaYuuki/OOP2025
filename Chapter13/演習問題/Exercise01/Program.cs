
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
            var maxPrice = Library.Books
                .MaxBy(x => x.Price);
            Console.WriteLine(maxPrice);
        }

        private static void Exercise1_3() {
            var groups = Library.Books
                .GroupBy(b => b.PublishedYear);
            foreach (var group in groups) {
                Console.WriteLine($"{group.Key}: {group.Count()}");
            }
        }

        private static void Exercise1_4() {
            
        }

        private static void Exercise1_5() {
            
        }

        private static void Exercise1_6() {
            
        }

        private static void Exercise1_7() {
            
        }

        private static void Exercise1_8() {
            
        }
    }
}
