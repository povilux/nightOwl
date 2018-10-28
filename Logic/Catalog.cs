using System;
using System.Collections;
using System.Collections.Generic;

namespace nightOwl.Logic
{
    public class Catalog<T> : IEnumerable<T>
    {
        private List<T> labels;

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
    }
}
