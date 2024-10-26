using System;

namespace Laboratory
{
    internal class String
    {
        public String(char[] chars)
        {

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
                return;

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
                throw new Exception("Can\'t remove an element from the empty stack.");

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
