using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridPuzzles
{
    internal class PuzzleScreenWriter : IPuzzleScreenWriter
    {
        public void Write(GridPuzzle puzzle)
        {
            for (var row = 0; row < 4; row++)
            {
                //Write the horizontal calculation and result
                Console.WriteLine(puzzle.Numbers[0, row] + "\t" + puzzle.HorizontalOperators[0, row].Text + "\t" +
                                    puzzle.Numbers[1, row] + "\t" + puzzle.HorizontalOperators[1, row].Text + "\t" +
                                    puzzle.Numbers[2, row] + "\t" + puzzle.HorizontalOperators[2, row].Text + "\t" +
                                    puzzle.Numbers[3, row] + "\t" + " = " + puzzle.HorizontalResults[row]);
                if (row < 3)
                {
                    Console.WriteLine(puzzle.VerticalOperators[0, row].Text + "\t\t" +
                                        puzzle.VerticalOperators[1, row].Text + "\t\t" +
                                        puzzle.VerticalOperators[2, row].Text + "\t\t" +
                                        puzzle.VerticalOperators[3, row].Text);
                }

                if (row == 3)
                {
                    Console.WriteLine("=\t\t=\t\t=\t\t=");
                    Console.WriteLine(puzzle.VerticalResults[0] + "\t\t" +
                                        puzzle.VerticalResults[1] + "\t\t" +
                                        puzzle.VerticalResults[2] + "\t\t" +
                                        puzzle.VerticalResults[3]);
                }
            }
        }
    }
}