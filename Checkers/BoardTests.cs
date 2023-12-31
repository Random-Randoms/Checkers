using Checkers;

namespace BoardTests
{
    public class IsCell
    {

        [Test]
        public void IsCellTest1()
        {
            Board board = new();
            Assert.That(Board.IsCell(new Cell(1, 2)), Is.EqualTo(false));
        }
        [Test]
        public void IsCellTest2()
        {
            Board board = new();
            Assert.That(Board.IsCell(new Cell(-1, 5)), Is.EqualTo(false));
        }
        [Test]
        public void IsCellTest3()
        {
            Board board = new();
            Assert.That(Board.IsCell(new Cell(3, 9)), Is.EqualTo(false));
        }
        [Test]
        public void IsCellTest4()
        {
            Board board = new();
            Assert.That(Board.IsCell(new Cell(3, 7)), Is.EqualTo(true));
        }
    }

    public class Occupant
    {
        [Test]
        public void Occupant1()
        {
            Board board = new();
            board.FillWithSame(new Figure(Color.Black, Type.Checker));
            Assert.That(board.Occupant(new Cell(1, 1)),
                Is.EqualTo(new Figure(Color.Black, Type.Checker)));
        }

        [Test]
        public void Occupant2()
        {
            Board board = new();
            board.FillWithNulls();
            Assert.That(board.Occupant(new Cell(1, 1)), Is.EqualTo(null));
        }

        [Test]
        public void Occupant3()
        {
            Board board = new();
            Assert.That(board.Occupant(new  Cell(1, 100)), Is.EqualTo(null));
        }
    }

    public class IsFree
    {
        [Test]
        public void IsFree1()
        {
            Board board = new();
            board.FillWithNulls();
            board.FillCell(new Cell(3, 5), new Figure(Color.Black, Type.Checker));
            Assert.That(board.IsFree(new Cell(1, 1)), Is.True);
        }

        [Test]
        public void IsFree2()
        {
            Board board = new();
            board.FillWithNulls();
            board.FillCell(new Cell(3, 5), new Figure(Color.Black, Type.Checker));
            Assert.That(board.IsFree(new Cell(3, 5)), Is.False);
        }
    }

    public class MatchesColor
    {
        [Test]
        public void MatchesColor1()
        {
            Board board = new();
            Assert.That(board.MatchesColor(new Cell(3, 5), Color.Black), Is.False);
        }

        [Test]
        public void MatchesColor2()
        {
            Board board = new();
            Assert.That(board.MatchesColor(new Cell(-3, 5), Color.Black), Is.False);
        }

        [Test]
        public void MatchesColor3()
        {
            Board board = new();
            Cell cell = new(3, 5);
            board.FillCell(cell, new Figure(Color.White, Type.Checker));
            Assert.That(board.MatchesColor(cell, Color.White), Is.True);
        }

        [Test]
        public void MatchesColor4()
        {
            Board board = new();
            Cell cell = new(3, 5);
            board.FillCell(cell, new Figure(Color.White, Type.Checker));
            Assert.That(board.MatchesColor(cell, Color.Black), Is.False);
        }
    }

    public class FiguresOfColor
    {
        [Test]
        public void FiguresOfColor1()
        {
            Board board = new();
            Cell cell1 = new(3, 5);
            Cell cell2 = new(3, 7);
            Cell cell3 = new(2, 4);
            Cell cell4 = new(6, 4);
            Cell cell5 = new(7, 1);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Figure blackChecker = new(Color.Black, Type.Checker);
            Figure whiteQueen = new(Color.White, Type.Queen);
            Figure blackQueen = new(Color.Black, Type.Queen);
            board.FillCell(cell1, whiteQueen);
            board.FillCell(cell2, blackQueen);
            board.FillCell(cell3, blackChecker);
            board.FillCell(cell4, whiteChecker);
            board.FillCell(cell5, blackQueen);
            Assert.That(board.FiguresOfColor(Color.Black).Count, Is.EqualTo(3));
        }
    }

    public class ClearCell
    {
        [Test]
        public void ClearCell1()
        {
            Board board = new();
            board.FillWithSame(new Figure(Color.Black, Type.Checker));
            board.ClearCell(new Cell(1, 1));
            Assert.That(board.Occupant(new Cell(1, 1)), Is.EqualTo(null));
        }

