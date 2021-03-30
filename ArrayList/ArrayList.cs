using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayList
{
    class ArrayList<T> : IList<T> where T : IComparable<T>
    {
        private T[] items;
        private int capacity;
        private const int defaultCapacity = 8;

        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                if (value <= Count)
                {
                    throw new ArgumentException("The capacity must be greater than the filled part of the array!", nameof(capacity));
                }

                capacity = value;
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

        public int Count { get; set; }

        public bool IsReadOnly { get; set; }

        public ArrayList()
        {
            items = new T[defaultCapacity];
            Capacity = defaultCapacity;
        }

        public ArrayList(int capacity)
        {
            items = new T[capacity];
            Capacity = capacity;
        }

        public void Add(T item)
        {
            CheckData(item);

            if (Count == Capacity)
            {
                Capacity *= 2;
                Array.Resize(ref items, Capacity);
            }

            items[Count] = item;
            Count++;
        }

        public void Clear()
        {
            Array.Clear(items, 0, Count);
        }

        public bool Contains(T item)
        {
            CheckData(item);

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
            Array.Copy(items, array, Count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return items[i];
            }
        }

        public int IndexOf(T item)
        {
            /*for (int i = 0; i < Count; i++)
            {
                if (items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;*/
            CheckData(item);

            return Array.IndexOf(items, item);
        }

        public void Insert(int index, T item)
        {
            CheckIndex(index);
            CheckData(item);

            if (Count == Capacity)
            {
                Capacity *= 2;
                Array.Resize(ref items, Capacity);
            }

            for (int i = Count; i > index; i--)
            {
                items[i] = items[i - 1];
            }

            items[index] = item;
        }

        public bool Remove(T item)
        {
            CheckData(item);

            var index = IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            Count--;

            if (Count == 0)
            {
                Array.Clear(items, 0, 1);
                return true;
            }

            for (int i = index; i < Count; i++)
            {
                items[i] = items[i + 1];
            }

            Array.Clear(items, Count, 1);

            return true;
        }

        public void RemoveAt(int index)
        {
            CheckIndex(index);

            Count--;

            if (Count == 0)
            {
                Array.Clear(items, 0, Capacity);
                return;
            }

            for (int i = index; i < Count; i++)
            {
                items[i] = items[i + 1];
            }

            Array.Clear(items, Count, Capacity);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return new StringBuilder("[").AppendJoin(", ", items).Append("]").ToString();
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary [0, {Count}] of the list. Current index value: {index}.");
            }
        }

        private void CheckData(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), $"The argument cannot be null!");
            }
        }
    }
}
