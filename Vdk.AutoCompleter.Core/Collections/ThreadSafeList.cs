// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadSafeList.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the ThreadSafeList type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// The thread safe list.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element
    /// </typeparam>
    public abstract class ThreadSafeList<T> : IList<T>, IList
    {
        /// <summary>
        /// Gets the count list.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// The indexer.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public abstract T this[int index] { get; }

        /// <summary>
        /// The add item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public abstract void Add(T item);

        /// <summary>
        /// The index of item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public virtual int IndexOf(T item)
        {
            IEqualityComparer<T> comparer = EqualityComparer<T>.Default;

            var count = this.Count;
            for (var i = 0; i < count; i++)
            {
                if (comparer.Equals(item, this[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// The contains item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Contains(T item)
        {
            return this.IndexOf(item) != -1;
        }

        /// <summary>
        /// The copy from array.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="arrayIndex">
        /// The array index.
        /// </param>
        public abstract void CopyTo(T[] array, int arrayIndex);

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            var count = this.Count;
            for (var i = 0; i < count; i++)
            {
                yield return this[i];
            }
        }

        #region Protected methods

        /// <summary>
        /// Gets a value indicating whether is synchronized base.
        /// </summary>
        protected abstract bool IsSynchronizedBase { get; }

        /// <summary>
        /// The copy to base.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="arrayIndex">
        /// The array index.
        /// </param>
        protected virtual void CopyToBase(Array array, int arrayIndex)
        {
            for (int i = 0; i < this.Count; ++i)
            {
                array.SetValue(this[i], arrayIndex + i);
            }
        }

        /// <summary>
        /// The add base.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int AddBase(object value)
        {
            this.Add((T)value);
            return this.Count - 1;
        }

        #endregion

        #region Explicit interface implementations

        /// <summary>
        /// The i list&lt; t&gt;.this.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// </exception>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T IList<T>.this[int index]
        {
            get { return this[index]; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// </exception>
        void IList<T>.Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The remove at.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// </exception>
        void IList<T>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets a value indicating whether is read only.
        /// </summary>
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// The clear.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// </exception>
        void ICollection<T>.Clear()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// </exception>
        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets a value indicating whether is fixed size.
        /// </summary>
        bool IList.IsFixedSize
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether is read only.
        /// </summary>
        bool IList.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// The i list.this.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// </exception>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object IList.this[int index]
        {
            get { return this[index]; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// The remove at.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// </exception>
        void IList.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// </exception>
        void IList.Remove(object value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <exception cref="NotSupportedException">
        /// </exception>
        void IList.Insert(int index, object value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The index of.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int IList.IndexOf(object value)
        {
            return this.IndexOf((T)value);
        }

        /// <summary>
        /// The clear.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// </exception>
        void IList.Clear()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IList.Contains(object value)
        {
            return ((IList)this).IndexOf(value) != -1;
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int IList.Add(object value)
        {
            return this.AddBase(value);
        }

        /// <summary>
        /// Gets a value indicating whether is synchronized.
        /// </summary>
        bool ICollection.IsSynchronized
        {
            get { return this.IsSynchronizedBase; }
        }

        /// <summary>
        /// Gets the sync root.
        /// </summary>
        object ICollection.SyncRoot
        {
            get { return null; }
        }

        /// <summary>
        /// The copy to.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="arrayIndex">
        /// The array index.
        /// </param>
        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            this.CopyToBase(array, arrayIndex);
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}