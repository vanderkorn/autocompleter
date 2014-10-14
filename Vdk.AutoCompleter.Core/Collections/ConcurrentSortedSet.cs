// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcurrentSortedSet.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the ConcurrentSortedSet type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Collections
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The concurrent sorted set.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element
    /// </typeparam>
    public class ConcurrentSortedSet<T> : IEnumerable where T : class 
    {
        /// <summary>
        /// The sorted set.
        /// </summary>
        readonly SortedSet<T> sortedSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentSortedSet{T}"/> class.
        /// </summary>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        public ConcurrentSortedSet(IComparer<T> comparer)
        {
            this.sortedSet = new SortedSet<T>(comparer);
            MaxSize = int.MaxValue;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                lock (this.sortedSet)
                {
                    return this.sortedSet.Count;
                }
            }
        }

        /// <summary>
        /// Gets or sets the max size.
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(T item)
        {
            lock (this.sortedSet)
            {
                this.sortedSet.Add(item);

                while (this.sortedSet.Count > MaxSize)
                {
                    this.sortedSet.Remove(this.sortedSet.Max);
                }
            }
        }

        /// <summary>
        /// The first or default.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T FirstOrDefault()
        {
            lock (this.sortedSet)
            {
                return this.sortedSet.Count > 0
                    ? this.sortedSet.Min
                    : null;
            }
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Remove(T item)
        {
            lock (this.sortedSet)
            {
                this.sortedSet.Remove(item);
            }
        }

        /// <summary>
        /// The take.
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<T> Take(int i)
        {
            lock (this.sortedSet)
            {
                return this.sortedSet.Take(i);
            }
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            lock (this.sortedSet)
            {
                return this.sortedSet.GetEnumerator();
            }
        }
    }
}