        [Test]
        public void ClearCell2()
        {
            Board board = new(  );
            board.FillWithSame(new Figure(Color.Black, Type.Checker));
            board.ClearCell(new Cell(1, 1));
            Assert.That(board.Occupant(new Cell(1, 3)), Is.Not.EqualTo(null));
        }
    }

    public class FillCell
    {
        [Test]
        public void FillCell1()
        {
            Board board = new();
            Cell cell = new(1, 1);
            Figure figure1 = new(Color.Black, Type.Checker);
            Figure figure2 = new(Color.Black, Type.Checker);
            board.FillWithSame(figure1);
            board.FillCell(cell, figure2);
            Assert.That(board.Occupant(cell), Is.EqualTo(figure2));
        }

        [Test]
        public void FillCell2()
        {
            Board board = new();
            Cell cell1 = new(1, 1);
            Cell cell2 = new(3, 7);
            Figure figure1 = new(Color.Black, Type.Checker);
            Figure figure2 = new(Color.Black, Type.Checker);
            board.FillWithSame(figure1);
            board.FillCell(cell1, figure2);
            Assert.That(board.Occupant(cell2), Is.EqualTo(figure1));
        }
    }

    public class Adjacents
    {
        [Test]
        public void Adjacents1()
        {
            Board board = new();
            Cell cell = new(1, 1);
            Cells adj = board.Adjacents(cell);
            Assert.That(adj, Is.EqualTo(new Cells() {new Cell(2, 2) }));
        }

        [Test] public void Adjacents2()
        {
            Board board = new();
            Cell cell = new(1, 3);
            Cells adj = board.Adjacents(cell);
            Cells targ = new Cells() { new Cell(2, 2), new Cell(2, 4) };
            Assert.That(adj.All(targ.Contains) && targ.All(adj.Contains), Is.True);
        }

        [Test] public void Adjacents3()
        {
            Board board = new();
            Cell cell = new(2, 2);
            Cells adj = board.Adjacents(cell);
            Cells targ = new Cells()
            {
                new Cell(1, 1),
                new Cell(1, 3),
                new Cell(3, 1),
                new Cell(3, 3)
            };
            Assert.That(adj.All(targ.Contains) && targ.All(adj.Contains), Is.True);
        }

        [Test] public void Adjacents4()
        {
            Board board = new();
            Cell cell = new(0, 4);
            Cells adj = board.Adjacents(cell);
            Assert.That(adj, Is.Empty);
        }
    }

    public class FrontAdjacents
    {
        [Test]
        public void FrontAdjacents1() 
        {
            Board board = new();
            Cell cell = new(1, 3);
            Cells adj = board.FrontAdjacents(cell);
            Assert.That(adj, Is.EqualTo(new Cells() { new Cell(2, 4) }));
        }

        [Test]
        public void FrontAdjacents2()
        {
            Board board = new();
            Cell cell = new(4, 2);
            Cells adj = board.FrontAdjacents(cell);
            Cells targ = new() { new Cell(3, 3), new Cell(5, 3) };
            Assert.That(adj.All(targ.Contains) && targ.All(adj.Contains), Is.True);
        }

        [Test]
        public void FrontAdjacents3()
        {
            Board board = new();
            Cell cell = new(0, 4);
            Cells adj = board.FrontAdjacents(cell);
            Assert.That(adj, Is.Empty);
        }

        [Test]
        public void FrontAdjacents4()
        {
            Board board = new();
            Cell cell = new(2, 8);
            Cells adj = board.FrontAdjacents(cell);
            Assert.That(adj, Is.Empty);
        }
    }

    public class DiagonalsUntilFigure
    {
        [Test]
        public void DiagonalsUntilFigure1()
        {
            Board board = new();
            Figure blackQueen = new(Color.Black, Type.Queen);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell cell = new(3, 5);
            Cell obstacle1 = new(4, 6);
            Cell obstacle2 = new(1, 7);
            board.FillCell(cell, blackQueen);
            board.FillCell(obstacle1, whiteChecker);
            board.FillCell(obstacle2, whiteChecker);
            Cells expected = new()
            {
                new(2, 6),
                new(2, 4),
                new(1, 3),
                new(4, 4),
                new(5, 3),
                new(6, 2),
                new(7, 1)
            };
            Cells got = board.DiagonalsUntilFigure(cell);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }
    }

    public class OnLine
    {
        [Test]
        public void OnLine1()
        {
            Board board = new();
            Cell cell1 = new(4, 2);
            Cell cell2 = new(7, 5);
            Assert.That(board.OnLine(cell1, cell2), Is.True);
        }

