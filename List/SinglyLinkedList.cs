using System;
using System.Text;

namespace SinglyLinkedList
{
    public class SinglyLinkedList<T>
    {
        private ListItem<T> head;

        public int Count { get; private set; }

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
            CheckIndex(index);

            return GetItemByIndex(index).Data;
        }

        public T Set(int index, T data)
        {
            CheckIndex(index);

            var currentItem = GetItemByIndex(index);
            var oldData = currentItem.Data;
            currentItem.Data = data;

            return oldData;
        }

        public T RemoveAt(int index)
        {
            CheckIndex(index);

            if (index == 0)
            {
                var itemData = head.Data;
                RemoveFirst();
                return itemData;
            }

            Count--;

            var previousItem = GetItemByIndex(index - 1);
            var currentItem = previousItem.Next;

            previousItem.Next = currentItem.Next;
            currentItem.Next = null;

            return currentItem.Data;
        }

        public void AddFirst(T data)
        {
            head = new ListItem<T>(data, head);
            Count++;
        }

        public void Insert(int index, T data)
        {
            CheckIndex(index);

            if (index == 0)
            {
                AddFirst(data);
                return;
            }

            var previousItem = GetItemByIndex(index - 1);
            var currentItem = previousItem.Next;

            if (index == Count - 1)
            {
                currentItem.Next = new ListItem<T>(data, currentItem);
            }
            else
            {
                previousItem.Next = new ListItem<T>(data, currentItem);
            }

            Count++;
        }

        public bool Remove(T data)
        {
            if (head == null)
            {
                return false;
            }

            ListItem<T> previousItem = null;
            ListItem<T> currentItem = head;

            do
            {
                var isEquals = (currentItem.Data == null || data == null) ? (currentItem.Data == null && data == null) : (currentItem.Data.Equals(data));

                if (isEquals)
                {
                    if (currentItem == head)
                    {
                        RemoveFirst();
                        return true;
                    }

                    Count--;

                    previousItem.Next = currentItem.Next;
                    currentItem.Next = null;

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

            var deletedData = head.Data;
            head = head.Next;
            Count--;

            return deletedData;
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
            if (head == null)
            {
                return new SinglyLinkedList<T>();
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

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary [0, {Count}) of the list. Current index value: {index}.");
            }
        }

        private ListItem<T> GetItemByIndex(int index)
        {
            ListItem<T> currentItem = head;

            for (int i = 0; i < index; i++)
            {
                currentItem = currentItem.Next;
            }

            return currentItem;
        }
    }
}
