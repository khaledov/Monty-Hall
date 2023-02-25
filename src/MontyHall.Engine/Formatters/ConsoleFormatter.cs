using MontyHall.Engine.Models;

namespace MontyHall.Engine.Formatters
{

    public class ConsoleFormatter
    {
        #region Attributes
        const char _block = '■';
        const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
        const string _twirl = "-\\|/"; 
        #endregion

        #region Public methods
        /// <summary>
        /// Writes the special.
        /// </summary>
        /// <param name="content">The content.</param>
        public static void WriteSpecial(string content)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(content + "\t\t\t");
            Console.ForegroundColor = ConsoleColor.Gray;
        }



        /// <summary>
        /// Writes the result.
        /// </summary>
        /// <param name="statistics">The statistics.</param>
        public static void WriteResult(Statistics statistics)
        {
            Console.WriteLine();

            WriteInfo("Strategy", statistics.Strategy);
            WriteInfo("You have chosen to run engine", statistics.GamesCount.ToString() + " times.");
            WriteInfo("You wins", statistics.WinsCount.ToString());
            WriteInfo("Win rate", statistics.Accuracy.ToString("0.00") + " %");

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Writes the information.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="value">The value.</param>
        public static void WriteInfo(string header, string? value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{header} : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(value + "\n");
        }



        /// <summary>
        /// Writes the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void WriteException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\t**************** FAILED *******************");
            Console.WriteLine("Exception Type :\t" + ex.GetType());
            Console.WriteLine();
            Console.WriteLine("Exception Message :\t" + ex.Message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }


        /// <summary>
        /// Writes the progress bar.
        /// </summary>
        /// <param name="percent">The percent.</param>
        /// <param name="update">if set to <c>true</c> [update].</param>
        public static void WriteProgressBar(long percent, bool update = false)
        {
            if (update)
                Console.Write(_back);
            Console.Write("[");
            var p = (long)((percent / 10f) + .5f);
            for (var i = 0; i < 10; ++i)
            {
                if (i >= p)
                    Console.Write(' ');
                else
                    Console.Write(_block);
            }
            Console.Write("] {0,3:##0}%", percent);
        }
        /// <summary>
        /// Writes the progress.
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <param name="update">if set to <c>true</c> [update].</param>
        public static void WriteProgress(int progress, bool update = false)
        {
            if (update)
                Console.Write("\b");
            Console.Write(_twirl[progress % _twirl.Length]);
        }
        #endregion
    }
}
