using Laboratory.Structures;

namespace Laboratory
{
    internal class Lab1
    {
        public static void Run()
        {
            WriteHintMessage();

            //CheckStringFunctionality();
            //CheckListFunctionality();
            //CheckStackFunctionality();
            //CheckQueueFunctionality();
            //CheckTreeFunctionality();
            //CheckGraphFunctionality();
            CheckTableFunctionality();
            //CheckSetFunctionality();
        }

        static void WriteHintMessage()
        {
            System.Console.WriteLine("Logs:");
            System.Console.WriteLine("\t[msg]: Simple message");
            System.Console.WriteLine("\t[wrn]: Warning");
            System.Console.WriteLine("\t[err]: Critical error");
            System.Console.WriteLine();
            System.Console.WriteLine("\t[cmp]: Comparing values");
            System.Console.WriteLine("\t[act]: Performing an action or logging after code execution");
            System.Console.WriteLine("\t[val]: Current value");
            System.Console.WriteLine("\t[...]: Etc.");
            System.Console.WriteLine("\n");
        }


        static void CheckStringFunctionality()
        {
            System.Console.WriteLine("Checking String functionality:");

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

            System.Console.WriteLine("\n");
        }

        static void CheckListFunctionality()
        {
            System.Console.WriteLine("Checking List functionality:");

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

            System.Console.WriteLine("\n");
        }

        static void CheckStackFunctionality()
        {
            System.Console.WriteLine("Checking Stack functionality:");

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

            System.Console.WriteLine("\n");
        }

        static void CheckQueueFunctionality()
        {
            System.Console.WriteLine("Checking Queue functionality:");

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

            System.Console.WriteLine("\n");
        }

        static void CheckTreeFunctionality()
        {

        }

        static void CheckGraphFunctionality()
        {

        }

        static void CheckTableFunctionality()
        {
            System.Console.WriteLine("Checking Table functionality:");

            Table table = new Table("First table")
                .AddColumn(
                    Table.ColumnsDisplayingTypes.String
                ).AddColumn(
                    Table.ColumnsDisplayingTypes.Integer,
                    "Integers"
                ).AddColumn(
                    Table.ColumnsDisplayingTypes.Double
                ).AddColumn(
                    Table.ColumnsDisplayingTypes.Double,
                    "Doubles with short fractional part",
                    1
                );

            table.Print();

            string[][] data = new string[][]
            {
                new string[] { "Lorem ipsum", "1", null, "21.36" },
                new string[] { null, "123", "12,54321", null },
                new string[] { "Hello, World!", (-2).ToString(), 43.5.ToString() }
            };

            table.Insert(data);
            table.Print();

            Table table2 = new Table(table, "First table copy");
            table2.RemoveColumn("Column 3");

            table2.Print();
            table.Print();

            string[][] nestedArraysRepresentation = table.ToNestedArraysAsRows();

            for (int row = 0; row < nestedArraysRepresentation.Length; row++)
            {
                for (int col = 0; col < nestedArraysRepresentation[row].Length; col++)
                {
                    string element = nestedArraysRepresentation[row][col];

                    if (element != null)
                        System.Console.Write("\"" + nestedArraysRepresentation[row][col] + "\"");
                    
                    System.Console.Write(", ");
                }

                System.Console.WriteLine("\b\b \n");
            }

            System.Console.WriteLine();
            Logger.Msg($"Table (with values at `Integer` column > 1):", "act");
            Table result = table.Select(
                new string[1][]
                {
                    new string[2] { "Integers", ">1" }
                }
            );

            result.Print();

            System.Console.WriteLine();
            Logger.Msg($"Table (with values at `Doubles with short fractional part` column < 1):", "act");
            result = table.Select(
                new string[1][]
                {
                    new string[2] { "Doubles with short fractional part", "<1" }
                }
            );

            result.Print();
        }

        static void CheckSetFunctionality()
        {

        }
    }
}
