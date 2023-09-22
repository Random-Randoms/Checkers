using Checkers;

namespace GraphicsTests
{
        public class CellToString
        {
            [Test] 
            public void CellToString1()
            {
                Assert.That(Graphics.CellToString(new Cell(1, 3), false), Is.EqualTo("A3"));
            }

            [Test]
            public void CellToString2()
            {
                Assert.That(Graphics.CellToString(new Cell(5, 7), false), Is.EqualTo("E7"));
            }

            [Test]
            public void CellToString3()
            {
                Assert.That(Graphics.CellToString(new Cell(-1, 7), false), Is.EqualTo(null));
            }

            [Test]
            public void CellToString4()
            {
                Assert.That(Graphics.CellToString(new Cell(6, 8), false), Is.EqualTo("F8"));
            }
        }

        public class StringToCell
        {
            [Test]
            public void StringToCell1()
            {
                Assert.That(Graphics.StringToCell("D6", false), Is.EqualTo(new Cell(4, 6)));
            }

            [Test]
            public void StringToCell2()
            {
                Assert.That(Graphics.StringToCell("G5", false), Is.EqualTo(new Cell(7, 5)));
            }

            [Test]
            public void StringToCell3()
            {
                Assert.That(Graphics.StringToCell("cipsmgu", false), Is.Null);
            }

            [Test]
            public void StringToCell4()
            {
                Assert.That(Graphics.StringToCell("B2", true), Is.EqualTo(new Cell(7, 7)));
            }
        }

        public class FigureToChar
        {
            [Test]
            public void FigureToChar1()
            {
                Assert.That(Graphics.FigureToString(null), Is.EqualTo("."));
            }

            [Test]
            public void FigureToChar2()
            {
                Assert.That(Graphics.FigureToString(new(Color.Black, Type.Checker)), 
                    Is.EqualTo("b"));
            }

            [Test]
            public void FigureToChar3()
            {
                Assert.That(Graphics.FigureToString(new(Color.Black, Type.Queen)),
                    Is.EqualTo("B"));
            }

            [Test]
            public void FigureToChar4()
            {
                Assert.That(Graphics.FigureToString(new(Color.White, Type.Checker)),
                    Is.EqualTo("w"));
            }

            [Test]
            public void FigureToChar5()
            {
                Assert.That(Graphics.FigureToString(new(Color.White, Type.Queen)),
                    Is.EqualTo("W"));
            }
        }
        
        public class BoardToString
        {
            [Test]
            public void BoardToString1()
            {
                Board board = new();
                String expected = 
                    "8........\n" +
                    "7........\n" +
                    "6........\n" +
                    "5........\n" +
                    "4........\n" +
                    "3........\n" +
                    "2........\n" +
                    "1........\n" +
                    " ABCDEFGH";
                Assert.That(Graphics.BoardToString(board), Is.EqualTo(expected));
            }

            [Test]
            public void BoardToString2()
            {
                Board board = new();
                Figure whiteChecker = new(Color.White, Type.Checker);
                Figure blackChecker = new(Color.Black, Type.Checker);
                Figure whiteQueen = new(Color.White, Type.Queen);
                Figure blackQueen = new(Color.Black, Type.Queen);
                board.FillCell(new Cell(1, 1), whiteQueen);
                board.FillCell(new Cell(3, 5), whiteChecker);
                board.FillCell(new Cell(2, 8), blackQueen);
                board.FillCell(new Cell(6, 6), blackChecker);
                board.FillCell(new Cell(5, 5), whiteQueen);
                String expected =
                    "8.B......\n" +
                    "7........\n" +
                    "6.....b..\n" +
                    "5..w.W...\n" +
                    "4........\n" +
                    "3........\n" +
                    "2........\n" +
                    "1W.......\n" +
                    " ABCDEFGH";

                Assert.That(Graphics.BoardToString(board), Is.EqualTo(expected));
            }

            [Test]
            public void BoardToString3()
            {
                Board board = new();
                board.FillDefault();
                String expected =
                    "8.b.b.b.b\n" +
                    "7b.b.b.b.\n" +
                    "6.b.b.b.b\n" +
                    "5........\n" +
                    "4........\n" +
                    "3w.w.w.w.\n" +
                    "2.w.w.w.w\n" +
                    "1w.w.w.w.\n" +
                    " ABCDEFGH";

                Assert.That(Graphics.BoardToString(board), Is.EqualTo(expected));
            }

            [Test] 
            public void BoardToString4()
            {
                Board board = new();
                board.FillDefault();
                board.Flip();
                String expected =
                    "1.w.w.w.w\n" +
                    "2w.w.w.w.\n" +
                    "3.w.w.w.w\n" +
                    "4........\n" +
                    "5........\n" +
                    "6b.b.b.b.\n" +
                    "7.b.b.b.b\n" +
                    "8b.b.b.b.\n" +
                    " HGFEDCBA";

                Assert.That(Graphics.BoardToString(board), Is.EqualTo(expected));
            }
        }
}
