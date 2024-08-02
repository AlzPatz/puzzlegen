using LinearPuzzles;

Console.WriteLine("Linear Puzzle Creation");

ILinearPuzzleGenerator _generator = new LinearPuzzleGenerator();
IPuzzleScreenWriter _screenWriter = new PuzzleScreenWriter();
IPuzzleFileWriter _fileWriter = new PuzzleFileWriter();

//var puzzles = _generator.Generate();
var puzzles = _generator.GenerateDistribution(480);


_screenWriter.Write(puzzles);
_fileWriter.Write(puzzles);

Console.ReadLine();