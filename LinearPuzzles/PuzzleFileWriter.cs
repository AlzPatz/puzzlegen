namespace LinearPuzzles
{
    internal class PuzzleFileWriter : IPuzzleFileWriter
    {
        public void Write(List<LinearPuzzle> puzzles)
        {
            var puzzleWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\puzzles.txt");
            var answerWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\answers.txt");

            var index = 0;

            foreach(var puzzle in puzzles)
            {
                var line = "";

                line += index.ToString() + " ->\t";
                line += "Total: " + puzzle.Result.ToString();
                line += "\t|\tNumbers: ";
                List<int> ints = new List<int>() { puzzle.N0, puzzle.N1, puzzle.N2, puzzle.N3, puzzle.N4 };
                ints.Order();
                for (var n = 0; n < 5; n++)
                {
                    line += ints[n].ToString();
                    if (n < 4)
                    {
                        line += " , ";
                    }
                }

                puzzleWriter.WriteLine(line);

                answerWriter.WriteLine(index.ToString() + " ->\t" +
                                    puzzle.N0.ToString() + " " + puzzle.O0 + " " +
                                    puzzle.N1.ToString() + " " + puzzle.O1 + " " +
                                    puzzle.N2.ToString() + " " + puzzle.O2 + " " +
                                    puzzle.N3.ToString() + " " + puzzle.O3 + " " +
                                    puzzle.N4.ToString() + " " + puzzle.O4 + " " +
                                    puzzle.N5.ToString() + " = " + puzzle.Result.ToString());

                index++;
            }

            puzzleWriter.Flush();
            puzzleWriter.Close();

            answerWriter.Flush();
            answerWriter.Close();
        }
    }
}
