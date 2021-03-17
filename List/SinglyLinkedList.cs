using System;
using System.Collections.Generic;
using System.Text;

namespace SinglyLinkedList
{
    class SinglyLinkedList<T>
    {
        private ListItem<T> head;

        public int Count { get; private set; }

        public SinglyLinkedList()
        {
            head = null;
            Count = 0;
        }

        public T GetFirstItem()
        {
            if (head == null)
            {
                throw new Exception("The list is empty. Unable to get the value of the first element.");
            }

            return head.Data;
        }

        public T GetItem(int index)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary [0, {Count}] of the list. Current index value: {index}.");
            }

            ListItem<T> currentItem = head;

            for (int i = 1; i < index; i++)
            {
                currentItem = currentItem.Next;
            }

            return currentItem.Data;
        }

        public T SetItem(int index, T data)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary [0, {Count}] of the list. Current index value: {index}.");
            }

            ListItem<T> currentItem = head;
            ListItem<T> previousItem = null;

            for (int i = 1; i < index; i++)
            {
                previousItem = currentItem;
                currentItem = currentItem.Next;
            }

            ListItem<T> newItem = new ListItem<T>(data);
            newItem.Next = currentItem.Next;
            previousItem.Next = newItem;
            currentItem.Next = null;

            return currentItem.Data;
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary [0, {Count}] of the list. Current index value: {index}.");
            }

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

        public void Add(T data)
        {
            ListItem<T> listItem = new ListItem<T>(data);
            listItem.Next = head;
            head = listItem;
            Count++;
        }

        public void Insert(int index, T data)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary [0, {Count}] of the list. Current index value: {index}.");
            }

            ListItem<T> currentItem = head;
            ListItem<T> previousItem = null;

            for (int i = 1; i < index; i++)
            {
                previousItem = currentItem;
                currentItem = currentItem.Next;
            }

            ListItem<T> listItem = new ListItem<T>(data);
            previousItem.Next = listItem;
            listItem.Next = currentItem;
            Count++;
        }

        public bool Remove(T data)
        {
            if (head == null)
            {
                throw new Exception("The list is empty.");
            }

            ListItem<T> currentItem = head;
            ListItem<T> previousItem = null;
            ListItem<T> deleteItem = new ListItem<T>(data);

            do
            {
                if (currentItem.Equals(deleteItem))
                {
                    if (Count == 1)
                    {
                        head = null;
                    }
                    else
                    {
                        previousItem.Next = currentItem.Next;
                        currentItem.Next = null;
                    }

                    Count--;
                    return true;
                }

                previousItem = currentItem;
                currentItem = currentItem.Next;

            } while (currentItem != null);

            return false;
        }

        public T RemoveFirstItem()
        {
            if (head == null)
            {
                throw new Exception("The list is empty.");
            }

            head = head.Next;
            Count--;

            return head.Data;
        }

        public void Reverse()
        {
            if (Count == 1)
            {
                return;
            }

            ListItem<T>[] buffer = new ListItem<T>[3];

            for (ListItem<T> p = head; p != null; p = p.Next)
            {
                if (buffer[1] != null)
                {
                    buffer[1].Next = buffer[0];
                }

                buffer[0] = buffer[1];
                buffer[1] = buffer[2];
                buffer[2] = p;
            }

            buffer[1].Next = buffer[0];
            buffer[2].Next = buffer[1];
            head = buffer[2];
        }

        public static SinglyLinkedList<T> Copy(SinglyLinkedList<T> sourse)
        {
            SinglyLinkedList<T> singlyLinkedList = new SinglyLinkedList<T>();

            for (ListItem<T> p = sourse.head; p != null; p = p.Next)
            {
                singlyLinkedList.Add(p.Data);
            }

            return singlyLinkedList;
        }

        public override string ToString()
        {
            StringBuilder stringBilder = new StringBuilder($"({head}");
            ListItem<T> currentItem = head;

            for (int i = 1; i < Count; i++)
            {
                currentItem = currentItem.Next;
                stringBilder.Append($", {currentItem}");
            }

            return stringBilder.Append(")").ToString();
        }
    }
}
