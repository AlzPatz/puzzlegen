namespace LinearPuzzles
{
    internal class PuzzleScreenWriter : IPuzzleScreenWriter
    {
        public void Write(List<LinearPuzzle> puzzles)
        {
            Console.WriteLine();
            Console.WriteLine("Number of Puzzles: " + puzzles.Count.ToString());
            Console.WriteLine();

            puzzles.ForEach(p =>
            {
                Console.WriteLine(p.N0.ToString() + " " + p.O0 + " " +
                                    p.N1.ToString() + " " + p.O1 + " " +
                                    p.N2.ToString() + " " + p.O2 + " " +
                                    p.N3.ToString() + " " + p.O3 + " " +
                                    p.N4.ToString() + " " + p.O4 + " " +
                                    p.N5.ToString() + " = " + p.Result.ToString());
            });
        }
    }
}