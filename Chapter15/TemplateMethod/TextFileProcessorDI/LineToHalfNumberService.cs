using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    public class LineToHalfNumberService : ITextFileService {
        private int _count = 0;

        public void Initialize(string fname) =>
            _count = 0;
        
        public void Execute(string line) {
            //string.Normalize だと英字も変換されてしまうため採用しない
            var output = new StringBuilder();
            foreach (char c in line) {
                if(c >= '０' && c <= '９') {
                    output.Append((char)(c - 0xFEE0));
                    _count++;
                } else {
                    output.Append(c);
                }
            }
            Console.WriteLine(output);
        }

        public void Terminate() {
            Console.WriteLine($"(半角への変換数：{_count})");
        }
    }
}
