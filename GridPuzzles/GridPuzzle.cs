using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GridPuzzles
{
    internal class GridPuzzle
    {
        public int[,] Numbers { get; set; }
        public Operator[,] HorizontalOperators { get; set; }
        public Operator[,] VerticalOperators { get; set; }
        public int[] HorizontalResults { get; set; }
        public int[] VerticalResults { get; set; }
    }
}