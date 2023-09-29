using System.Text;

namespace Checkers
{
    /*
     * Entry point class
     */
    public static class App
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            GameManager.StartGame();
        }
    }
}
