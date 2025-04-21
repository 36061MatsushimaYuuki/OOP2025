namespace ProductSample {
    internal class Program {
        static void Main(string[] args) {

            Product karinto = new Product(123, "かりんとう", 180);
            Product ramen = new Product(240, "ラーメン", 300);

            // 税抜きの価格を表示【かりんとうの税抜き価格は○○円です】
            Console.WriteLine(karinto.Name + "の税抜き価格は" + karinto.Price + "円です");

            // 消費税額の表示【かりんとうの消費税額は○○円です】
            Console.WriteLine(karinto.Name + "の消費税額は" + karinto.GetTax() + "円です");

            // 税込価格の表示【かりんとうの税込価格は○○円です】
            Console.WriteLine(karinto.Name + "の税込価格は" + karinto.GetPriceIncludingTax() + "円です");

            Console.WriteLine(ramen.Name + "の税抜き価格は" + ramen.Price + "円です");

            Console.WriteLine(ramen.Name + "の消費税額は" + ramen.GetTax() + "円です");

            Console.WriteLine(ramen.Name + "の税込価格は" + ramen.GetPriceIncludingTax() + "円です");
        }
    }
}
