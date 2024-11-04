namespace Laboratory.Structures
{
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
}
