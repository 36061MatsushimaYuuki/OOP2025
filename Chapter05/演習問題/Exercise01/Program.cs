namespace Exercise01 {
    public class Program {
        static void Main(string[] args) {
            YearMonth date = new YearMonth(2025, 12);
            Console.WriteLine(date);
            Console.WriteLine(date.Is21Century);
            Console.WriteLine(date.AddOneMonth());
            Console.WriteLine(date);
        }
    }
}
