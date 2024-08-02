using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridPuzzles
{
    internal interface IGridPuzzleGenerator
    {
        GridPuzzle Generate();
    }
}
