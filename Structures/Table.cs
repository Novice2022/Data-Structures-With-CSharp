namespace Laboratory.Structures
{
    internal class Table
    {
        public enum ColumnsDisplayingTypes
        {
            Integer,
            Double,
            String
        }

        class Column
        {
            ColumnsDisplayingTypes type;
            int fractionalPartSize;
            int width = 0;
            string header;
            List<string> values;

            public string Header { get { return header; } }
            public int Length { get { return values.Length; } }
            public int Width { get { return width; } }
            public int FractionalPartWidth { get { return fractionalPartSize; } }
            public string Next { get { return values.Next; } }

            public ColumnsDisplayingTypes Type { get { return type; } }

            public Column(
                ColumnsDisplayingTypes type,
                string header,
                int fractionalPartSizeForDoubles = 3
            )
            {
                values = new List<string>();
                this.header = header;

                if (type == ColumnsDisplayingTypes.Double)
                    fractionalPartSize = fractionalPartSizeForDoubles;
                else
                    fractionalPartSize = 0;

                width = header.Length;

                this.type = type;
            }

            public Column Add(string value)
            {
                if (value == null)
                    value = "";

                if (type == ColumnsDisplayingTypes.Double)
                {
                    if (value == "")
                        value = "0,0";

                    while (value.IndexOf(".") > 0)
                        value = value.Replace('.', ',');

                    value = System.Math.Round(
                        double.Parse(value),
                        fractionalPartSize
                    ).ToString();
                }

                values.Add(value);

                if (value.Length > width)
                    width = value.Length;

                if (header.Length > width)
                    width = header.Length;

                return this;
            }

            public Column Remove(string value, int amount = -1)
            {
                values.Remove(value, amount);
                int newMaxWidth = header.Length;

                if (value.Length > header.Length)
                    values.ForEach(
                        element =>
                        {
                            if (element.Length > newMaxWidth)
                                newMaxWidth = element.Length;
                        }
                    );

                if (newMaxWidth < width)
                    width = newMaxWidth;

                return this;
            }

            public bool Includes(string value) => values.Includes(value);

            public override int GetHashCode() => values.GetHashCode();

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                if (GetHashCode() != obj.GetHashCode())
                    return false;

                Column other = (Column)obj;

                return values.Equals(other.values);
            }

            public override string ToString()
            {
                string result = header + " | width: " + width + " | height: " + values.Length;
                result += "\nType: ";

                switch (type)
                {
                    case ColumnsDisplayingTypes.Integer:
                        result += "Integer.";
                        break;
                    case ColumnsDisplayingTypes.String:
                        result += "String.";
                        break;
                    case ColumnsDisplayingTypes.Double:
                        result += "Double with " + fractionalPartSize + " fractional part size.";
                        break;
                }

                return result + "\n";
            }
        }


        Column[] columns;
        string name;

        int lastCreatedColumnsIndex = 1;

        public Table(string name)
        {
            columns = new Column[0];
            this.name = name;
        }

        public Table(Table other, string newName = null)
        {
            columns = new Column[other.columns.Length];

            for (int i = 0; i < other.columns.Length; i++)
                columns[i] = other.columns[i];

            if (newName == null)
                name = other.name;
            else
                name = newName;
        }


        public Table AddColumn(
            ColumnsDisplayingTypes type,
            string header = null,
            int fractionalPartSizeForDoubles = 3
        )
        {
            if (header == null)
                header = "Column " + lastCreatedColumnsIndex.ToString();

            for (int col = 0; col < columns.Length; col++)
                if (columns[col].Header == header)
                    throw new System.Exception(header + " column has already used in current table!");

            lastCreatedColumnsIndex++;
            Column[] newColumns = new Column[columns.Length + 1];

            int i = 0;
            for (; i < columns.Length; i++)
                newColumns[i] = columns[i];

            newColumns[i] = new Column(type, header, fractionalPartSizeForDoubles);
            columns = newColumns;

            return this;
        }

        public Table RemoveColumn(string columnHeader)
        {
            int removeIndex = -1;

            for (int i = 0; i < columns.Length; i++)
                if (columns[i].Header == columnHeader)
                {
                    removeIndex = i;
                    break;
                }

            if (removeIndex == -1)
                return this;

            Column[] newColumns = new Column[columns.Length - 1];

            for (int i = 0, j = 0; i < columns.Length; i++)
                if (columns[i].Header != columnHeader)
                {
                    newColumns[j] = columns[i];
                    j++;
                }

            columns = newColumns;

            return this;
        }

        public Table InsertRow(string[] row)
        {
            int i = 0;

            for (; i < row.Length && i < columns.Length; i++)
                columns[i].Add(row[i]);

            while (i < columns.Length)
            {
                columns[i].Add("");
                i++;
            }

            return this;
        }

        public Table Insert(string[][] rows)
        {
            for (int i = 0; i < rows.Length; i++)
                InsertRow(rows[i]);

            return this;
        }

        public Table Select(string[][] columnsConditions, string[] columnsInResult = null, string resultTableName = "Result")
        {
            Table result = new Table(resultTableName);

            if (columns.Length == 0)
                return result;

            for (int col = 0; col < columns.Length; col++)
                result.AddColumn(columns[col].Type, columns[col].Header, columns[col].FractionalPartWidth);


            for (int row = 0; row < columns[0].Length; row++)
            {
                bool conditionSatisfied = true;
                string[] insertingRow = new string[columns.Length];

                for (int col = 0; col < columns.Length; col++)
                    insertingRow[col] = columns[col].Next;

                if (columnsConditions != null)
                    for (int conditionIndex = 0; conditionIndex < columnsConditions.Length; conditionIndex++)
                    {
                        if (columnsConditions[conditionIndex].Length != 2)
                            continue;

                        for (int col = 0; col < columns.Length; col++)
                            if (columns[col].Header == columnsConditions[conditionIndex][0])
                            {
                                string condition = columnsConditions[conditionIndex][1];
                                string compareOperation;

                                if (!(condition.StartsWith("=") || condition.StartsWith(">") || condition.StartsWith("<") || condition.StartsWith("!")))
                                    continue;
                                else if (condition.StartsWith(">=") || condition.StartsWith("<=") || condition.StartsWith("!="))
                                    compareOperation = condition.Substring(0, 2);
                                else
                                    compareOperation = condition.Substring(0, 1);

                                if (columns[col].Type == ColumnsDisplayingTypes.Integer || columns[col].Type == ColumnsDisplayingTypes.Double)
                                {
                                    double insertingElement, conditionElement;

                                    if (double.TryParse(insertingRow[col], out insertingElement) && double.TryParse(condition.Substring(1), out conditionElement))
                                        if (!this.conditionSatisfied(compareOperation, insertingElement, conditionElement))
                                        {
                                            conditionSatisfied = false;
                                            break;
                                        }
                                }
                            }
                    }

                if (conditionSatisfied)
                    result.InsertRow(insertingRow);
            }

            for (int i = 0; i < result.columns.Length; i++)
            {
                bool saveColumn = false;

                if (columnsInResult != null)
                {
                    for (int j = 0; j < columnsInResult.Length; j++)
                        if (result.columns[i].Header == columnsInResult[j])
                        {
                            saveColumn = true;
                            break;
                        }

                    if (!saveColumn)
                        result.RemoveColumn(result.columns[i].Header);
                }
            }

            return result;
        }

        public void Print() => System.Console.Write(ToString());


        public string[][] ToNestedArraysAsColumns()
        {
            string[][] result = new string[columns.Length][];

            for (int i = 0; i < columns.Length; i++)
            {
                result[i] = new string[columns[i].Length];

                for (int j = 0; j < columns[i].Length; j++)
                    result[i][j] = columns[i].Next;
            }

            return result;
        }

        public string[][] ToNestedArraysAsRows()
        {
            int maxColumnLength = 0;

            for (int col = 0; col < columns.Length; col++)
                if (columns[col].Length > maxColumnLength)
                    maxColumnLength = columns[col].Length;

            string[][] result = new string[maxColumnLength][];

            for (int row = 0; row < maxColumnLength; row++)
                result[row] = new string[columns.Length];

            for (int col = 0; col < columns.Length; col++)
                for (int row = 0; row < columns[col].Length; row++)
                    result[row][col] = columns[col].Next;

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (GetHashCode() != obj.GetHashCode())
                return false;

            Table other = (Table)obj;

            if (columns.Length != other.columns.Length)
                return false;

            for (int i = 0; i < columns.Length; i++)
                if (!columns[i].Equals(other.columns[i]))
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            int result = 0;

            for (int i = 0; i < columns.Length; i++)
                result += columns[i].GetHashCode();

            return result / columns.Length + name.GetHashCode();
        }

        public override string ToString()
        {
            int maxHeigth = 0;

            for (int i = 0; i < columns.Length; i++)
                if (columns[i].Length > maxHeigth)
                    maxHeigth = columns[i].Length;

            int[] widths = new int[columns.Length];

            for (int i = 0; i < columns.Length; i++)
                widths[i] = columns[i].Width + 2;

            const string cross = "╬",

                            verticalTop = "╦",
                            verticalMiddle = "║",
                            verticalBottom = "╩",

                            horizontalLeft = "╠",
                            horizontalMiddle = "═",
                            horizontalRight = "╣",

                            cornerTopLeft = "╔",
                            cornerTopRight = "╗",
                            cornerBottomLeft = "╚",
                            cornerBottomRight = "╝";

            //const string cross = "+",

            //                verticalTop = ".",
            //                verticalMiddle = "|",
            //                verticalBottom = "'",

            //                horizontalLeft = ":",
            //                horizontalMiddle = "-",
            //                horizontalRight = ":",

            //                cornerTopLeft = "+",
            //                cornerTopRight = "+",
            //                cornerBottomLeft = "+",
            //                cornerBottomRight = "+";

            string table = "\n" + name + "\n";

            table += cornerTopLeft;
            for (int i = 0; i < widths.Length; i++)
            {
                for (int _ = 0; _ < widths[i]; _++)
                    table += horizontalMiddle;

                table += verticalTop;
            }
            table += "\b" + cornerTopRight + "\n" + verticalMiddle;
            
            for (int i = 0; i < columns.Length; i++)
            {
                table += " " + columns[i].Header;

                for (int _ = 0; _ < columns[i].Width - columns[i].Header.Length; _++)
                    table += " ";

                table += " " + verticalMiddle;
            }

            if (maxHeigth == 0)
            {
                table += "\n" + cornerBottomLeft;
                for (int i = 0; i < widths.Length; i++)
                {
                    for (int _ = 0; _ < widths[i]; _++)
                        table += horizontalMiddle;

                    table += verticalBottom;
                }
                table += "\b" + cornerBottomRight + "\n";

                return table + "\n";
            }

            table += "\n" + horizontalLeft;
            for (int i = 0; i < widths.Length; i++)
            {
                for (int _ = 0; _ < widths[i]; _++)
                    table += horizontalMiddle;

                table += cross;
            }
            table += "\b" + horizontalRight + "\n";


            for (int i = 0; i < maxHeigth; i++)
            {
                table += verticalMiddle + " ";

                for (int col = 0; col < columns.Length; col++)
                {
                    string cell = columns[col].Next;
                    int cellLength = cell.Length;

                    if (columns[col].Type == ColumnsDisplayingTypes.String)
                    {
                        for (int _ = 0; _ < columns[col].Width - cellLength; _++)
                            cell += " ";
                    }
                    else
                    {
                        if (columns[col].Type == ColumnsDisplayingTypes.Double)
                        {
                            int fractionalLength = columns[col].FractionalPartWidth - (cell.Length - cell.IndexOf(",") - 1);

                            if (cell.IndexOf(",") == -1)
                            {
                                cell += ",";
                                fractionalLength = columns[col].FractionalPartWidth;
                            }

                            if (columns[col].FractionalPartWidth < fractionalLength)
                                fractionalLength = columns[col].FractionalPartWidth - 1;

                            for (int _ = 0; _ < fractionalLength; _++)
                                cell += "0";
                        }

                        cellLength = cell.Length;

                        for (int _ = 0; _ < columns[col].Width - cellLength; _++)
                            cell = " " + cell;
                    }
                    
                    table += cell + " " + verticalMiddle + " ";
                }

                table += "\n";
            }


            table += cornerBottomLeft;
            for (int i = 0; i < widths.Length; i++)
            {
                for (int _ = 0; _ < widths[i]; _++)
                    table += horizontalMiddle;

                table += verticalBottom;
            }
            table += "\b" + cornerBottomRight + "\n\n";

            return table;
        }

        bool conditionSatisfied(string compareOperation, double left, double right)
        {
            if (compareOperation == ">=")
                return left >= right;

            if (compareOperation == "<=")
                return left <= right;

            if (compareOperation == "!=")
                return left != right;

            if (compareOperation == ">")
                return left > right;

            if (compareOperation == "<")
                return left < right;

            return left == right;
        }
    }
}
