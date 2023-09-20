namespace Checkers
{
    public static class Lol
    {
        public static void Main(string[] args)
        {
            Board board = new(8);
            Cell start = new(1, 1);
            Cell onDiag1 = new(5, 5);
            Figure figure = new(Color.White, Type.Queen);
            board.FillCell(onDiag1, figure);
            Cells expected = new() { onDiag1 };
            Cells got = board.NearestFIgureOnDiagonals(start);
        }
    }
}
