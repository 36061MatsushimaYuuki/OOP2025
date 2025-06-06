using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01
{
    public class YearMonth
    {
        //private readonly int _year;
        //private readonly int _month;
        public int Year { get; init; }
        public int Month { get; init; }

        public bool Is21Century => 2001 <= Year && Year <= 2100;

        public YearMonth AddOneMonth() {
            bool isLastMonth = Month == 12;
            int calcYear = isLastMonth ? Year + 1 : Year;
            int calcMonth = isLastMonth ? 1 : Month + 1;
            return new YearMonth(calcYear, calcMonth);
        }

        public YearMonth(int year, int month) {
            Year = year;
            Month = month;
        }

        public override string ToString() => $"{Year}年{Month}月";
    }
}
