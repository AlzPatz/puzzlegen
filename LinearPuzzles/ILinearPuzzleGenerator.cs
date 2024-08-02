namespace LinearPuzzles
{
    internal interface ILinearPuzzleGenerator
    {
        List<LinearPuzzle> Generate(int numberOfPuzzles = 25, int maxValue = 20, int maxCalculationResult = 120, bool avoidDivideByOne = true);
        List<LinearPuzzle> GenerateDistribution(int minNumberPuzzles = 480,
                                                int maxValue = 20,
                                                int maxCalculationResult = 120,
                                                bool returnShuffled = true,
                                                bool avoidDivideByOne = true);
    }
}