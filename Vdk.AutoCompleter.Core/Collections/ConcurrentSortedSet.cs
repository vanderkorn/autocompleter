using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vdk.AutoCompleter.Core.Models;

namespace Vdk.AutoCompleter.Core.Collections
{
    class ConcurrentSortedSet<T> : IEnumerable where T :class 
    {
        readonly SortedSet<T> _sortedSet;
        public ConcurrentSortedSet(IComparer<T> comparer)
        {
            _sortedSet = new SortedSet<T>(comparer);
            MaxSize = int.MaxValue;
        }

        public int Count
        {
            get
            {
                lock (_sortedSet)
                {
                    return _sortedSet.Count;
                }
            }
        }

        public int MaxSize { get; set; }
        public void Add(T item)
        {
            lock (_sortedSet)
            {
                _sortedSet.Add(item);

                while (_sortedSet.Count > MaxSize)
                {
                    _sortedSet.Remove(_sortedSet.Max);
                }
            }
        }

        public T FirstOrDefault()
        {
            lock (_sortedSet)
            {
                return _sortedSet.Count > 0
                    ? _sortedSet.Min
                    : null;
            }
        }

        public void Remove(T item)
        {
            lock (_sortedSet)
            {
                _sortedSet.Remove(item);
            }
        }


        public IEnumerable<T> Take(int i)
        {
            lock (_sortedSet)
            {
                return _sortedSet.Take(i);
            }
        }

        public IEnumerator GetEnumerator()
        {
            lock (_sortedSet)
            {
                return _sortedSet.GetEnumerator();
            }
        }
    }
}
