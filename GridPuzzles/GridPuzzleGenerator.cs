using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridPuzzles
{
    internal class GridPuzzleGenerator : IGridPuzzleGenerator
    {
        public GridPuzzle Generate()
        {
            var grid = new GridPuzzle();

            Dictionary<string, Operator> Operators = new Dictionary<string, Operator>();

            Operators.Add("+", new Operator() { Text = "+", Calc = (x, y) => x + y });
            Operators.Add("-", new Operator() { Text = "-", Calc = (x, y) => x - y });
            Operators.Add("x", new Operator() { Text = "x", Calc = (x, y) => x * y });

            var ops = new string[] { "+", "-", "x" };

            var rnd = new Random();

            var numbers = new int[4];

            do
            {
                for (var n = 0; n < 4; n++)
                {
                    var valid = false;
                    var number = -1;
                    while (!valid)
                    {
                        number = rnd.Next(9) + 1; // 1 to 9 
                        if (n > 0)
                        {
                            var found = false;

                            for (var m = 0; m < n; m++)
                            {
                                if (number == numbers[m])
                                {
                                    found = true;
                                }
                            }

                            valid = !found;
                        }
                        else
                        {
                            valid = true;
                        }
                    }
                    numbers[n] = number;
                }

                Func<int, int, int, int[,], bool> ValidatePotentialGridNumber = (col, row, num, grid) =>
                    {
                        //Check hasn't been used so far in column
                        if (row > 0)
                        {
                            for (var r = 0; r < row; r++)
                            {
                                if (grid[col, r] == num)
                                {
                                    return false;
                                }
                            }
                        }

                        //Check hasn't been used so far in row
                        if (col > 0)
                        {
                            for (var c = 0; c < col; c++)
                            {
                                if (grid[c, row] == num)
                                {
                                    return false;
                                }
                            }
                        }

                        //Pass the use of the number
                        return true;
                    };

                //Put the numbers into a grid
                grid.Numbers = new int[4, 4];

                var pass = false;
                for (var row = 0; row < 4; row++)
                {
                    for (var col = 0; col < 4; col++)
                    {
                        if (!SolutionPossible(grid.Numbers, numbers, col, row))
                        {
                            col = 5;
                            row = -1;
                        }
                        else
                        {
                            pass = false;
                            var num = -1;
                            while (!pass)
                            {
                                var rndIndex = rnd.Next(4);
                                num = numbers[rndIndex];
                                pass = ValidatePotentialGridNumber(col, row, num, grid.Numbers);
                            }
                            grid.Numbers[col, row] = num;
                        }
                    }
                }

                //Generate operators
                grid.HorizontalOperators = new Operator[3, 4];
                grid.VerticalOperators = new Operator[4, 3];

                pass = false;
                for (var row = 0; row < 4; row++)
                {
                    for (var col = 0; col < 4; col++)
                    {
                        var op = "";

                        if (col < 3)
                        {
                            pass = false;
                            op = "";
                            while (!pass)
                            {
                                var rndIndex = rnd.Next(3);
                                op = ops[rndIndex];
                                pass = ValidatePotentialHorizontalGridOperator(col, row, op, grid.HorizontalOperators);
                            }

                            grid.HorizontalOperators[col, row] = Operators[op];
                        }

                        if (row < 3)
                        {
                            pass = false;
                            op = "";
                            while (!pass)
                            {
                                var rndIndex = rnd.Next(3);
                                op = ops[rndIndex];
                                pass = ValidatePotentialVerticalGridOperator(col, row, op, grid.VerticalOperators);
                            }

                            grid.VerticalOperators[col, row] = Operators[op];
                        }
                    }
                }

                CalculateResults(grid);

            } while (!Validate(grid));

            return grid;
        }

        private bool SolutionPossible(int[,] grid, int[] numbers, int col, int row)
        {
            var available = new bool[] { true, true, true, true };

            if (row > 0)
            {
                for (var r = 0; r < row; r++)
                {
                    var nOnCol = grid[col, r];
                    for (var n = 0; n < 4; n++)
                    {
                        if (numbers[n] == nOnCol)
                        {
                            available[n] = false;
                        }
                    }
                }
            }

            if (col > 0)
            {
                for (var c = 0; c < col; c++)
                {
                    var nOnRow = grid[c, row];
                    for (var n = 0; n < 4; n++)
                    {
                        if (numbers[n] == nOnRow)
                        {
                            available[n] = false;
                        }
                    }
                }
            }

            for (var n = 0; n < 4; n++)
            {
                if (available[n])
                {
                    return true;
                }
            }
            return false;
        }

        private void CalculateResults(GridPuzzle grid)
        {
            grid.VerticalResults = new int[4];
            grid.HorizontalResults = new int[4];

            for (var col = 0; col < 4; col++)
            {
                grid.VerticalResults[col] = grid.VerticalOperators[col, 2].Calc(
                                            grid.VerticalOperators[col, 1].Calc(
                                            grid.VerticalOperators[col, 0].Calc(
                                                grid.Numbers[col, 0],
                                                grid.Numbers[col, 1]),
                                                grid.Numbers[col, 2]),
                                                grid.Numbers[col, 3]);
            }

            for (var row = 0; row < 4; row++)
            {
                grid.HorizontalResults[row] = grid.HorizontalOperators[2, row].Calc(
                                            grid.HorizontalOperators[1, row].Calc(
                                            grid.HorizontalOperators[0, row].Calc(
                                                grid.Numbers[0, row],
                                                grid.Numbers[1, row]),
                                                grid.Numbers[2, row]),
                                                grid.Numbers[3, row]);
            }
        }

        private bool ValidatePotentialHorizontalGridOperator(int col, int row, string op, Operator[,] horizontalOperators)
        {
            if (col > 0)
            {
                for (var c = 0; c < col; c++)
                {
                    if (op == horizontalOperators[c, row].Text)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool ValidatePotentialVerticalGridOperator(int col, int row, string op, Operator[,] verticalOperators)
        {
            if (row > 0)
            {
                for (var r = 0; r < row; r++)
                {
                    if (op == verticalOperators[col, r].Text)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool Validate(GridPuzzle grid)
        {
            //Add other validation here?

            //remove multiple by 1? (option..)
            for(var n = 0;n < 4; n++)
            {
                for (var o = 0; o < 3; o++)
                {
                    if (grid.HorizontalOperators[o, n].Text == "x")
                    {
                        if (grid.Numbers[o + 1, n] == 1)
                        {
                            return false;
                        }
                    }
                    if (grid.VerticalOperators[n, o].Text == "x")
                    {
                        if (grid.Numbers[n ,o + 1] == 1)
                        {
                            return false;
                        }
                    }
                }
            }

            //remove negative number reults
            for(var n = 0; n < 4; n++)
            {
                if (grid.VerticalResults[n] <= 0)
                {
                    return false;
                }
                if (grid.HorizontalResults[n] <= 0)
                {
                    return false;
                }
            }

            //Don't let any row results equal columns even if calc is different
            for(var c = 0; c < 4;c++)
            {
                for(var r = 0; r < 4; r++)
                {
                    if (grid.HorizontalResults[c] == grid.VerticalResults[r])
                    {
                        return true;
                    }
                }
            }

            return true;
        }
    }
}