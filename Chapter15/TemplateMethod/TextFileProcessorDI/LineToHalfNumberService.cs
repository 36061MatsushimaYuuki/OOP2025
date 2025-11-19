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
                output.Append((c >= '０' && c <= '９') ? (char)(c - 0xFEE0) : c);
            }
            Console.WriteLine(output);
        }

        public void Terminate() {
            return;
        }
    }
}
