using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            var jsonString = File.ReadAllText("novelist.json");
            var novelist = Deserialize(jsonString);
            if (novelist is not null) {
                Console.WriteLine(novelist);
                foreach (var item in novelist.Masterpieces) {
                    Console.WriteLine(item);
                }
            }
        }

        static Novelist? Deserialize(string jsonString) {
            var options = new JsonSerializerOptions {
                NumberHandling = JsonNumberHandling.AllowReadingFromString, //文字列の数字を数値に変換する
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            return JsonSerializer.Deserialize<Novelist>(jsonString, options);
        }
    }

    public record Novelist {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        [JsonPropertyName("birth")] //プロパティのキー値を設定
        public DateTime Birthday { get; init; }
        public string[] Masterpieces { get; init; } = [];
    }
}
