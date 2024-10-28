using System;

namespace Laboratory
{
    internal class String
    {
        char[] value;
        int size;

        public int Length
        {
            get { return size; }
        }

        public String() {
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

    internal class List<T>
    {
        class Node<T>
        {
            public T value;
            public Node<T> next;

            public Node(T value)
            {
                this.value = value;
            }

            public override string ToString()
            {
                return $"value: { value }";
            }
        }

        Node<T> head, tail;
        int length = 0;

        public int Length
        {
            get
            {
                return length;
            }
        }

        public void Add(T value)
        {
            if (head == null)
            {
                head = new Node<T>(value);
                tail = head;
            }
            else
            {
                tail.next = new Node<T>(value);
                tail = tail.next;
            }

            length++;
        }

        public void Remove(T value)
        {
            if (head == null)
                Logger.Err("Can\'t remove an element from the empty List.");

            while (head.value.Equals(value))
            {
                head = head.next;
                length--;
            }

            Node<T> current = head.next;
            Node<T> previous = head;

            while (current != null)
            {
                if (current.value.Equals(value))
                {
                    previous.next = current.next;
                    length--;
                }

                previous = previous.next;

                if (previous == null || previous.next == null)
                    break;

                current = previous.next;
            }
        }

        override public string ToString()
        {
            if (head == null)
            {
                return "[]";
            }

            string result = "[";
            Node<T> current = head;

            do
            {
                result += current.value.ToString() + ", ";
                current = current.next;
            } while (current != null);

            return result + "\b\b]";
        }

        override public int GetHashCode()
        {
            if (head == null)
                return -1;

            int result = 0;

            Node<T> current = head;

            do
            {
                result += current.value.GetHashCode();
                current = current.next;
            }
            while (current != null);

            return result;
        }

        override public bool Equals(object other)
        {
            if (GetHashCode() != other.GetHashCode())
                return false;

            List<T> list;

            if (!(other is List<T>))
                return false;

            list = (List<T>)other;

            Node<T> currentThis = head;
            Node<T> currentOther = list.head;

            do
            {
                if (!currentThis.value.Equals(currentOther.value))
                {
                    return false;
                }
                currentThis = currentThis.next;
                currentOther = currentOther.next;
            }
            while (currentThis != null && currentOther != null);

            return currentThis == null && currentOther == null;
        }
    }

    internal class Stack<T>
    {
        class Node<T>
        {
            public T value;
            public Node<T> next;

            public Node() { }

            public Node(T value)
            {
                this.value = value;
            }
        }

        Node<T> availableElement;
        bool isEmpty;

        public bool IsEmpty
        {
            get { return isEmpty; }
        }

        public Stack()
        {
            isEmpty = true;
        }

        public void Add(T value)
        {
            if (isEmpty)
            {
                availableElement = new Node<T>(value);
                isEmpty = false;
            }
            else
            {
                Node<T> newElement = new Node<T>(value);
                newElement.next = availableElement;
                availableElement = newElement;
            }
        }

        // Returns available value.
        public T Get() { return availableElement.value; }

        // Returns available value and removes it.
        public T Pop()
        {
            if (isEmpty)
                Logger.Err("Can\'t remove an element from the empty Stack.");

            T result = availableElement.value;

            availableElement = availableElement.next;

            if (availableElement.value == null)
                isEmpty = true;

            return result;
        }

        public override int GetHashCode()
        {
            int result = ToString().Length;

            Node<T> current = availableElement;

            while (current != null)
            {
                int currentHash = current.value.GetHashCode();
                result += currentHash * currentHash;

                current = current.next;
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (GetHashCode() != obj.GetHashCode())
                return false;

            Stack<T> other = (Stack<T>)obj;

            if (isEmpty != other.isEmpty || (
                !availableElement.value.Equals(other.availableElement.value)
            ))
                return false;

            Node<T> thisCurrent = availableElement,
                    otherCurrent = other.availableElement;

            while (true)
            {
                if (thisCurrent == null && otherCurrent == null)
                    return true;
                else if (thisCurrent == null || otherCurrent == null)
                    return false;

                if (!thisCurrent.value.Equals(otherCurrent.value))
                    return false;

                thisCurrent = thisCurrent.next;
                otherCurrent = otherCurrent.next;
            }
        }

        public override string ToString()
        {
            if (availableElement == null)
                return "Empty stack";

            string result = "\n\tNote:\tHEAD - the reachable element.\n\tHEAD(";

            Node<T> current = availableElement;

            result += $"{current.value}) -> ";
            current = current.next;

            while (current != null)
            {
                result += $"{current.value} -> ";
                current = current.next;
            }

            return result + "\b\b\b  ";
        }
    }

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

        public T Pop() {
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
