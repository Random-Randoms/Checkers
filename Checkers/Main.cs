namespace Checkers
{
    public static class Lol
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();

            string? input;

            while (!game.IsVictory())
            {
                Console.Clear();
                Console.WriteLine(Graphics.BoardToString(game.board));
                Console.WriteLine("Enter cell");

                input = Console.ReadLine();

                if (input == null)
                    continue;

                Cell? start = Graphics.StringToCell(input, game.board.Flipped);

                if(start == null)
                    continue;

                Console.WriteLine("Enter destination");

                input = Console.ReadLine();

                if (input == null)
                    continue;

                Cell? end = Graphics.StringToCell(input, game.board.Flipped);

                if (end == null)
                    continue;

                if (!game.PossibleTurns(start.Value).Contains(end.Value))
                    continue;

                game.MakeTurn(start.Value, end.Value);
            }
        }
    }
}
