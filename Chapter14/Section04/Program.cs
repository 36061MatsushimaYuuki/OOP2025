namespace Section04 {
    internal class Program {
        static async Task Main(string[] args) {
            HttpClient hc = new HttpClient();
            await GetHtmlExample(hc);
        }

        static async Task GetHtmlExample(HttpClient httpClient) {
            var url = "https://abehiroshi.la.coocan.jp/";
            var text = await httpClient.GetStringAsync(url);
            Console.WriteLine(text);
        }
    }
}
