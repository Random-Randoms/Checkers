﻿namespace Checkers
{
    /*
     * Represents current state of a game
     * Contains current board, current turn, cell that has been
     * moved if player is currrently making a coplex turn
     * Provides possible moves information, winner of a game, current
     * board
     * Implements making turns
     */
    internal sealed class Game
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

        public Game(Game game) 
        {
            board = new(game.board);
            turn = game.turn;
            movedCell = game.movedCell;
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
        internal Cells PossibleTurnStarts() 
        {
            Cells result = new();

            foreach(Cell cell in board.FiguresOfColor(turn))
            {
                if (PossibleTurns(cell).Count > 0)
                    result.Add(cell);
            }

            return result;
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
                {
                    EndTurn();
                    return;
                }
                    
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

        internal Color? Winner()
        {
            if (board.FiguresOfColor(Color.White).Count == 0)
                return Color.Black;
            if (board.FiguresOfColor(Color.Black).Count == 0)
                return Color.White;
            return null;
        }

        internal Color EnemyColor()
        {
            if (turn == Color.Black)
                return Color.White;
            else
                return Color.Black;
        }

        private Cells IsCorrectTurn(Cell attacker, Cell defender, Figure figure) 
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

        private bool TryPutQueen(Cell cell)
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
    }
}
