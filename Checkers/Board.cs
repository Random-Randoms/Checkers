global using NUnit.Framework;
global using Cells = System.Collections.Generic.List<Cell>;

/*
 * Represents color of a figure or a player
 */
internal enum Color
{
    Black,
    White
}

/*
 * Represents type of a figure: Queen or a Checker
 */
internal enum Type
{
    Checker,
    Queen
}

/*
 * Represents single checker (or queen)
 */
internal record Figure(Color Color, Type Type);


/*
 * Represents a single board cell
 */
internal struct Cell
{ 
    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X { set; get; }
    public int Y { set; get; }

    public string? toString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ")";
    }
}

namespace Checkers
{
    
    /*
     * Contiains 32 possible cells of checkers board
     * and figures staying in each cell. Methods provides geometrical
     * information about board and figures
     */
    internal sealed class Board
    {
        private bool flipped = false;

        internal static int Size = 8;

        internal Dictionary<Cell, Figure?> figures = new();

        public Board()
        {
            FillWithNulls();
        }

        public Board(Board board)
        {
            figures = board.figures.ToDictionary(entry => entry.Key,
                                               entry => entry.Value);
            flipped = board.IsFlipped();
        }

        internal static bool IsCell(Cell cell)
        {
            if (cell.X <= 0 || cell.Y <= 0) return false;
            if (cell.X > Board.Size || cell.Y > Board.Size) return false;
            return (cell.X + cell.Y) % 2 == 0;
        }

        internal bool IsFlipped()
        {
            return flipped;
        }

        internal Figure? Occupant(Cell cell)
        {
            if (!IsCell(cell))
            {
                return null;
            }

            return figures[cell];
        }

        internal bool IsFree(Cell cell)
        {
            if (!IsCell(cell))
            {
                return false; 
            }
            return Occupant(cell) == null;
        }

        internal bool MatchesColor(Cell cell, Color color)
        {
            if (!IsCell(cell))
                return false;

            Figure? occupant = Occupant(cell);

            if (occupant == null)
                return false;

            if (occupant.Color == color) return true;

            return false;
        }

        internal Cells FiguresOfColor(Color color)
        {
            Cells result = new();

            foreach (var cell in figures)
            {
                if (cell.Value == null)
                    continue;

                if (cell.Value.Color == color)
                    result.Add(cell.Key);
            }

            return result;
        }

        internal Cells Adjacents(Cell cell)
        {
            if (!IsCell(cell))
                return new Cells();

            Cells result = new();
            Cells possibleAdjacents = new() {
            new Cell(cell.X + 1, cell.Y + 1),
            new Cell(cell.X + 1, cell.Y - 1),
            new Cell(cell.X - 1, cell.Y + 1),
            new Cell(cell.X - 1, cell.Y - 1),
            };

            foreach(Cell possibleAdjacent in possibleAdjacents)
            {
                if (IsCell(possibleAdjacent))
                { 
                    result.Add(possibleAdjacent);
                }
            }

            return result;
        }

        internal Cells FrontAdjacents(Cell cell)
        {
            if (!IsCell(cell))
                return new Cells();

            Cells result = new();
            Cells possibleAdjacents = new() {
            new Cell(cell.X + 1, cell.Y + 1),
            new Cell(cell.X - 1, cell.Y + 1),
            };

            foreach (Cell possibleAdjacent in possibleAdjacents)
            {
                if (IsCell(possibleAdjacent))
                {
                    result.Add(possibleAdjacent);
                }
            }

            return result;
        }

        internal Cells DiagonalsUntilFigure(Cell cell)
        {
            Cells result = new();

            result.AddRange(DiagonalUntilFigure(cell, 1, 1));
            result.AddRange(DiagonalUntilFigure(cell, 1, -1));
            result.AddRange(DiagonalUntilFigure(cell, -1, 1));
            result.AddRange(DiagonalUntilFigure(cell, -1, -1));


            return result;
        }

        internal bool OnLine(Cell first, Cell second)
        {
            if (!IsCell(second) || !IsCell(first)) return false;

            if (first.X + first.Y == second.X + second.Y) return true;

            if (first.X - first.Y == second.X - second.Y) return true;

            return false;
        }

