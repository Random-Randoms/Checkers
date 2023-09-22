using Checkers;

namespace GameTests
{
    public class PossibleMoveTurns
    {
        [Test]
        public void PossibleMoveTurns1()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure whiteChecker = new(Color.White, Type.Checker);
            Figure blackChecker = new(Color.Black, Type.Checker);
            Cell start = new(3, 5);
            Cell defender1 = new(4, 6);
            Cell defender2 = new(2, 6);
            Cell defender3 = new(5, 7);
            game.board.FillCell(start, blackChecker);
            game.board.FillCell(defender1, whiteChecker);
            game.board.FillCell(defender3 , blackChecker);
            Assert.That(game.PossibleMoveTurns(start), Is.EqualTo(new Cells() { defender2}));
        }

        [Test]
        public void PossibleMoveTurns2() 
        {
            Game game = new();
            game.turn = Color.Black;
            Figure whiteChecker = new(Color.White, Type.Checker);
            Figure blackChecker = new(Color.Black, Type.Checker);
            Cell start = new(3, 5);
            Cell defender1 = new(4, 6);
            Cell defender2 = new(2, 6);
            game.board.FillCell(start, blackChecker);
            game.board.FillCell(defender1, whiteChecker);
            game.board.FillCell(defender2, whiteChecker);
            Assert.That(game.PossibleMoveTurns(start), Is.EqualTo(new Cells()));
        }

