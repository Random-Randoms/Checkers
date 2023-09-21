namespace Checkers
{
    public static class Lol
    {
        public static void Main(string[] args)
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackQueen = new(Color.Black, Type.Queen);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell attacker = new(3, 5);
            Cell defender = new(5, 7);
            Cell turn = new(6, 8);
            game.board.FillCell(attacker, blackQueen);
            game.board.FillCell(defender, whiteChecker);
            game.MakeTurn(attacker, turn);
            int a = 0;
        }
    }
}
