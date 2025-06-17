using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test01 {
    public class Student {
        //学生の名前
        public required string Name { get; init; } = string.Empty;
        //科目名
        public required string Subject { get; init; } = string.Empty;
        //点数
        public required int Score { get; init; }
    }
}
