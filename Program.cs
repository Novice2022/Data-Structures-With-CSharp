using System;

namespace Laboratory
{
	internal class Program
	{
		static void Main(string[] args)
		{
			WriteHintMessage();

			CheckStringFunctionality();
            CheckListFunctionality();
			CheckStackFunctionality();
			CheckQueueFunctionality();
			CheckTreeFunctionality();
			CheckGraphFunctionality();
			CheckTableFunctionality();
			CheckSetFunctionality();
        }

        static void WriteHintMessage()
		{
			Console.WriteLine("Logs:");
			Console.WriteLine("\t[msg]: Simple message");
			Console.WriteLine("\t[wrn]: Warning");
			Console.WriteLine("\t[err]: Critical error");
			Console.WriteLine();
			Console.WriteLine("\t[cmp]: Comparing values");
			Console.WriteLine("\t[act]: Performing an action or logging after code execution");
			Console.WriteLine("\t[val]: Current value");
			Console.WriteLine("\t[...]: Etc.");
			Console.WriteLine("\n");
		}

		static void CheckStringFunctionality()
		{

		}

		static void CheckListFunctionality()
		{
			Console.WriteLine("Checking List functionality:");

			List<int> first = new List<int>();
			for (int i = 1; i < 11; i++)
				first.Add(i);

			Logger.Msg($"first: {first}", "val");

			List<int> second = new List<int>();

			Logger.Msg($"second: {second}\n", "val");
			Logger.Msg($"first.Equals(second): {first.Equals(second)}\n", "cmp");

			for (int i = 1; i < 11; i++)
				second.Add(i);

			Logger.Msg($"second: {second}\n", "val");
			Logger.Msg($"first.Equals(second): {first.Equals(second)}\n", "cmp");

			List<string> third = new List<string>();

			third.Add("1");
			third.Add("1");
			third.Add("3");
			third.Add("5");
			third.Add("2");
			third.Add("4");

			Logger.Msg($"third: {third}\n", "val");

            Logger.Msg($"second.Equals(third): {second.Equals(third)}\n", "cmp");

            third.Remove("0"); // -> ["1", "1", "3", "5", "2", "4"]
			Logger.Msg($"third after Remove(\"0\"): {third}", "act");

			third.Remove("1"); // -> ["3", "5", "2", "4"]
			Logger.Msg($"third after Remove(\"1\"): {third}", "act");

			third.Remove("4"); // -> ["3", "5", "2"]
			Logger.Msg($"third after Remove(\"4\"): {third}", "act");

			third.Remove("5"); // -> ["3", "2"]
			Logger.Msg($"third after Remove(\"5\"): {third}", "act");

			Console.WriteLine("\n");
		}

        static void CheckStackFunctionality()
		{
            Console.WriteLine("Checking Stack functionality:");

            Stack<int> first = new Stack<int>();
            for (int i = 1; i < 11; i++)
                first.Add(i);

            Logger.Msg($"first: {first}", "val");

            Stack<int> second = new Stack<int>();

            Logger.Msg($"second: {second}\n", "val");
            Logger.Msg($"first.Equals(second): {first.Equals(second)}\n", "cmp");

            for (int i = 1; i < 11; i++)
                second.Add(i);

            Logger.Msg($"second: {second}\n", "val");
            Logger.Msg($"first.Equals(second): {first.Equals(second)}\n", "cmp");

            Stack<string> third = new Stack<string>();

            third.Add("1");
            third.Add("1");
            third.Add("3");
            third.Add("5");
            third.Add("2");
            third.Add("4");

            Logger.Msg($"third: {third}\n", "val");

            Logger.Msg($"second.Equals(third): {second.Equals(third)}\n", "cmp");

            third.Pop();
            Logger.Msg($"third after Pop(): {third}", "act");

            third.Pop();
            Logger.Msg($"third after Pop(): {third}", "act");

            third.Pop();
            Logger.Msg($"third after Pop(): {third}", "act");

            Console.WriteLine("\n");
        }

		static void CheckQueueFunctionality()
		{

		}

		static void CheckTreeFunctionality()
		{

		}

		static void CheckGraphFunctionality()
		{

		}

		static void CheckTableFunctionality()
		{

		}

		static void CheckSetFunctionality()
		{

		}
	}
}
