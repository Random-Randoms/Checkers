global using NUnit.Framework;
global using Cells = System.Collections.Generic.List<Cell>;

internal enum Color
{
    Black,
    White
}

internal enum Type
{
    Checker,
    Queen
}

internal record Figure(Color Color, Type Type);

internal struct Cell
{ 
    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X { set; get; }
    public int Y { set; get; }
}

namespace Checkers
{
    

    public class Board
    {
        protected int size;

        internal Dictionary<Cell, Figure?> figures = new();

        public Board(int size)
        {
            if (size % 2 != 0)
            {
                throw new ArgumentException("Size of board must be even");
            }

            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            this.size = size;

            FillWithNulls();
        }

        internal bool IsCell(Cell cell)
        {
            if (cell.X <= 0 || cell.Y <= 0) return false;
            if (cell.X > this.size || cell.Y > this.size) return false;
            return (cell.X + cell.Y) % 2 == 0;
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
            for (int i = 1; i < size; ++i)
            {
                for (int j = 0; j < size / 2; ++j)
                {
                    figures[new Cell(i, 2 * j + i % 2)] = figure;
                }
            }
        }

        private protected Cell? NearestFigureOnDiagonal(Cell start,
            int deltaX, int deltaY)
        {
            if (!IsCell(start))
                return null;
            Console.WriteLine($"start cell: {0:D}, {1:D}\n", start.X, start.Y);
            start.X += deltaX;
            start.Y += deltaY;
            while (IsCell(start))
            {
                Console.WriteLine($"current cell: {0:D}, {1:D}\n", start.X, start.Y);
                if (!IsFree(start))
                    return start;
                start.X += deltaX;
                start.Y += deltaY;

            };
            return null;
        }
    }
}
