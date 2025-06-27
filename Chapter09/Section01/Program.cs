using System.Globalization;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {

            var today = new DateTime(2025, 7, 12); //日付
            var now = DateTime.Now;     //日付と時刻

            /*なんか消えたので萎えた*/

            //Console.WriteLine($"Today:{today.Month}");
            //Console.WriteLine($"Now:{now}");

            //自分の生年月日は何曜日かをプログラムを書いて調べる
            Console.WriteLine("日付を入力");
            Console.Write("西暦: ");
            var year = int.Parse(Console.ReadLine());
            Console.Write("月: ");
            var month = int.Parse(Console.ReadLine());
            Console.Write("日: ");
            var day = int.Parse(Console.ReadLine());

            var birthday = new DateTime(year, month, day);
            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            var japaneseDate = birthday.ToString("ggyy年M月d日", culture); //和暦変換
            var weekday = culture.DateTimeFormat.GetDayName(birthday.DayOfWeek);

            //日数の計算
            var livingDate = now.Date - birthday.Date;

            Console.WriteLine($"{japaneseDate}は{weekday}です。");
            Console.WriteLine($"生まれてから{livingDate.Days}日目です");

            var dayOfYear = now.DayOfYear;

            Console.WriteLine($"1月1日から{dayOfYear}日目です");

            var age = GetAge(birthday, now);

            Console.WriteLine($"あなたは{age}歳です");

            //うるう年の判定プログラムを作成する
            Console.WriteLine();
            Console.WriteLine("閏年判定プログラム");
            Console.Write("西暦を入力: ");
            var checkyear = int.Parse(Console.ReadLine());

            var message = DateTime.IsLeapYear(checkyear) ? "閏年" : "平年" ;
            Console.WriteLine($"{checkyear}年は{message}です");

            Console.WriteLine();
            Console.WriteLine("現在の時刻");
            while (true) {
                var nowTime = DateTime.Now;
                Console.Write($"\r{nowTime}");
            }
        }

        static int GetAge(DateTime birthday, DateTime targetDay) {
            var age = targetDay.Year - birthday.Year;
            if(targetDay < birthday.AddYears(age)) {
                age--;
            }
            return age;
        }
    }
}
