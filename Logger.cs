using System;

namespace Laboratory
{
    internal class Logger
    {
        public static void Log(string message, string suffix)
        {
            Console.WriteLine($" [{ suffix }]\t{ message }");
        }

        public static void Msg(string message, string suffix = "msg")
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($" [{ suffix }]\t{ message }");
            Console.ResetColor();
        }

        public static void Wrn(string message, string suffix = "wrn")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" [{ suffix }]\t{ message }");
            Console.ResetColor();
        }

        public static void Err(string message, string suffix = "err")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" [{ suffix }]\t{ message }");
            Console.ResetColor();
        }
    }
}
