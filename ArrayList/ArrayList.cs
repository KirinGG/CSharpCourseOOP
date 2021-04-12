using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayList
{
    public class ArrayList<T> : IList<T>
    {
        private const int DefaultCapacity = 8;

        private T[] items;
        private int modificationsNumber;

        public int ModificationsNumber
        {
            get
            {
                return modificationsNumber;
            }

            private set
            {
                if (modificationsNumber == int.MaxValue)
                {
                    modificationsNumber = 0;
                }
                else
                {
                    modificationsNumber = value;
                }
            }
        }

        public int Capacity
        {
            get
            {
                return items.Length;
            }
            set
            {
                if (value < Count)
                {
                    throw new ArgumentException($"The capacity must be equal or greater than the filled part of the array! Capacity: {value}. Filled part of the array: {Count}", nameof(value));
                }

                if (items.Length != value)
                {
                    Array.Resize(ref items, value);
                }
            }
        }

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return items[index];
            }
            set
            {
                CheckIndex(index);
                items[index] = value;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public ArrayList()
        {
            items = new T[DefaultCapacity];
            Capacity = DefaultCapacity;
        }

        public ArrayList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException($"The array capacity must be greater than or equal to zero! Capacity: {capacity}.", nameof(capacity));
            }

            items = new T[capacity];
            Capacity = capacity;
        }

        public void Add(T item)
        {
            if (Count == Capacity)
            {
                Capacity = (Capacity == 0) ? DefaultCapacity : Capacity * 2;
                Array.Resize(ref items, Capacity);
            }

            Insert(Count, item);
        }

        public void Clear()
        {
            if (items == null)
            {
                throw new InvalidOperationException("Arraylist is empty!");
            }

            Array.Clear(items, 0, Count);
            Count = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (items[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), $"The index goes beyond the boundary [0, {array.Length}] of the list. Current index value: {arrayIndex}.");
            }

            Array.Copy(items, 0, array, arrayIndex, Count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentModificationsNumber = ModificationsNumber;

            for (int i = 0; i < Count; i++)
            {
                if (currentModificationsNumber != ModificationsNumber)
                {
                    throw new InvalidOperationException("The data has been modified!");
                }

                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            if (items == null)
            {
                throw new InvalidOperationException("Arraylist is empty!");
            }

            return Array.IndexOf(items, item);
        }

        public void Insert(int index, T item)
        {
            CheckIndex(index);

            if (Count == Capacity)
            {
                Capacity = (Capacity == 0) ? DefaultCapacity : Capacity * 2;
                Array.Resize(ref items, Capacity);
            }

            Array.Copy(items, index, items, index + 1, Count - index);

            items[index] = item;
            Count++;
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            RemoveAt(index);

            return true;
        }

        public void RemoveAt(int index)
        {
            if (items == null)
            {
                throw new InvalidOperationException("Arraylist is empty!");
            }

            CheckIndex(index);

            Count--;
            Array.Copy(items, index + 1, items, index, Count - (index - 1));
            Array.Clear(items, Count, Capacity - Count);
        }

        public void trimToSize()
        {
            if (items == null || items.Length == Count)
            {
                return;
            }

            Array.Resize(ref items, Count);
        }

        public override string ToString()
        {
            return new StringBuilder("[").AppendJoin(", ", items.Take(Count).Select(i => (i == null) ? "null" : i.ToString())).Append("]").ToString();
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary (0, {Count}] of the list. Current index value: {index}.");
            }
        }
    }
}
