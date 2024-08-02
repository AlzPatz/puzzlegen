using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridPuzzles
{
    internal class Operator
    {
        public string Text { get; set; }
        public Func<int, int, int> Calc { get; set; }
    }
}
