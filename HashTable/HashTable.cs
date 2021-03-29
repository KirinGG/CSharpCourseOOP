using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTables
{
    class HashTable<T> : ICollection<T>
    {
        private List<T>[] keys;
        private int capacity;
        private const int defaultCapacity = 100;

        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                if (value <= 0)
                {
                    value = defaultCapacity;
                }

                capacity = value;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; private set; }

        public HashTable()
        {
            Capacity = defaultCapacity;
            keys = new List<T>[Capacity];
            IsReadOnly = true;
        }

        public HashTable(int capacity)
        {
            Capacity = capacity;
            keys = new List<T>[Capacity];
            IsReadOnly = true;
        }

        public void Add(T item)
        {
            var key = Math.Abs(item.GetHashCode()) % Capacity;

            if (keys[key] == null)
            {
                keys[key] = new List<T>();
            }

            keys[key].Add(item);
            Count++;
        }

        public void Clear()
        {
            Array.Clear(keys, 0, Count);
        }

        public bool Contains(T item)
        {
            var key = Math.Abs(item.GetHashCode()) % Capacity;

            return keys[key].Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            keys.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Capacity; ++i)
            {
                if (keys[i] == null)
                {
                    continue;
                }

                foreach (T item in keys[i])
                {
                    yield return item;

                }
            }
        }

        public bool Remove(T item)
        {
            var key = Math.Abs(item.GetHashCode()) % Capacity;

            if (keys[key].Remove(item))
            {
                Count--;
                return true;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(", ", keys.Where(x => x != null).Select(p => string.Join(", ", p)));
        }
    }
}
