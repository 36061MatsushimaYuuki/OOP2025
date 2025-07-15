using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("***** 12.1.1 *****\n");
            var emp = new Employee {
                Id = 123,
                Name = "山田太郎",
                HireDate = new DateTime(2018, 10, 1),
            };
            var jsonString = Serialize(emp);
            Console.WriteLine(jsonString);
            var obj = Deserialize(jsonString);
            Console.WriteLine(obj);

            //問題12.1.2
            Employee[] employees = [
                new () {
                    Id = 123,
                    Name = "山田太郎",
                    HireDate = new DateTime(2018, 10, 1),
                },
                new () {
                    Id = 198,
                    Name = "田中華子",
                    HireDate = new DateTime(2020, 4, 1),
                },
            ];
            Console.WriteLine("\n***** 12.1.2 *****\n");
            Console.WriteLine("ファイル出力なので特になし");
            Serialize("employees.json", employees);

            //問題12.1.3
            Console.WriteLine("\n***** 12.1.3 *****\n");
            var empdata = Deserialize_f("employees.json");
            foreach (var empd in empdata)
                Console.WriteLine(empd);
        }
        //問題12.1.1
        static string Serialize(Employee emp) {
            //キー名をCamelCaseに設定
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            return JsonSerializer.Serialize(emp, options);
        }

        static Employee? Deserialize(string text) {
            //キー名がCamelCaseなのでオプションに設定
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<Employee>(text, options);
        }

        //問題12.1.2
        //シリアル化してファイルへ出力する
        static void Serialize(string filePath, IEnumerable<Employee> employees) {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(employees, options);
            try {
                File.WriteAllText(filePath, jsonString); //ファイル出力
                //UTF-8バイト配列にシリアル化
                //byte[] utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(employees, options);
                //File.WriteAllBytes(filePath, utf8Bytes);
            }
            catch (Exception ex) {
                Console.WriteLine("エラー: " + ex.Message);
            }
        }

        //問題12.1.3
        //ファイルを読み込み逆シリアル化
        static Employee[]? Deserialize_f(string filePath) {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Employee[]>(text, options);
        }
    }

    public record Employee {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
    }
}