        [Test]
        public void PossibleMoveTurns3()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure whiteChecker = new(Color.White, Type.Checker);
            Figure blackChecker = new(Color.Black, Type.Checker);
            Cell start = new(1, 3);
            Cell defender1 = new(2, 4);
            game.board.FillCell(start, blackChecker);
            game.board.FillCell(defender1, whiteChecker);
            Assert.That(game.PossibleMoveTurns(start), Is.EqualTo(new Cells()));
        }

        [Test]
        public void PossibleMoveTurns4()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure whiteChecker = new(Color.White, Type.Checker);
            Figure blackChecker = new(Color.Black, Type.Checker);
            Cell start = new(1, 3);
            Cell defender1 = new(2, 4);
            game.board.FillCell(start, blackChecker);
            Assert.That(game.PossibleMoveTurns(start), Is.EqualTo(new Cells() { defender1 }));
        }

        [Test]
        public void PossibleMoveTurns5()
        {
            Game game = new();
            Figure whiteChecker = new(Color.White, Type.Checker);
            Figure blackChecker = new(Color.Black, Type.Checker);
            Cell start = new(4, 8);
            Assert.That(game.PossibleMoveTurns(start), Is.EqualTo(new Cells()));
        }

        [Test]
        public void PossibleMoveTurns6()
        {
            Game game = new();
            Figure whiteQueen = new(Color.White, Type.Queen);
            Figure blackChecker = new(Color.Black, Type.Checker);
            Cell start = new(3, 5);
            Cell defender1 = new(5, 7);
            Cell defender2 = new(6, 8);
            game.board.FillCell(start, whiteQueen);
            game.board.FillCell(defender1, blackChecker);
            game.board.FillCell(defender2 , blackChecker);
            Cells expected = new()
            {
                new(4, 6),
                new(2, 6),
                new(1, 7),
                new(4, 4),
                new(5, 3),
                new(6, 2),
                new(7, 1),
                new(2, 4),
                new(1, 3)
            };
            Cells got = game.PossibleMoveTurns(start);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }
    }

    public class PossibleAttackTurns
    {
        [Test]
        public void PossibleAttackTurns1()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackChecker = new(Color.Black, Type.Checker);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell attacker = new(3, 5);
            Cell defender1 = new(4, 6);
            game.board.FillCell(attacker, blackChecker);
            game.board.FillCell(defender1, whiteChecker);
            Cells expected = new() {new(5, 7) };
            Cells got = game.PossibleAttackTurns(attacker);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }

        [Test]
        public void PossibleAttackTurns2()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackChecker = new(Color.Black, Type.Checker);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell attacker = new(3, 5);
            Cell defender2 = new(2, 6);
            Cell defender3 = new(1, 7);
            game.board.FillCell(attacker, blackChecker);
            game.board.FillCell(defender2, whiteChecker);
            game.board.FillCell(defender3, blackChecker);
            Cells expected = new() {};
            Cells got = game.PossibleAttackTurns(attacker);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }

        [Test]
        public void PossibleAttackTurns3()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackChecker = new(Color.Black, Type.Checker);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell attacker = new(3, 5);
            Cell defender4 = new(2, 4);
            game.board.FillCell(attacker, blackChecker);
            game.board.FillCell(defender4, blackChecker);
            Cells expected = new Cells() {};
            Cells got = game.PossibleAttackTurns(attacker);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }

        [Test]
        public void PossibleAttackTurns4()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackChecker = new(Color.Black, Type.Checker);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell attacker = new(2, 4);
            Cell defender1 = new(1, 3);
            game.board.FillCell(attacker, blackChecker);
            game.board.FillCell(defender1, whiteChecker);
            Cells expected = new() {};
            Cells got = game.PossibleAttackTurns(attacker);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }

        [Test]
        public void PossibleAttackTurns5()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackQueen = new(Color.Black, Type.Queen);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell attacker = new(3, 5);
            Cell defender1 = new(5, 7);
            game.board.FillCell(attacker, blackQueen);
            game.board.FillCell(defender1, whiteChecker);
            Cells expected = new() { new(6, 8) };
            Cells got = game.PossibleAttackTurns(attacker);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }

        [Test]
        public void PossibleAttackTurns6()
        {
            Game game = new();
            Figure blackQueen = new(Color.Black, Type.Queen);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell attacker = new(4, 6);
            Cell defender2 = new(2, 4);
            Cell defender3 = new(1, 3);
            game.board.FillCell(attacker, blackQueen);
            game.board.FillCell(defender2, whiteChecker);
            game.board.FillCell(defender3, blackQueen);
            Cells expected = new() {};
            Cells got = game.PossibleAttackTurns(attacker);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }

        [Test]
        public void PossibleAttackTurns7()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackQueen = new(Color.Black, Type.Queen);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell attacker = new(3, 5);
            Cell defender4 = new(2, 4);
            game.board.FillCell(attacker, blackQueen);
            game.board.FillCell(defender4, blackQueen);
            Cells expected = new() {};
            Cells got = game.PossibleAttackTurns(attacker);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }

        [Test]
        public void PossibleAttackTurns8()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackQueen = new(Color.Black, Type.Queen);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell attacker = new(3, 5);
            Cell defender5 = new(1, 7);
            game.board.FillCell(attacker, blackQueen);
            game.board.FillCell(defender5, whiteChecker);
            Cells expected = new() {};
            Cells got = game.PossibleAttackTurns(attacker);
            Assert.That(got.All(expected.Contains) && expected.All(got.Contains), Is.True);
        }
    }

    public class MakeTurn
    {
        [Test]
        public void MakeTurn1()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackChecker = new(Color.Black, Type.Checker);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell attacker = new(3, 5);
            Cell defender = new(4, 6);
            Cell turn = new(5, 7);
            game.board.FillCell(attacker, blackChecker);
            game.board.FillCell(defender, whiteChecker);
            game.MakeTurn(attacker, turn);
            game.EndTurn();
            Assert.Multiple(() =>
            {
                Assert.That(game.board.Occupant(attacker), Is.EqualTo(null));
                Assert.That(game.board.Occupant(defender), Is.EqualTo(null));
                Assert.That(game.board.Occupant(turn), Is.EqualTo(blackChecker));
            });
        }

        [Test]
        public void MakeTurn2()
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
            game.EndTurn();
            Assert.Multiple(() =>
            {
                Assert.That(game.board.Occupant(attacker), Is.EqualTo(null));
                Assert.That(game.board.Occupant(defender), Is.EqualTo(null));
                Assert.That(game.board.Occupant(turn), Is.EqualTo(blackQueen));
            });
        }

        [Test]
        public void MakeTurn3()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackQueen = new(Color.Black, Type.Queen);
            Cell attacker = new(3, 5);
            Cell turn = new(6, 8);
            game.board.FillCell(attacker, blackQueen);
            game.MakeTurn(attacker, turn);
            game.EndTurn();
            Assert.Multiple(() =>
            {
                Assert.That(game.board.Occupant(attacker), Is.EqualTo(null));
                Assert.That(game.board.Occupant(turn), Is.EqualTo(blackQueen));
            });
        }

        [Test] 
        public void MakeTurn4() 
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackChecker = new(Color.Black, Type.Checker);
            Cell attacker = new(3, 5);
            Cell turn = new(4, 6);
            game.board.FillCell(attacker, blackChecker);
            game.MakeTurn(attacker, turn);
            game.EndTurn();
            Assert.Multiple(() =>
            {
                Assert.That(game.board.Occupant(attacker), Is.EqualTo(null));
                Assert.That(game.board.Occupant(turn), Is.EqualTo(blackChecker));
            });
        }

        [Test] 
        public void MakeTurn5()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackChecker = new(Color.Black, Type.Checker);
            Cell attacker = new(3, 5);
            Cell turn = new(6, 6);
            game.board.FillCell(attacker, blackChecker);
            game.MakeTurn(attacker, turn);
            Assert.Multiple(() =>
            {
                Assert.That(game.board.Occupant(turn), Is.EqualTo(null));
                Assert.That(game.board.Occupant(attacker), Is.EqualTo(blackChecker));
            });
        }

        [Test] 
        public void MakeTurn6()
        {
            Game game = new();
            game.turn = Color.Black;
            Figure blackChecker = new(Color.Black, Type.Checker);
            Figure blackQueen = new(Color.Black, Type.Queen);
            Figure whiteChecker = new(Color.White, Type.Checker);
            Cell attacker = new(4, 6);
            Cell defender = new(5, 7);
            Cell turn = new(6, 8);
            game.board.FillCell(attacker, blackChecker);
            game.board.FillCell(defender, whiteChecker);
            game.MakeTurn(attacker, turn);
            Assert.Multiple(() =>
            {
                Assert.That(game.board.Occupant(attacker), Is.EqualTo(null));
                Assert.That(game.board.Occupant(defender), Is.EqualTo(null));
                Assert.That(game.board.Occupant(turn), Is.EqualTo(blackQueen));
            });
        }
    }
}
