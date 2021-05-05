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
        private int modificationsCount;

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

        public ArrayList() : this(DefaultCapacity) { }

        public ArrayList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException($"The array capacity must be greater than or equal to zero! Capacity: {capacity}.", nameof(capacity));
            }

            items = new T[capacity];
        }

        public void Add(T item)
        {
            Insert(Count, item);
        }

        public void Clear()
        {
            if (Count == 0)
            {
                return;
            }

            Array.Clear(items, 0, Count);
            Count = 0;
            modificationsCount++;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), $"The index goes beyond the boundary [0, {array.Length}) of the list. Current index value: {arrayIndex}.");
            }

            if (Count > array.Length - arrayIndex)
            {
                throw new ArgumentException("The number of elements in the source collection ICollection<T> is greater than the available space from the position specified by the value of the arrayIndex parameter to the end of the destination array array.");
            }

            Array.Copy(items, 0, array, arrayIndex, Count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentModificationsCount = modificationsCount;

            for (var i = 0; i < Count; i++)
            {
                if (currentModificationsCount != modificationsCount)
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
            for (var i = 0; i < Count; i++)
            {
                if (Equals(items[i], item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary [0, {Count}] of the list. Current index value: {index}.");
            }

            IncreaseCapacity();

            Array.Copy(items, index, items, index + 1, Count - index);

            items[index] = item;
            Count++;
            modificationsCount++;
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
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary [0, {Count}] of the list. Current index value: {index}.");
            }

            if (Count != Capacity)
            {
                Array.Copy(items, index + 1, items, index, Count - index);
            }

            Count--;
            items[Count] = default;
            modificationsCount++;
        }

        public void TrimExcess()
        {
            if (Count < items.Length * 0.9)
            {
                Capacity = Count;
            }
        }

        public override string ToString()
        {
            return new StringBuilder("[")
                       .AppendJoin(", ", items
                                         .Take(Count)
                                         .Select(e => (e == null) ? "null" : e.ToString()))
                       .Append("]")
                       .ToString();
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index goes beyond the boundary [0, {Count}) of the list. Current index value: {index}.");
            }
        }

        private void IncreaseCapacity()
        {
            if (Count == Capacity)
            {
                Capacity = (Capacity == 0) ? DefaultCapacity : Capacity * 2;
            }
        }
    }
}
