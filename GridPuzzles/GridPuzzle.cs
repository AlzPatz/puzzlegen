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