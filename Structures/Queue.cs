namespace Laboratory.Structures
{
    internal class Queue<T>
    {
        class Node<T>
        {
            public T value;
            public Node<T> next;
            public Node<T> previous;

            public Node(T value)
            {
                this.value = value;
            }
        }

        Node<T> input, output;
        int size;

        public Queue()
        {
            size = 0;
        }

        public void Add(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (size == 0)
            {
                input = newNode;
                output = newNode;

                size++;

                return;
            }

            if (size == 1)
            {
                input = newNode;
                input.next = output;
                output.previous = input;
            }
            else
            {
                Node<T> currentInput = input;

                input = newNode;
                input.next = currentInput;
                currentInput.previous = input;
            }

            size++;
        }

        public T Pop()
        {
            if (size == 0)
                Logger.Err("Can\'t remove an element from the empty Queue.");

            T result = output.value;

            output = output.previous;
            output.next = null;

            size--;

            return result;
        }

        public override string ToString()
        {
            if (size == 0)
                return "Empty queue";
            else if (size == 1)
                return $"({input.value})";

            string result = "HEAD(";
            Node<T> current = input;

            for (int i = 0; i < size - 1; i++)
            {
                result += current.value.ToString();

                if (i == 0)
                    result += ")";

                result += " -> ";
                current = current.next;
            }

            return result + $"END({current.value})";
        }

        public override bool Equals(object obj)
        {
            if (GetHashCode() != obj.GetHashCode())
                return false;

            Queue<T> other = (Queue<T>)obj;

            if (size != other.size)
                return false;

            Node<T> thisCurrent = input;
            Node<T> otherCurrent = other.input;

            for (int i = 0; i < size; i++)
            {
                if (thisCurrent.value.Equals(otherCurrent.value))
                    return false;

                thisCurrent = thisCurrent.next;
                otherCurrent = otherCurrent.next;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int result = ToString().Length;

            Node<T> current = input;

            for (int i = 0; i < size; i++)
            {
                int currentHash = current.value.GetHashCode();
                result += currentHash * currentHash;
            }

            return result;
        }
    }
}