        internal Cells NearestFIgureOnDiagonals(Cell cell)
        {
            if(!IsCell(cell))
                return new Cells();

            Cells result = new();

            Cell? possibleNearest = NearestFigureOnDiagonal(cell, 1, 1);
            if (possibleNearest.HasValue)
                result.Add(possibleNearest.Value);
            possibleNearest = NearestFigureOnDiagonal(cell, 1, -1);
            if (possibleNearest.HasValue)
                result.Add(possibleNearest.Value);
            possibleNearest = NearestFigureOnDiagonal(cell, -1, 1);
            if (possibleNearest.HasValue)
                result.Add(possibleNearest.Value);
            possibleNearest = NearestFigureOnDiagonal(cell, -1, -1);
            if (possibleNearest.HasValue)
                result.Add(possibleNearest.Value);

            return result;
        }

        internal Cell? AttackLandingCell(Cell attacker, Cell defender)
        {
            if (!IsCell(attacker) || !IsCell(defender))
                return null;

            if (!OnLine(attacker, defender))
                return null;

            Cell result = defender;

            if (attacker.X < defender.X)
                result.X++;
            else result.X--;

            if (attacker.Y < defender.Y)
                result.Y++;
            else result.Y--;

            if (IsCell(result))
                return result;

            return null;
        }

        internal Cell? DefenderCell(Cell attacker, Cell landing) 
        {
            if (!IsCell(attacker) || !IsCell(landing))
                return null;

            if (!OnLine(attacker, landing))
                return null;

            Cell result = landing;

            if (attacker.X < landing.X)
                result.X--;
            else result.X++;

            if (attacker.Y < landing.Y)
                result.Y--;
            else result.Y++;

            if (IsCell(result))
                return result;

            return null;
        }
        internal void ClearCell(Cell cell)
        {
            FillCell(cell, null);
        }

        internal void FillCell(Cell cell, Figure? figure)
        {
            if (!IsCell(cell))
                return;
            figures[cell] = figure;
        }

        internal void FillWithNulls()
        {
            FillWithSame(null);
        }

        internal void FillWithSame(Figure? figure)
        {
            for (int i = 1; i <= Size; ++i)
            {
                for (int j = 0; j < Size / 2; ++j)
                {
                    Cell cell = new(i, 2 * j + (i + 1) % 2 + 1);
                    figures[cell] = figure;
                }
            }
        }

        internal void FillDefault()
        {
            int threeFilledLanes = 3;

            for (int i = 1; i <= Size; ++i)
            {
                for (int j = 0; j < Size / 2; ++j)
                {
                    Cell cell = new(i, 2 * j + (i + 1) % 2 + 1);
                    if (cell.Y <= threeFilledLanes)
                        figures[cell] = new Figure(Color.White, Type.Checker);
                    if (cell.Y >= Size - threeFilledLanes + 1)
                        figures[cell] = new Figure(Color.Black, Type.Checker);
                }
            }
        }

        internal void Flip()
        {
            for (int i = 1; i <= Size / 2; ++i)
            {
                for (int j = 0; j < Size / 2; ++j)
                {
                    Cell cell1 = new(i, 2 * j + (i + 1) % 2 + 1);
                    Cell cell2 = new(Size + 1 - cell1.X, Size + 1 - cell1.Y);
                    (figures[cell2], figures[cell1]) = (figures[cell1], figures[cell2]);
                }
            }

            flipped = !flipped;
        }

        private Cell? NearestFigureOnDiagonal(Cell start,
            int deltaX, int deltaY)
        {
            if (!IsCell(start))
                return null;
            start.X += deltaX;
            start.Y += deltaY;
            while (IsCell(start))
            {
                if (!IsFree(start))
                    return start;
                start.X += deltaX;
                start.Y += deltaY;

            };
            return null;
        }

        private Cells DiagonalUntilFigure(Cell cell, int deltaX, int deltaY)
        {
            Cells result = new();
            
            cell.X += deltaX;
            cell.Y += deltaY;

            while (IsCell(cell))
            {
                if (!IsFree(cell))
                    break;

                result.Add(cell);

                cell.X += deltaX;
                cell.Y += deltaY;
            }

            return result;
        }
    }
}
