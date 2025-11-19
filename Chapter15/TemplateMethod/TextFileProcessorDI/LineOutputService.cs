using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    public class LineOutputService : ITextFileService {
        private int _count = 0;

        public void Initialize(string fname) =>
            _count = 0;

        public void Execute(string line) {
            _count++;
            if (_count <= 20) {
                Console.WriteLine(line);
            }
        }

        public void Terminate() {
            if(_count > 20) {
                Console.WriteLine($"(残り {_count - 20} 行省略)");
            }
        }
    }
}
