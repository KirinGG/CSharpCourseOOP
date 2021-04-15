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
            if (Count == Capacity)
            {
                Capacity = (Capacity == 0) ? DefaultCapacity : Capacity * 2;
            }

            Insert(Count, item);
        }

        public void Clear()
        {
            Array.Clear(items, 0, Count);
            Count = 0;
            modificationsCount++;
        }

        public bool Contains(T item)
        {
            if (IndexOf(item) == -1)
            {
                return false;
            }

            return true;
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

            if(array.Rank > 1)
            {
                throw new ArgumentException("The array is multidimensional!", nameof(array));
            }

            if(!(items is T[]))
            {
                throw new ArrayTypeMismatchException("The source array type cannot be automatically converted to the destination array type!");
            }

            if (items.Rank > 1)
            {
                throw new RankException("The source array is multidimensional!");
            }

            foreach(var item in items)
            {
                if(!(item is T))
                {
                    throw new InvalidCastException("At least one element in the source array cannot be cast to the destination array type!");
                }
            }

            Array.Copy(items, 0, array, arrayIndex, Count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentModificationsNumber = modificationsCount;

            for (int i = 0; i < Count; i++)
            {
                if (currentModificationsNumber != modificationsCount)
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
            for (int i = 0; i < Count; i++)
            {
                if (items[i] == null && item == null)
                {
                    return i;
                }

                if (items[i] == null || item == null)
                {
                    continue;
                }

                if (items[i].Equals(item))
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

            if (Count == Capacity)
            {
                Capacity = (Capacity == 0) ? DefaultCapacity : Capacity * 2;
            }

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
            CheckIndex(index);

            Array.Copy(items, index + 1, items, index, Count - index);
            Count--;
            Array.Clear(items, Count, 1);
            modificationsCount++;
        }

        public void TrimExpress()
        {
            if (100 - Count / items.Length * 100 > 10)
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
    }
}
