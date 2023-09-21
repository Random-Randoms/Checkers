namespace Checkers
{
    internal static class Graphics
    {
        internal static Cell? StringToCell(string str)
        {
            if (str.Length != 2)
                return null;

            Cell result = new();

            string numbers = "12345678";

            string letters = "ABCDEFGH";

            if (!numbers.Contains(str[1]))
                return null;

            if (!letters.Contains(str[0]))
                return null;

            result.Y = str[1] - '0';
            result.X = str[0] - 'A' + 1;

            return result;
        }

        internal static string? CellToString(Cell cell) 
        {
            if (!Board.IsCell(cell))
                return null;
            string result = "";
            result += (char)(cell.X + 'A' - 1);
            result += (char)(cell.Y + '0');
            return result;
        }

        internal static string FigureToString(Figure? figure)
        {
            if (figure == null)
                return ".";
            if (figure.Color == Color.Black && figure.Type == Type.Checker)
                return "b";
            if (figure.Color == Color.Black && figure.Type == Type.Queen)
                return "B";
            if (figure.Color == Color.White && figure.Type == Type.Checker)
                return "w";
            return "W";
        }

        internal static string BoardToString(Board board)
        {
            string result = "";

            for (int i = Board.size; i >= 1; i--)
            {
                result += (char)(i + '0');
                for (int j = 1;  j <= Board.size; j++)
                {
                    Cell cell = new(j, i);
                    result += FigureToString(board.Occupant(cell));
                }
                result += "\n";
            }

            result += " ";

            for (int i = 1; i <= Board.size; i++)
            {
                result += (char)(i + 'A' - 1);
            }

            return result;
        }
    }
}
