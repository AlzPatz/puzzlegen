using GridPuzzles;

const int NUMBER_OF_PUZZLES = 50;

Console.WriteLine("Grid Puzzle Creation");

var puzzles = new GridPuzzle[NUMBER_OF_PUZZLES];

for (var n = 0; n < NUMBER_OF_PUZZLES; n++)
{
    Console.WriteLine();

    IGridPuzzleGenerator _generator = new GridPuzzleGenerator();
    IPuzzleScreenWriter _screenWriter = new PuzzleScreenWriter();

    puzzles[n] = _generator.Generate();

    _screenWriter.Write(puzzles[n]);
}

//Bother to check for duplicate puzzles? - nah not now

IPuzzleFileWriter _fileWriter = new PuzzleFileWriter();
_fileWriter.Write(puzzles);

Console.ReadLine();