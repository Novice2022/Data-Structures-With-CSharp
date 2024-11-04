namespace Laboratory.Structures
{
    internal class String
    {
        char[] value;
        int size;

        public int Length
        {
            get { return size; }
        }

        public String()
        {
            size = 0;
            value = new char[size];
        }

        public String(char[] value)
        {
            this.value = value;
            size = value.Length;
        }

        public static String operator +(String left, String right)
        {
            char[] result = new char[left.size + right.size];

            int i = 0;

            for (; i < left.size; i++)
                result[i] = left.value[i];

            for (int j = 0; j < right.size; j++, i++)
                result[i] = right.value[j];

            return new String(result);
        }

        public static String operator *(String left, int amount)
        {
            char[] value = left.value;
            char[] result = new char[left.size * amount];

            int k = 0;

            for (int i = 0; i < amount; i++)
                for (int j = 0; j < value.Length; j++, k++)
                    result[k] = value[j];

            return new String(result);
        }

        public override bool Equals(object obj)
        {
            if (GetHashCode() != obj.GetHashCode())
                return false;

            return value.Equals(((String)obj).value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode() + size.GetHashCode();
        }

        public override string ToString()
        {
            if (size == 0)
                return "\"\"";

            string result = "";

            for (int i = 0; i < size; i++)
                result += value[i];

            return result;
        }
    }
}
