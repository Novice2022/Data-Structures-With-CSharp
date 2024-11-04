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

            List<string> values;
            int width = 0;
            string header;

            public string Header { get { return header; } }

            public int Length
            {
                get { return values.Length; }
            }

            public int Width
            {
                get { return width; }
            }

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

        public Table(string name)
        {
            columns = new Column[0];
            this.name = name;
        }

        public Table(Table other)
        {
            columns = other.columns;
            name = other.name;
        }


        public Table AddColumn(
            ColumnsDisplayingTypes type,
            string header = null,
            int fractionalPartSizeForDoubles = 3
        )
        {
            Column[] newColumns = new Column[columns.Length + 1];

            int i = 0;

            for (; i < columns.Length; i++)
                newColumns[i] = columns[i];

            if (header == null)
                header = "Column " + newColumns.Length.ToString();

            newColumns[i] = new Column(type, header, fractionalPartSizeForDoubles);

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

        public void Print() => System.Console.Write(ToString());

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

            const string    cross = "╬",

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

                return table;
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
                            int fractionalLength = columns[col].FractionalPartWidth - (cell.Length - cell.IndexOf(","));

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
    }
}
