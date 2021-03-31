using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashTables
{
    class HashTable<T> : ICollection<T>
    {
        private List<T>[] lists;
        private const int DefaultArrayLength = 10;
        private ModificationCounter modificationCounter;

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public HashTable()
        {
            lists = new List<T>[DefaultArrayLength];
            modificationCounter = new ModificationCounter(false);
        }

        public HashTable(int arrayLength)
        {
            if (arrayLength < 1)
            {
                throw new ArgumentException("The array length must be greater than 0", nameof(arrayLength));
            }

            lists = new List<T>[arrayLength];
            modificationCounter = new ModificationCounter(false);
        }

        public void Add(T item)
        {
            CheckArgument(item);

            var index = GetIndex(item);

            if (lists[index] == null)
            {
                lists[index] = new List<T>();
            }

            lists[index].Add(item);
            Count++;
            modificationCounter.Add();
        }

        public void Clear()
        {
            Array.Clear(lists, 0, lists.Length);
            modificationCounter.Add();
        }

        public bool Contains(T item)
        {
            CheckArgument(item);

            var index = GetIndex(item);

            return lists[index].Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if(arrayIndex < 0 || arrayIndex >= lists.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), $"The index goes beyond the boundary [0, {lists.Length}] of the list. Current index value: {arrayIndex}.");
            }

            lists[arrayIndex].CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            CheckArgument(item);

            var index = GetIndex(item);

            if (lists[index].Remove(item))
            {
                Count--;
                modificationCounter.Add();
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            modificationCounter.Enable = true;

            foreach (var list in lists)
            {
                if (modificationCounter.Count > 0)
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

            modificationCounter.Enable = false;
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

        private static void CheckArgument(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), $"The argument cannot be null!");
            }
        }

        private int GetIndex(T item)
        {
            return Math.Abs(item.GetHashCode()) % lists.Length;
        }
    }
}
