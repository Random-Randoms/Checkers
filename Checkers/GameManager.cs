namespace Checkers
{
    internal class GameManager
    {
        internal enum ErrorMessage
        {
            None,
            NullInput,
            NotACell,
            NoPossibleTurns,
            ImpossibleTurn
        }

        internal static Dictionary<ErrorMessage, string> errorMessages = new()
        {
            {ErrorMessage.None, "" },
            {ErrorMessage.NullInput, "Incorrect input: null input"},
            {ErrorMessage.NotACell, "Incorrect input: not a cell"},
            {ErrorMessage.NoPossibleTurns, "Incorrect input: no turns from this cell"},
            {ErrorMessage.ImpossibleTurn, "Incorrect input: impossible turn" }
        };
        internal static void StartGame()
        {
            Game game = new();
            game.Start();

            PrintTopMessage(ErrorMessage.None);

            while (game.Winner() == null)
            {
                ProcessTurn(game);
            }

            PrintWinner(game.Winner().Value);
        }

        internal static void ProcessTurn(Game game)
        {
            Cells possibleStarts = game.PossibleTurnStarts();

            PrintInfoStart(game, possibleStarts);
            AskStartCell(possibleStarts, game);

            Cell? start = ProcessTurnStartInput(game, possibleStarts);
            if (start == null)
                return;

            Cells possibleEnds = game.PossibleTurns(start.Value);

            PrintInfoEnd(game, possibleEnds, start.Value);
            AskEndCell(possibleEnds, game); 

            Cell? end = ProcessTurnEndInput(game, possibleEnds);
            if (end == null)
                return;

            game.MakeTurn(start.Value, end.Value);
        }

        private protected static Cell? ProcessCellInput(Game game)
        {
            string? input = Console.ReadLine();

            if (input == null)
            {
                PrintTopMessage(ErrorMessage.NullInput);
                return null;
            }


            Cell? start = Graphics.StringToCell(input, game.board.IsFlipped());

            if (!start.HasValue)
            {
                PrintTopMessage(ErrorMessage.NotACell);
                return null;
            }

            return start.Value;
        }

        private protected static Cell? ProcessTurnStartInput(Game game, Cells possibleStarts)
        {
            Cell? start = ProcessCellInput(game);

            if (start == null)
                return null;

            if (!possibleStarts.Contains(start.Value))
            {
                PrintTopMessage(ErrorMessage.NoPossibleTurns);
                return null;
            }

            PrintTopMessage(ErrorMessage.None);

            return start;
        }
        private protected static Cell? ProcessTurnEndInput(Game game, Cells possibleEnds)
        {
            Cell? end = ProcessCellInput(game);

            if (end == null)
                return null;

            if (!possibleEnds.Contains(end.Value))
            {
                PrintTopMessage(ErrorMessage.ImpossibleTurn);
                return null;
            }

            PrintTopMessage(ErrorMessage.None);

            return end;
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

        private protected static void PrintTopMessage(ErrorMessage msg) 
        {
            PrintTopMessage(errorMessages[msg]);
        }

        private protected static void PrintTopMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            PrintLine();
        }

        private protected static void AskStartCell(Cells cells, Game game)
        {
            Console.WriteLine("Possible turn starts:");
            Console.WriteLine(Graphics.CellsToString(cells, game.board.IsFlipped()));
            Console.WriteLine("Enter turn start");
        }

        private protected static void AskEndCell(Cells cells, Game game)
        {
            Console.WriteLine("Possible destinations");
            Console.WriteLine(Graphics.CellsToString(cells, game.board.IsFlipped()));
            Console.WriteLine("Enter turn destination");
        }

        private protected static void PrintWinner(Color winner)
        {
            Console.Clear();
            Console.Write("Game Has Ended. Winner: ");
            Console.WriteLine(winner);
        }

        private protected static void PrintInfoStart(Game game, Cells possibleStarts) 
        {
            PrintTurn(game);
            Console.WriteLine(Graphics.BoardToString(game.board,
                possibleStarts, null, BoardStyle.Fancy));
            PrintLine();
        }

        private protected static void PrintInfoEnd(Game game, Cells possibleEnds, Cell start)
        {
            PrintTurn(game);
            Console.WriteLine(Graphics.BoardToString(game.board,
                possibleEnds, start, BoardStyle.Fancy));
            PrintLine();
        }
    }
}