        [Test]
        public void OnLine2()
        {
            Board board = new();
            Cell cell1 = new(6, 2);
            Cell cell2 = new(3, 5);
            Assert.That(board.OnLine(cell1, cell2), Is.True);
        }

        [Test]
        public void OnLine3()
        {
            Board board = new();
            Cell cell1 = new(2, 2);
            Cell cell2 = new(8, 4);
            Assert.That(board.OnLine(cell1, cell2), Is.False);
        }

        [Test]
        public void OnLine4()
        {
            Board board = new();
            Cell cell1 = new(-1, 3);
            Cell cell2 = new(1, 1);
            Assert.That(board.OnLine(cell1, cell2), Is.False);
        }
    }
    
    public class NearestFigureOnDiagonals
    {
        [Test]
        public void NearestFigureOnDiagonals1() 
        {
            Board board = new();
            Cell start = new(1, 1);
            Cell onDiag1 = new(5, 5);
            Figure figure = new(Color.White, Type.Queen);
            board.FillCell(onDiag1, figure);
            Cells expected = new() { onDiag1};
            Cells got =  board.NearestFIgureOnDiagonals(start);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }

        [Test]
        public void NearestFigureOnDiagonals2()
        {
            Board board = new();
            Cell start = new(3, 5);
            Cell onDiag1 = new(4, 6);
            Cell onDiag2 = new(1, 7);
            Cell onDiag3 = new(2, 8);
            Figure figure = new(Color.White, Type.Queen);
            board.FillCell(onDiag1, figure);
            board.FillCell(onDiag2, figure);
            board.FillCell(onDiag3, figure);
            Cells expected = new() { onDiag1, onDiag2 };
            Cells got = board.NearestFIgureOnDiagonals(start);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }

        [Test]
        public void NearestFigureOnDiagonals3()
        {
            Board board = new();
            Cell start = new(-1, 5);
            Assert.That(board.NearestFIgureOnDiagonals(start), Is.Empty);
        }
    }

    public class AttackLandingCell
    {
        [Test]
        public void AttackLandingCell1() 
        {
            Board board = new();
            Cell attacker = new(1, 3);
            Cell defender = new(3, 5);
            Cell landing = new(4, 6);
            Assert.That(board.AttackLandingCell(attacker, defender), Is.EqualTo(landing));
        }

        [Test]
        public void AttackLandingCell2()
        {
            Board board = new();
            Cell attacker = new(1, 3);
            Cell defender = new(6, 8);
            Assert.That(board.AttackLandingCell(attacker, defender), Is.Null);
        }

        [Test]
        public void AttackLandingCell3()
        {
            Board board = new();
            Cell attacker = new(-1, 3);
            Cell defender = new(4, 8);
            Assert.That(board.AttackLandingCell(attacker, defender), Is.Null);
        }

        [Test]
        public void AttackLandingCell4()
        {
            Board board = new();
            Cell attacker = new(1, 3);
            Cell defender = new(5, 9);
            Assert.That(board.AttackLandingCell(attacker, defender), Is.Null);

        }

        [Test]
        public void AttackLandingCell5()
        {
            Board board = new();
            Cell attacker = new(1, 3);
            Cell defender = new(3, 7);
            Assert.That(board.AttackLandingCell(attacker, defender), Is.Null);
        }
    }

    public class DefenderCell
    {
        [Test]
        public void DefenderCell1() 
        {
            Board board = new();
            Cell attacker = new(1, 3);
            Cell landing = new(5, 7);
            Cell defender = new(4, 6);
            Assert.That(board.DefenderCell(attacker, landing), Is.EqualTo(defender));
        }

        [Test]
        public void DefenderCell2()
        {
            Board board = new();
            Cell attacker = new(-1, 3);
            Cell landing = new(6, 8);
            Assert.That(board.DefenderCell(attacker, landing), Is.Null);
        }

        [Test]
        public void DefenderCell3()
        {
            Board board = new();
            Cell attacker = new(1, 3);
            Cell landing = new(7, 9);
            Assert.That(board.DefenderCell(attacker, landing), Is.Null);
        }

        [Test]
        public void DefenderCell4()
        {
            Board board = new();
            Cell attacker = new(1, 3);
            Cell landing = new(3, 7);
            Assert.That(board.DefenderCell(attacker, landing), Is.Null);
        }
    }
}