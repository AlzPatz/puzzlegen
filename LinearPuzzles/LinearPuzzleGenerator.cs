namespace LinearPuzzles
{
    internal class LinearPuzzleGenerator : ILinearPuzzleGenerator
    {
        public List<LinearPuzzle> GenerateDistribution(int minNumberPuzzles = 480,
                                                       int maxValue = 20,
                                                       int maxCalculationResult = 120,
                                                       bool returnShuffled = true,
                                                       bool avoidDivideByOne = true)
        {
            //To distribute puzzle types equally we need the numberof results equal to 5!*4
            //This of course does not take into account two of the same operators in reverse positions are equivalent and so there 
            //will be a heavier weighting to those operator patterns that are equivalent. Think this is a minor problem in the quest
            //for a set of diverse solutions

            var rnd = new Random();

            var puzzles = new List<LinearPuzzle>();

            var numberOfPuzzles = minNumberPuzzles;

            if (numberOfPuzzles <= 0)
            {
                numberOfPuzzles = 480;
            }

            var numPuzzlesPerConfiguration = (numberOfPuzzles / 480) + 1;

            if (numberOfPuzzles % 480 != 0)
            {
                numberOfPuzzles = numPuzzlesPerConfiguration * 480;
            }
            else
            {
                numPuzzlesPerConfiguration--;
            }

            var operators = new string[] { "+", "-", "x", "/" };

            for (var floatingOperatorType = 0; floatingOperatorType < 4; floatingOperatorType++)
            {
                for (var floatingOperatorPosition = 0; floatingOperatorPosition < 5; floatingOperatorPosition++)
                {
                    for (var operatorOnePosition = 0; operatorOnePosition < 4; operatorOnePosition++)
                    {
                        for (var operatorTwoPosition = 0; operatorTwoPosition < 4; operatorTwoPosition++)
                        {
                            if (operatorTwoPosition == operatorOnePosition)
                            {
                                continue;
                            }

                            for (var operatorThreePosition = 0; operatorThreePosition < 4; operatorThreePosition++)
                            {
                                if (operatorTwoPosition == operatorOnePosition || operatorThreePosition == operatorTwoPosition || operatorThreePosition == operatorOnePosition)
                                {
                                    continue;
                                }

                                var operatorFourPosition = FindOperatorMissingPosition(operatorOnePosition, operatorTwoPosition, operatorThreePosition);

                                for (var p = 0; p < numPuzzlesPerConfiguration; p++)
                                {

                                    puzzles.Add(GeneratePuzzle(rnd,
                                                               maxValue,
                                                               maxCalculationResult,
                                                               operators,
                                                               floatingOperatorType,
                                                               floatingOperatorPosition,
                                                               operatorOnePosition,
                                                               operatorTwoPosition,
                                                               operatorThreePosition,
                                                               operatorFourPosition,
                                                               avoidDivideByOne));
                                }
                            }
                        }
                    }
                }
            }

            if (returnShuffled)
            {
                Shuffle<LinearPuzzle>(puzzles);
            }

            return puzzles;
        }

        private int FindOperatorMissingPosition(int operatorOnePosition, int operatorTwoPosition, int operatorThreePosition)
        {
            var filled = new bool[] { false, false, false, false };
            filled[operatorOnePosition] = true;
            filled[operatorTwoPosition] = true;
            filled[operatorThreePosition] = true;
            for (var i = 0; i < 4; i++)
            {
                if (!filled[i])
                {
                    return i;
                }
            }
            throw new Exception("Something wrong, should not have trouble finding the false array entry");
        }

        private LinearPuzzle GeneratePuzzle(Random rnd,
                                            int maxValue,
                                            int maxCalculationResult,
                                            string[] operators,
                                            int floatingOperatorType,
                                            int floatingOperatorPosition,
                                            int operatorOnePosition,
                                            int operatorTwoPosition,
                                            int operatorThreePosition,
                                            int operatorFourPosition,
                                            bool avoidDivideByOne)
        {
            var puzzle = new LinearPuzzle();

            var puzzleOperators = OrderOperators(operators,
                                                 floatingOperatorType,
                                                 floatingOperatorPosition,
                                                 operatorOnePosition,
                                                 operatorTwoPosition,
                                                 operatorThreePosition,
                                                 operatorFourPosition);

            var pass = false;
            while (!pass)
            {
                puzzle.N0 = rnd.Next(maxValue) + 1;

                puzzle.O0 = puzzleOperators[0];
                puzzle.N1 = rnd.Next(maxValue) + 1;

                puzzle.O1 = puzzleOperators[1];
                puzzle.N2 = rnd.Next(maxValue) + 1;

                puzzle.O2 = puzzleOperators[2];
                puzzle.N3 = rnd.Next(maxValue) + 1;

                puzzle.O3 = puzzleOperators[3];
                puzzle.N4 = rnd.Next(maxValue) + 1;

                puzzle.O4 = puzzleOperators[4];
                puzzle.N5 = rnd.Next(maxValue) + 1;

                puzzle.Result = EvaluatePuzzle(puzzle);

                pass = ValidatePuzzle(puzzle, maxCalculationResult, avoidDivideByOne);
            }

            return puzzle;
        }

        private string[] OrderOperators(string[] operators,
                                        int floatingOperatorType,
                                        int floatingOperatorPosition,
                                        int operatorOnePosition,
                                        int operatorTwoPosition,
                                        int operatorThreePosition,
                                        int operatorFourPosition)
        {
            var ops = new string[5];

            for (var i = 0; i < 5; i++)
            {
                ops[i] = "FAIL";
            }

            int floating, one, two, three, four;

            floating = floatingOperatorPosition;
            one = operatorOnePosition >= floatingOperatorPosition ? operatorOnePosition + 1 : operatorOnePosition;
            two = operatorTwoPosition >= floatingOperatorPosition ? operatorTwoPosition + 1 : operatorTwoPosition;
            three = operatorThreePosition >= floatingOperatorPosition ? operatorThreePosition + 1 : operatorThreePosition;
            four = operatorFourPosition >= floatingOperatorPosition ? operatorFourPosition + 1 : operatorFourPosition;

            ops[floating] = operators[floatingOperatorType];
            ops[one] = operators[0];
            ops[two] = operators[1];
            ops[three] = operators[2];
            ops[four] = operators[3];

            for (var i = 0; i < 5; i++)
            {
                if (ops[i] == "FAIL")
                {
                    throw new Exception("Did not set all the operator positions. Check issue with algo");
                }
            }

            return ops;
        }

        public List<LinearPuzzle> Generate(int numberOfPuzzles, int maxValue, int maxCalculationResult, bool avoidDivideByOne = true)
        {
            var puzzles = new List<LinearPuzzle>();

            var rnd = new Random();

            for (int i = 0; i < numberOfPuzzles; i++)
            {
                puzzles.Add(GeneratePuzzle(rnd, maxValue, maxCalculationResult));
            }

            return puzzles;
        }

        private LinearPuzzle GeneratePuzzle(Random rnd, int maxValue, int maxCalculationResult, bool avoidDivideByOne = true)
        {
            var puzzle = new LinearPuzzle();

            Tuple<string, int> operatorAndInteger;

            var pass = false;
            while (!pass)
            {
                puzzle.N0 = rnd.Next(maxValue) + 1;

                operatorAndInteger = GenerateOperatorAndInteger(rnd, puzzle, 0, maxValue);
                puzzle.O0 = operatorAndInteger.Item1;
                puzzle.N1 = operatorAndInteger.Item2;

                operatorAndInteger = GenerateOperatorAndInteger(rnd, puzzle, 1, maxValue);
                puzzle.O1 = operatorAndInteger.Item1;
                puzzle.N2 = operatorAndInteger.Item2;

                operatorAndInteger = GenerateOperatorAndInteger(rnd, puzzle, 2, maxValue);
                puzzle.O2 = operatorAndInteger.Item1;
                puzzle.N3 = operatorAndInteger.Item2;

                operatorAndInteger = GenerateOperatorAndInteger(rnd, puzzle, 3, maxValue);
                puzzle.O3 = operatorAndInteger.Item1;
                puzzle.N4 = operatorAndInteger.Item2;

                operatorAndInteger = GenerateOperatorAndInteger(rnd, puzzle, 4, maxValue);
                puzzle.O4 = operatorAndInteger.Item1;
                puzzle.N5 = operatorAndInteger.Item2;

                puzzle.Result = EvaluatePuzzle(puzzle);

                pass = ValidatePuzzle(puzzle, maxCalculationResult, avoidDivideByOne);
            }

            return puzzle;
        }

        private Tuple<string, int> GenerateOperatorAndInteger(Random rnd, LinearPuzzle puzzle, int step, int max)
        {
            bool hasPlus = false, hasMinus = false, hasMultiply = false, hasDivide = false;
            bool hasSecondOperator = false;

            string op = "";

            if (step > 0)
            {
                for (var s = 0; s < step; s++)
                {
                    switch (s)
                    {
                        case 0:
                            op = puzzle.O0;
                            break;
                        case 1:
                            op = puzzle.O1;
                            break;
                        case 2:
                            op = puzzle.O2;
                            break;
                        case 3:
                            op = puzzle.O3;
                            break;
                        default:
                            throw new Exception("Something wrong, step integer not in correct range");
                    }

                    switch (op)
                    {
                        case "-":
                            if (hasMinus)
                            {
                                hasSecondOperator = true;
                            }
                            else
                            {
                                hasMinus = true;
                            }
                            break;
                        case "+":
                            if (hasPlus)
                            {
                                hasSecondOperator = true;
                            }
                            else
                            {
                                hasPlus = true;
                            }
                            break;
                        case "x":
                            if (hasMultiply)
                            {
                                hasSecondOperator = true;
                            }
                            else
                            {
                                hasMultiply = true;
                            }
                            break;
                        case "/":
                            if (hasDivide)
                            {
                                hasSecondOperator = true;
                            }
                            else
                            {
                                hasDivide = true;
                            }
                            break;
                    }
                }
            }

            //Pick operator
            var pass = false;

            while (!pass)
            {
                switch (rnd.Next(4))
                {
                    case 0:
                        op = "+";
                        pass = !hasPlus || (hasPlus && !hasSecondOperator);
                        break;
                    case 1:
                        op = "-";
                        pass = !hasMinus || (hasMinus && !hasSecondOperator);
                        break;
                    case 2:
                        op = "x";
                        pass = !hasMultiply || (hasMultiply && !hasSecondOperator);
                        break;
                    case 3:
                        op = "/";
                        pass = !hasDivide || (hasDivide && !hasSecondOperator);
                        break;
                }
            }

            //Pick Number
            var num = rnd.Next(max) + 1;

            return new Tuple<string, int>(op, num);
        }

        private int EvaluatePuzzle(LinearPuzzle puzzle)
        {
            int calc = puzzle.N0;

            calc = EvaluateStep(calc, puzzle.O0, puzzle.N1);
            calc = EvaluateStep(calc, puzzle.O1, puzzle.N2);
            calc = EvaluateStep(calc, puzzle.O2, puzzle.N3);
            calc = EvaluateStep(calc, puzzle.O3, puzzle.N4);
            calc = EvaluateStep(calc, puzzle.O4, puzzle.N5);

            return calc;
        }

        private int EvaluateStep(int calc, string op, int n)
        {
            int result = -1;

            switch (op)
            {
                case "+":
                    result = calc + n;
                    break;
                case "-":
                    result = calc - n;
                    break;
                case "x":
                    result = calc * n;
                    break;
                case "/":
                    result = calc / n;
                    break;
            }

            return result;
        }

        private bool ValidatePuzzle(LinearPuzzle puzzle, int maxCalculationResult, bool avoidDivideByOne)
        {
            if (puzzle.Result < 0 || puzzle.Result > maxCalculationResult)
            {
                return false;
            }

            var integers = new int[] { puzzle.N0, puzzle.N1, puzzle.N2, puzzle.N3, puzzle.N4, puzzle.N5 };

            for (var i = 0; i < 5; i++)
            {
                for (var j = i + 1; j < 6; j++)
                {
                    if (integers[i] == integers[j])
                    {
                        return false;
                    }
                }
            }

            int calculation = 0;
            for (var step = 0; step < 6; step++)
            {
                switch (step)
                {
                    case 0:
                        calculation = puzzle.N0;
                        break;
                    case 1:
                        if (IsValidStep(calculation, puzzle.N1, puzzle.O0, maxCalculationResult, avoidDivideByOne))
                        {
                            calculation = CalculateStep(calculation, puzzle.N1, puzzle.O0);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case 2:
                        if (IsValidStep(calculation, puzzle.N2, puzzle.O1, maxCalculationResult, avoidDivideByOne))
                        {
                            calculation = CalculateStep(calculation, puzzle.N2, puzzle.O1);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case 3:
                        if (IsValidStep(calculation, puzzle.N3, puzzle.O2, maxCalculationResult, avoidDivideByOne))
                        {
                            calculation = CalculateStep(calculation, puzzle.N3, puzzle.O2);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case 4:
                        if (IsValidStep(calculation, puzzle.N4, puzzle.O3, maxCalculationResult, avoidDivideByOne))
                        {
                            calculation = CalculateStep(calculation, puzzle.N4, puzzle.O3);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case 5:
                        if (IsValidStep(calculation, puzzle.N5, puzzle.O4, maxCalculationResult, avoidDivideByOne))
                        {
                            calculation = CalculateStep(calculation, puzzle.N5, puzzle.O4);
                        }
                        else
                        {
                            return false;
                        }
                        break;

                }
            }

            return true;
        }

        private bool IsValidStep(int init, int n, string op, int maxCalculationResult, bool avoidDivideByOne)
        {
            switch (op)
            {
                case "+":
                    if (init + n > maxCalculationResult)
                    {
                        return false;
                    }
                    return true;
                case "-":
                    if (init - n <= 0)
                    {
                        return false;
                    }
                    return true;
                case "x":
                    if (init * n > maxCalculationResult)
                    {
                        return false;
                    }
                    return true;
                case "/":
                    if (init % n != 0)
                    {
                        return false;
                    }
                    if(avoidDivideByOne)
                    {
                        if (n==1)
                        {
                            return false;
                        }
                    }
                    return true;
                default:
                    throw new Exception("Operator string shouldn't exist");
            }
        }

        private int CalculateStep(int init, int n, string op)
        {
            switch (op)
            {
                case "+":
                    return init + n;
                case "-":
                    return init - n;
                case "x":
                    return init * n;
                case "/":
                    return init / n;
                default:
                    throw new Exception("Operator string shouldn't exist");
            }
        }

        private void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}