namespace GridPuzzles
{
    internal class PuzzleFileWriter : IPuzzleFileWriter
    {
        public void Write(GridPuzzle[] puzzles)
        {
            var puzzleWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\grid_puzzles.txt");
            var answerWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\grid_answers.txt");

            var index = 0;

            foreach (var puzzle in puzzles)
            {
                var title = index.ToString() + ":";

                puzzleWriter.WriteLine(title);
                answerWriter.WriteLine(title);

                for (var row = 0; row < 4; row++)
                {
                    if (row == 0)
                    {
                        puzzleWriter.WriteLine(puzzle.Numbers[0, row] + "\t" + puzzle.HorizontalOperators[0, row].Text + "\t" +
                                 puzzle.Numbers[1, row] + "\t" + puzzle.HorizontalOperators[1, row].Text + "\t" +
                                 puzzle.Numbers[2, row] + "\t" + puzzle.HorizontalOperators[2, row].Text + "\t" +
                                 puzzle.Numbers[3, row] + "\t" + " = " + puzzle.HorizontalResults[row]);

                    }
                    else
                    {
                        puzzleWriter.WriteLine("_" + "\t" + "?" + "\t" +
                             "_" + "\t" + "?" + "\t" +
                             "_" + "\t" + "?"+ "\t" +
                             "_" + "\t" + " = " + puzzle.HorizontalResults[row]);
                    }

                    answerWriter.WriteLine(puzzle.Numbers[0, row] + "\t" + puzzle.HorizontalOperators[0, row].Text + "\t" +
                                        puzzle.Numbers[1, row] + "\t" + puzzle.HorizontalOperators[1, row].Text + "\t" +
                                        puzzle.Numbers[2, row] + "\t" + puzzle.HorizontalOperators[2, row].Text + "\t" +
                                        puzzle.Numbers[3, row] + "\t" + " = " + puzzle.HorizontalResults[row]);

                    if (row < 3)
                    {
                        puzzleWriter.WriteLine("?" + "\t\t" +
                           "?" + "\t\t" +
                           "?" + "\t\t" +
                           "?");

                        answerWriter.WriteLine(puzzle.VerticalOperators[0, row].Text + "\t\t" +
                                            puzzle.VerticalOperators[1, row].Text + "\t\t" +
                                            puzzle.VerticalOperators[2, row].Text + "\t\t" +
                                            puzzle.VerticalOperators[3, row].Text);
                    }

                    if (row == 3)
                    {
                        puzzleWriter.WriteLine("=\t\t=\t\t=\t\t=");
                        puzzleWriter.WriteLine(puzzle.VerticalResults[0] + "\t\t" +
                                            puzzle.VerticalResults[1] + "\t\t" +
                                            puzzle.VerticalResults[2] + "\t\t" +
                                            puzzle.VerticalResults[3]);

                        answerWriter.WriteLine("=\t\t=\t\t=\t\t=");
                        answerWriter.WriteLine(puzzle.VerticalResults[0] + "\t\t" +
                                            puzzle.VerticalResults[1] + "\t\t" +
                                            puzzle.VerticalResults[2] + "\t\t" +
                                            puzzle.VerticalResults[3]);

                    }
                }

                puzzleWriter.WriteLine();
                puzzleWriter.WriteLine("--------------------");
                puzzleWriter.WriteLine();

                answerWriter.WriteLine();
                answerWriter.WriteLine("--------------------");
                answerWriter.WriteLine();
                
                index++;
            }

            puzzleWriter.Flush();
            puzzleWriter.Close();

            answerWriter.Flush();
            answerWriter.Close();
        }
    }
}
