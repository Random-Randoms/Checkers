namespace Checkers
{ 
    internal class GameManager
    {
        internal static void StartGame()
        {
            Game game = new();
            game.Start();

            Console.Clear();

            while (game.Winner() == null)
            {
                ProcessTurn(game);
            }

            Console.Clear();
            Console.Write("Game Has Ended. Winner: ");
            Console.WriteLine(game.Winner());
        }

        internal static void ProcessTurn(Game game)
        {
            Cells possibleStarts = game.PossibleTurnStarts();

            PrintTurn(game);
            Console.WriteLine(Graphics.BoardToString(game.board, 
                possibleStarts, null, BoardStyle.Fancy));
            PrintLine();

            
            Console.WriteLine("Possible start cells:");
            Console.WriteLine(Graphics.CellsToString(possibleStarts, game.board.Flipped));
            Console.WriteLine("Enter cell");

            Cell? start = ProcessCellInput(game);

            if (start == null)
                return;

            if (!possibleStarts.Contains(start.Value))
            {
                Console.Clear();
                Console.WriteLine("Incorrect Input: no turns from this cell");
                return;
            }

            Cells possibleEnds = game.PossibleTurns(start.Value);

            Console.Clear();
            PrintTurn(game);
            Console.WriteLine(Graphics.BoardToString(game.board,
                possibleEnds, start, BoardStyle.Fancy));
            Console.WriteLine("Possible turns:");
            Console.WriteLine(Graphics.CellsToString(possibleEnds, game.board.Flipped));
            Console.WriteLine("Enter turn destination");

            Cell? end = ProcessCellInput(game);

            if (end == null)
                return;

            ProcessTurnInput(start.Value, end.Value, game);

            Console.Clear();
        }

        private protected static Cell? ProcessCellInput(Game game)
        {
            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.Clear();
                Console.WriteLine("Incorrect input: null input");
                PrintLine();
                return null;
            }


            Cell? start = Graphics.StringToCell(input, game.board.Flipped);

            if (!start.HasValue)
            {
                Console.Clear();
                Console.WriteLine("Incorrect input: not a cell");
                PrintLine();
                return null;
            }

            return start.Value;
        }

        private protected static void ProcessTurnInput(Cell start, Cell end, Game game)
        {
            if (!game.PossibleTurns(start).Contains(end))
            {
                Console.Clear();
                Console.WriteLine("Incorrect input: inpossible turn");
                PrintLine();
            }

            game.MakeTurn(start, end);
        }

        private protected static void PrintTurn(Game game)
        {
            Console.WriteLine("Current turn: " + game.turn);
            PrintLine();
        }

        private protected static void PrintLine()
        {
            Console.WriteLine("_________________________");
        }
    }
}
