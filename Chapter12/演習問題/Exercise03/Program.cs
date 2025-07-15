using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var employees = Deserialize("employees.json");
            ToXmlFile(employees);
        }

        static Employee[]? Deserialize(string filePath) {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Employee[]>(text, options);
        }

        static void ToXmlFile(Employee[]? employees) {
            try {
                using (var writer = XmlWriter.Create("employees.xml")) {
                    XmlRootAttribute xRoot = new XmlRootAttribute { //ルートの要素名(タグ名)の変更
                        ElementName = "Employees"
                    };
                    var serializer = new XmlSerializer(employees.GetType(), xRoot);
                    serializer.Serialize(writer, employees);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("エラー: " + ex.Message);
            }
        }
    }

    public record Employee {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime HireDate { get; set; }
    }
}
