using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01
{
    public class YearMonth
    {
        private readonly int _year;
        private readonly int _month;

        public bool Is21Century => _year > 2000 && _year <= 2100;

        public YearMonth AddOneMonth() {
            bool isLastMonth = _year == 12;
            int calcYear = isLastMonth ? _year + 1 : _year;
            int calcMonth = isLastMonth ? 1 : _month + 1;
            return new YearMonth(calcYear, calcMonth);
        }

        public YearMonth(int year, int month) {
            _year = year;
            _month = month;
        }
    }
}
