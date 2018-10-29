using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nightOwl.Logic
{
    public class PhotoList<T> : IEnumerable<T>
    {
        private List<T> photos;

        public PhotoList() { photos = new List<T>(); }
        public int Count() { return photos.Count; }
        public void Add(T value) { photos.Add(value); }
        public void Clear() { photos.Clear(); }

        public IEnumerator<T> GetEnumerator()
        {
            return photos.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
