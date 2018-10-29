using System;
using System.Collections;
using System.Collections.Generic;

namespace nightOwl.Logic
{
    public class Catalog<T> : ICollection<T>
    {
        private List<T> labels;

        int ICollection<T>.Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public Catalog()
        {
            labels = new List<T>();
        }

        public int Count()
        {
            return labels.Count;
        }

        public void Add(T value)
        {
            labels.Add(value);
        }
        public void Clear()
        {
            labels.Clear();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return labels.GetEnumerator();
        }
        public T[] ToArray()
        {
            return labels.ToArray();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }
    }
}
