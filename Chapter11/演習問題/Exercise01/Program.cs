
using System.Text.RegularExpressions;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine(IsPhoneNumber("080-9111-1234"));
            Console.WriteLine(IsPhoneNumber("090-9111-1234"));
            Console.WriteLine(IsPhoneNumber("060-9111-1234"));
            Console.WriteLine(IsPhoneNumber("190-9111-1234"));
            Console.WriteLine(IsPhoneNumber("091-9111-1234"));
            Console.WriteLine(IsPhoneNumber("090-9111-12341"));
            Console.WriteLine(IsPhoneNumber("A090-9111-1234"));
            Console.WriteLine(IsPhoneNumber("090-911-1234"));
            Console.WriteLine(IsPhoneNumber("090-1911-234"));
            //追加分
            Console.WriteLine("-------------");
            Console.WriteLine(IsPhoneNumber("070-1911-234D"));
            Console.WriteLine(IsPhoneNumber("070-19A1-2342"));
            Console.WriteLine(IsPhoneNumber("070-19111-2341"));
        }

        private static bool IsPhoneNumber(string telNum) {
            return Regex.IsMatch(telNum, @"^0[7-9]0-\d{4}-\d{4}$");
        }
    }
}
