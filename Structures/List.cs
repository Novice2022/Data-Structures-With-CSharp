namespace Laboratory.Structures
{
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
                return $"value: {value}";
            }
        }

        public List()
        {
            length = 0;
            currentIterationElement = head;
        }

        Node<T> head, tail;
        int length;

        Node<T> currentIterationElement;
        public T Next
        {
            get
            {
                T result = currentIterationElement.value;

                if (currentIterationElement.next == null)
                    currentIterationElement = head;
                else
                    currentIterationElement = currentIterationElement.next;

                return result;
            }
        }

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
                currentIterationElement = head;
            }
            else
            {
                tail.next = new Node<T>(value);
                tail = tail.next;
            }

            length++;
        }

        public void Remove(T value, int amount = -1)
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

            while (current != null && amount != 0)
            {
                if (current.value.Equals(value))
                {
                    previous.next = current.next;
                    length--;
                    amount--;
                }

                previous = previous.next;

                if (previous == null || previous.next == null)
                    break;

                current = previous.next;
                currentIterationElement = current;
            }

            if (length == 0)
                currentIterationElement = head;
        }

        public bool Includes(T value)
        {
            Node<T> current = head;

            while (current != null)
            {
                if (value.Equals(current.value))
                    return true;

                current = current.next;
            }

            return false;
        }

        public void ForEach(System.Action<T> action)
        {
            Node<T> current = head;

            while (current != null)
            {
                action(current.value);
                current = current.next;
            }
        }

        public void ResetNext() => currentIterationElement.next = head;

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
}
