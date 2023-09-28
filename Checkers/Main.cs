using System.Text;

namespace Checkers
{
    public static class App
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            GameManager.StartGame();
        }
    }
}
