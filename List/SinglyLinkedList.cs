using System;
using System.Text;

namespace SinglyLinkedList
{
    enum ArgumentType
    {
        Index,
        Data
    }

    class SinglyLinkedList<T>
    {
        private ListItem<T> head;

        public int Count { get; private set; }

        public SinglyLinkedList()
        {

        }

        public T GetFirst()
        {
            if (head == null)
            {
                throw new InvalidOperationException("The list is empty. Unable to get the value of the first element.");
            }

            return head.Data;
        }

        public T Get(int index)
        {
            CheckArgument(index, ArgumentType.Index);
            ListItem<T> currentItem = GetItemByIndex(index);

            return currentItem.Data;
        }

        public T Set(int index, T data)
        {
            CheckArgument(index, ArgumentType.Index);
            ListItem<T> currentItem = GetItemByIndex(index);
            T result = currentItem.Data;
            currentItem.Data = data;

            return result;
        }

        public T RemoveAt(int index)
        {
            CheckArgument(index, ArgumentType.Index);

            ListItem<T> currentItem = head;
            ListItem<T> previousItem = null;

            for (int i = 1; i < index; i++)
            {
                previousItem = currentItem;
                currentItem = currentItem.Next;
            }

            previousItem.Next = currentItem.Next;
            currentItem.Next = null;
            Count--;

            return currentItem.Data;
        }

        public void AddFirst(T data)
        {
            CheckArgument(data, ArgumentType.Data);
            head = new ListItem<T>(data, head);
            Count++;
        }

        public void Insert(int index, T data)
        {
            CheckArgument(index, ArgumentType.Index);
            CheckArgument(data, ArgumentType.Data);

            ListItem<T> currentItem = head;
            ListItem<T> previousItem = null;

            for (int i = 1; i < index; i++)
            {
                previousItem = currentItem;
                currentItem = currentItem.Next;
            }

            previousItem.Next = new ListItem<T>(data, currentItem); ;
            Count++;
        }

        public bool Remove(T data)
        {
            if (head == null)
            {
                return false;
            }

            CheckArgument(data, ArgumentType.Data);

            ListItem<T> currentItem = head;
            ListItem<T> previousItem = null;
            ListItem<T> deleteItem = new ListItem<T>(data);

            do
            {
                if (currentItem.Data.Equals(deleteItem.Data))
                {
                    if (Count == 1)
                    {
                        RemoveFirst();
                        return true;
                    }

                    previousItem.Next = currentItem.Next;
                    currentItem.Next = null;

                    Count--;
                    return true;
                }

                previousItem = currentItem;
                currentItem = currentItem.Next;

            } while (currentItem != null);

            return false;
        }

        public T RemoveFirst()
        {
            if (head == null)
            {
                throw new InvalidOperationException("The list is empty.");
            }
            
            T result = head.Data;
            head = head.Next;
            Count--;

            return result;
        }

        public void Reverse()
        {
            if (Count < 2)
            {
                return;
            }

            ListItem<T> link1 = null;
            ListItem<T> link2 = null;
            ListItem<T> link3 = null;

            for (ListItem<T> p = head; p != null; p = p.Next)
            {
                if (link2 != null)
                {
                    link2.Next = link1;
                }

                link1 = link2;
                link2 = link3;
                link3 = p;
            }

            link2.Next = link1;
            link3.Next = link2;
            head = link3;
        }

        public SinglyLinkedList<T> Copy()
        {
            if(head == null)
            {
                return null;
            }

            SinglyLinkedList<T> singlyLinkedList = new SinglyLinkedList<T>();
            singlyLinkedList.head = new ListItem<T>(head.Data);
            singlyLinkedList.Count = Count;

            for (ListItem<T> source = head.Next, receiver = singlyLinkedList.head; source != null; source = source.Next, receiver = receiver.Next)
            {
                receiver.Next = new ListItem<T>(source.Data);
            }

            return singlyLinkedList;
        }

        public override string ToString()
        {
            if (head == null)
            {
                return "[]";
            }

            StringBuilder stringBuilder = new StringBuilder("[").Append(head);
            ListItem<T> currentItem = head;

            for (int i = 1; i < Count; i++)
            {
                currentItem = currentItem.Next;
                stringBuilder.Append(", ").Append(currentItem);
            }

            return stringBuilder.Append("]").ToString();
        }

        private void CheckArgument(int data, ArgumentType type)
        {
            if (type == ArgumentType.Index)
            {
                if (data < 0 || data >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(data), $"The index goes beyond the boundary [0, {Count}] of the list. Current index value: {data}.");
                }
            }
        }

        private void CheckArgument(T data, ArgumentType type)
        {
            if (type == ArgumentType.Data)
            {
                if (data == null)
                {
                    throw new ArgumentNullException(nameof(data), $"The argument cannot be null!");
                }
            }
        }
    
        private ListItem<T> GetItemByIndex(int index)
        {
            ListItem<T> currentItem = head;

            for (int i = 1; i < index; i++)
            {
                currentItem = currentItem.Next;
            }

            return currentItem;
        }
    }
}
