using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashTables
{
    class HashTable<T> : ICollection<T>
    {
        private const int DefaultArrayLength = 10;

        private List<T>[] lists;
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
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public HashTable(int arrayLength = DefaultArrayLength)
        {
            if (arrayLength < 1)
            {
                throw new ArgumentException($"The array length must be greater than 0. Array length: {arrayLength}.", nameof(arrayLength));
            }

            lists = new List<T>[arrayLength];
        }

        public void Add(T item)
        {
            var index = GetIndex(item);

            if (lists[index] == null)
            {
                lists[index] = new List<T>();
            }

            lists[index].Add(item);
            Count++;
            ModificationsNumber++;
        }

        public void Clear()
        {
            Array.Clear(lists, 0, lists.Length);
            ModificationsNumber++;
            Count = 0;
        }

        public bool Contains(T item)
        {
            var index = GetIndex(item);

            if (lists[index] == null)
            {
                return false;
            }

            return lists[index].Contains(item);
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

            if (Count > (array.Length - arrayIndex))
            {
                throw new ArgumentException("The number of items in the source collection HashTable more available space from the position specified by the value of the index parameter to the end of the array destination array.");
            }

            var j = arrayIndex;

            for (var i = 0; i < lists.Length; i++)
            {
                if (lists[i] == null)
                {
                    continue;
                }

                foreach (var item in lists[i])
                {
                    array[j] = item;
                    j++;
                }
            }
        }

        public bool Remove(T item)
        {
            var index = GetIndex(item);

            if (lists[index] == null)
            {
                return false;
            }

            if (lists[index].Remove(item))
            {
                Count--;
                ModificationsNumber++;
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentModificationsNumber = ModificationsNumber;

            foreach (var list in lists)
            {
                if (currentModificationsNumber != ModificationsNumber)
                {
                    throw new InvalidOperationException("The data has been modified!");
                }

                if (list == null)
                {
                    continue;
                }

                foreach (T item in list)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            var items = lists
                .Select(x => (x == null || x.Count == 0) ? "null" : string.Join(", ", x))
                .ToArray();

            return new StringBuilder("[").Append(string.Join(", ", items)).Append("]").ToString();
        }

        private int GetIndex(T item)
        {
            if (item == null)
            {
                return 0;
            }

            return Math.Abs(item.GetHashCode()) % lists.Length;
        }
    }
}
