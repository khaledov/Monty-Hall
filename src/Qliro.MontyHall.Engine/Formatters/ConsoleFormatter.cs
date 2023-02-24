using Qliro.MontyHall.Engine.Models;

namespace Qliro.MontyHall.Engine.Formatters
{

    public class ConsoleFormatter
    {
        const char _block = '■';
        const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
        const string _twirl = "-\\|/";

        public static void WriteWarning(string content)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(content);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteSpecial(string content)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(content +"\t\t\t");
            Console.ForegroundColor = ConsoleColor.Gray;
        }



        public static void WriteResult(Statistics statistics)
        {
            Console.WriteLine();

            WriteInfo("Strategy", statistics.Strategy);
            WriteInfo("You have chosen to run engine", statistics.GamesCount.ToString() + " times.");
            WriteInfo("You wins", statistics.WinsCount.ToString());
            WriteInfo("Win rate", statistics.Accuracy.ToString() + " %");

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteInfo(string header, string? value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{header} : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(value + "\n");
        }
       
        public static void WriteFailed(TimeSpan? time = null)
        {
            var elapsed = time.HasValue ? " - " + time.Value.TotalSeconds.ToString("F4") + "s" : "";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("FAILED" + elapsed);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

        public static void WriteException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\t**************** FAILED *******************");
            Console.WriteLine("Exception Type :\t" + ex.GetType());
            Console.WriteLine();
            Console.WriteLine("Exception Message :\t" + ex.Message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteExceptionShort(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\t**************** FAILED *******************");
            Console.WriteLine();
            Console.WriteLine("(" + ex.GetType() + ") :\t" + ex.Message);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }



        public static void WriteProgressBar(int percent, bool update = false)
        {
            if (update)
                Console.Write(_back);
            Console.Write("[");
            var p = (int)((percent / 10f) + .5f);
            for (var i = 0; i < 10; ++i)
            {
                if (i >= p)
                    Console.Write(' ');
                else
                    Console.Write(_block);
            }
            Console.Write("] {0,3:##0}%", percent);
        }
        public static void WriteProgress(int progress, bool update = false)
        {
            if (update)
                Console.Write("\b");
            Console.Write(_twirl[progress % _twirl.Length]);
        }
    }
}
