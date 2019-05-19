using System;

namespace Tetris
{
#if WINDOWS || LINUX
    /// The main class.
    public static class Program
    {
        /// The main entry point for the application.
        [STAThread]
        static void Main()
        {
            using (var spel = new Spel1())
                spel.Run();
        }
    }
#endif
}
