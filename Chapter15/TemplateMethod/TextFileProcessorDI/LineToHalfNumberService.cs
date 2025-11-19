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
            string output = "";
            foreach (char c in line) {
                if(c >= '０' && c <= '９') {
                    output += (char)(c - 0xFEE0);
                    _count++;
                } else {
                    output += c;
                }
            }
            Console.WriteLine(output);
        }

        public void Terminate() {
            Console.WriteLine($"(半角への変換数：{_count})");
        }
    }
}
