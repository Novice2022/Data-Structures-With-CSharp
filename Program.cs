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
            Console.WriteLine("Checking String functionality:");

            String first = new String();
            Logger.Msg($"first: {first}\n", "val");

            String second = new String();
            Logger.Msg($"second: {second}\n", "val");
            Logger.Msg($"first.Equals(second): {first.Equals(second)}\n", "cmp");

            char[] secondContent = { 'H', 'e', 'l', 'l', 'o' };
            second = new String(secondContent);

            Logger.Msg($"second: {second}\n", "val");
            Logger.Msg($"first.Equals(second): {first.Equals(second)}\n", "cmp");

            String third = new String(new char[] { ',', ' ', 'w', 'o', 'r', 'l', 'd', '!' });
            Logger.Msg($"third: {third}\n", "val");

            Logger.Msg($"second.Equals(third): {second.Equals(third)}\n", "cmp");

            first = second + third;
            Logger.Msg($"first as second + third:\n\t{first}\n", "act");

            String fourth = new String(new char[] { 'b', 'r' }) * 5;
            Logger.Msg($"fourth: {fourth}\n", "val");

            Console.WriteLine("\n");
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
            Console.WriteLine("Checking Queue functionality:");

            Queue<int> first = new Queue<int>();
            for (int i = 1; i < 11; i++)
			{
                first.Add(i);
                Logger.Msg($"first after Add(): {first}", "act");
            }

            Logger.Msg($"first: {first}", "val");

            Queue<int> second = new Queue<int>();

            Logger.Msg($"second: {second}\n", "val");
            Logger.Msg($"first.Equals(second): {first.Equals(second)}\n", "cmp");

            for (int i = 1; i < 11; i++)
                second.Add(i);

            Logger.Msg($"second: {second}\n", "val");
            Logger.Msg($"first.Equals(second): {first.Equals(second)}\n", "cmp");

            Queue<string> third = new Queue<string>();

            third.Add("1");
            third.Add("1");
            third.Add("3");
            third.Add("5");
            third.Add("2");
            third.Add("4");

            Logger.Msg($"third: {third}\n", "val");

            Logger.Msg($"second.Equals(third): {second.Equals(third)}\n", "cmp");

            third.Pop();
            Logger.Msg($"third after Pop():\n\t{third}", "act");

            third.Pop();
            Logger.Msg($"third after Pop():\n\t{third}", "act");

            third.Pop();
            Logger.Msg($"third after Pop():\n\t{third}", "act");

            Console.WriteLine("\n");
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
