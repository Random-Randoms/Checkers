namespace Checkers
{
    enum BoardStyle
    {
        Default,
        Fancy
    }
    internal static class Graphics
    {
        internal static Cell? StringToCell(string str, bool flipped)
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

            if (flipped)
            {
                result.X = Board.Size + 1 - result.X;
                result.Y = Board.Size + 1 - result.Y;
            }

            return result;
        }

        internal static string? CellToString(Cell cell, bool flipped) 
        {
            if (!Board.IsCell(cell))
                return null;
            string result = "";
            if (flipped)
            {
                cell.X = Board.Size + 1 - cell.X;
                cell.Y = Board.Size + 1 - cell.Y;
            }
            result += (char)(cell.X + 'A' - 1);
            result += (char)(cell.Y + '0');
            return result;
        }

        internal static string? CellsToString(Cells cells, bool flipped)
        {
            string? result = "";

            if (cells.Count == 0) 
                return "";

            foreach (var cell in cells)
            {
                result += CellToString(cell, flipped);
                result += " ";

            }

            result = result[..^1];

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

        internal static string BoardToString(Board board, Cells choosable, Cell? chosen,
            BoardStyle style = BoardStyle.Default)
        {
            string result = "";

            bool flipped = board.Flipped;

            for (int i = Board.Size; i >= 1; i--)
            {
                if (flipped)
                    result += (char)(Board.Size + 1 - i + '0');
                else
                    result += (char)(i + '0');

                for (int j = 1;  j <= Board.Size; j++)
                {
                    Cell cell = new(j, i);
                    string figure = FigureToString(board.Occupant(cell));
                    if (style == BoardStyle.Default)
                        result += figure;
                    if (style == BoardStyle.Fancy)
                    {
                        if (cell.Equals(chosen))
                        {
                            result += "{" + figure + "}";
                            continue;
                        }
                        if (choosable.Contains(cell))
                        {
                            result += "[" + figure + "]";
                            continue;
                        }
                        result += "┊" + figure + "┊";
                    }
                        
                }

                result += "\n";
            }

            result += " ";

            for (int i = 1; i <= Board.Size; i++)
            {
                if (style == BoardStyle.Fancy)
                    result += " ";
                if (flipped)
                    result += (char)(Board.Size - i + 'A');
                else
                    result += (char)(i + 'A' - 1);
                if (style == BoardStyle.Fancy)
                    result += " ";
            }

            return result;
        }
    }
}
