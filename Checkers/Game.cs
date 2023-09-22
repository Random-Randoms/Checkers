namespace Checkers
{
    internal class Game
    {
        internal Board board;

        internal Color turn;

        internal Cell? movedCell;

        public Game()
        {
            board = new();
            turn = Color.White;
            movedCell = null;
        }

        internal Cells PossibleMoveTurns(Cell cell)
        {
            Figure? figure = board.Occupant(cell);

            Cells result = new();

            if (figure == null)
                return result;

            if (figure.Color != turn)
                return result;

            if (movedCell.HasValue)
                if (!movedCell.Value.Equals(cell))
                    return result;

            if (HasAttackTurns())
                return result;

            if (figure.Type == Type.Checker)
            {
                foreach (Cell turn in board.FrontAdjacents(cell)) 
                {
                    if (board.IsFree(turn))
                        result.Add(turn);
                }
            }

            if (figure.Type == Type.Queen)
            {
                result = board.DiagonalsUntilFigure(cell);
            }

            return result;
        }

        internal Cells PossibleAttackTurns(Cell cell) 
        {
            Figure? figure= board.Occupant(cell);

            Cells result = new();

            if (movedCell.HasValue) 
                if (!movedCell.Value.Equals(cell))
                    return result;

            if (figure == null) 
                return result;

            if (figure.Color != turn)
                return result;

            Cells possibleTurnes;

            if (figure.Type == Type.Checker) 
            {
                possibleTurnes = board.Adjacents(cell);
            } else
            {
                possibleTurnes = board.NearestFIgureOnDiagonals(cell);
            }

            foreach (Cell turn in possibleTurnes)
                result.AddRange(IsCorrectTurn(cell, turn, figure));

            return result;
        }

        internal Cells PossibleTurns(Cell cell)
        {
            Cells result = PossibleAttackTurns(cell);

            if (result.Count > 0)
                return result;

            if (movedCell != null)
                return result;

            return PossibleMoveTurns(cell);
        }

        internal bool HasAttackTurns()
        {
            foreach(Cell cell in board.FiguresOfColor(turn)) 
            {
                if (PossibleAttackTurns(cell).Count > 0) return true;
            }

            return false;
        }

        internal void MakeTurn(Cell begin, Cell end)
        {
            if (PossibleMoveTurns(begin).Contains(end))
            {
                Figure figure = (Figure)board.Occupant(begin);
                board.FillCell(end, figure);
                board.ClearCell(begin);
                movedCell = end;
                TryPutQueen(end);
                if (PossibleTurns(end).Count == 0)
                    EndTurn();
                return;
            }

            if (PossibleAttackTurns(begin).Contains(end))
            {
                movedCell = end;
                Figure figure = (Figure)board.Occupant(begin);
                Cell defender = (Cell)board.DefenderCell(begin, end);
                board.FillCell(end, figure);
                board.ClearCell(defender);
                board.ClearCell(begin);
                if (TryPutQueen(end))
                    EndTurn();
                if(PossibleTurns(end).Count == 0)
                    EndTurn();
                return;
            }
        }

        internal void EndTurn()
        {
            movedCell = null;

            turn = EnemyColor();

            board.Flip();
        }

        internal void Start()
        {
            board.FillDefault();
        }

        internal bool IsVictory()
        {
            return board.FiguresOfColor(EnemyColor()).Count == 0;
        }

        private protected Cells IsCorrectTurn(Cell attacker, Cell defender, Figure figure) 
        {
            if (board.Occupant(defender) == null)
                return new();

            if (board.Occupant(defender).Color == figure.Color)
                return new();

            Cell? landingCell = board.AttackLandingCell(attacker, defender);

            if (landingCell == null)
                return new();

            if (!board.IsFree(landingCell.Value))
                return new();

            return new() {landingCell.Value};
        }

        private protected bool TryPutQueen(Cell cell)
        {
            Figure? figure = board.Occupant(cell);

            if (figure == null)
                return false;

            if (figure.Type != Type.Checker) 
                return false;

            if (cell.Y == Board.Size)
            {
                board.FillCell(cell, new Figure(figure.Color, Type.Queen));
                return true;
            }

            return false;
        }

        private protected Color EnemyColor()
        {
            if (turn == Color.Black)
                return Color.White;
            else
                return Color.Black;
        }
    }
}
