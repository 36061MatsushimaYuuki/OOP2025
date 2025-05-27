namespace LinqSample {
    internal class Program {
        static void Main(string[] args) {

            var numbers = Enumerable.Range(1, 100);

            // 8の倍数の合計を出力
            Console.WriteLine("---8の倍数の合計を出力---");
            Console.WriteLine(numbers.Where(n => n % 8 == 0).Sum());

            //foreach(var num in numbers) {
            //    Console.WriteLine(num);
            //}
        }
    }
}
