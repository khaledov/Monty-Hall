using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qliro.MontyHall.Infra.Formatters
{
 
    public  class ConsoleFormatter
    {
        public static void WriteWarning(string content)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(content);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteSpecial(string content)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(content);
            Console.ForegroundColor = ConsoleColor.Gray;
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
    }
}
