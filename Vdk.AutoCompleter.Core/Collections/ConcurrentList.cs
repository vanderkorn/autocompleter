// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcurrentList.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the ConcurrentList.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Collections
{
    using System;
    using System.Threading;

    /// <summary>
    /// The concurrent list.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element
    /// </typeparam>
    public sealed class ConcurrentList<T> : ThreadSafeList<T>
    {
        /// <summary>
        /// The sizes.
        /// </summary>
        static readonly int[] Sizes;

        /// <summary>
        /// The counts.
        /// </summary>
        static readonly int[] Counts;

        /// <summary>
        /// Initializes static members of the <see cref="ConcurrentList"/> class.
        /// </summary>
        static ConcurrentList()
        {
            Sizes = new int[32];
            Counts = new int[32];

            int size = 1;
            int count = 1;
            for (int i = 0; i < Sizes.Length; i++)
            {
                Sizes[i] = size;
                Counts[i] = count;

                if (i < Sizes.Length - 1)
                {
                    size *= 2;
                    count += size;
                }
            }
        }

        /// <summary>
        /// The index.
        /// </summary>
        int index;

        /// <summary>
        /// The fuzzy count.
        /// </summary>
        int fuzzyCount;

        /// <summary>
        /// The count.
        /// </summary>
        int count;

        /// <summary>
        /// The array.
        /// </summary>
        readonly T[][] array;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentList{T}"/> class.
        /// </summary>
        public ConcurrentList()
        {
            this.array = new T[32][];
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public override T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                int arrayIndex = GetArrayIndex(index + 1);
                if (arrayIndex > 0)
                {
                    index -= ((int)Math.Pow(2, arrayIndex) - 1);
                }

                return this.array[arrayIndex][index];
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public override int Count
        {
            get
            {
                return this.count;
            }
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public override void Add(T element)
        {
            int index = Interlocked.Increment(ref this.index) - 1;
            int adjustedIndex = index;

            int arrayIndex = GetArrayIndex(index + 1);
            if (arrayIndex > 0)
            {
                adjustedIndex -= Counts[arrayIndex - 1];
            }

            if (this.array[arrayIndex] == null)
            {
                int arrayLength = Sizes[arrayIndex];
                Interlocked.CompareExchange(ref this.array[arrayIndex], new T[arrayLength], null);
            }

            this.array[arrayIndex][adjustedIndex] = element;

            int count = this.count;
            int fuzzyCount = Interlocked.Increment(ref this.fuzzyCount);
            if (fuzzyCount == index + 1)
            {
                Interlocked.CompareExchange(ref this.count, fuzzyCount, count);
            }
        }

        /// <summary>
        /// The copy to.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public override void CopyTo(T[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            int count = this.count;
            if (array.Length - index < count)
            {
                throw new ArgumentException("There is not enough available space in the destination array.");
            }

            int arrayIndex = 0;
            int elementsRemaining = count;
            while (elementsRemaining > 0)
            {
                T[] source = this.array[arrayIndex++];
                int elementsToCopy = Math.Min(source.Length, elementsRemaining);
                int startIndex = count - elementsRemaining;

                Array.Copy(source, 0, array, startIndex, elementsToCopy);

                elementsRemaining -= elementsToCopy;
            }
        }

        /// <summary>
        /// The get array index.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int GetArrayIndex(int count)
        {
            int arrayIndex = 0;

            if ((count & 0xFFFF0000) != 0)
            {
                count >>= 16;
                arrayIndex |= 16;
            }

            if ((count & 0xFF00) != 0)
            {
                count >>= 8;
                arrayIndex |= 8;
            }

            if ((count & 0xF0) != 0)
            {
                count >>= 4;
                arrayIndex |= 4;
            }

            if ((count & 0xC) != 0)
            {
                count >>= 2;
                arrayIndex |= 2;
            }

            if ((count & 0x2) != 0)
            {
                count >>= 1;
                arrayIndex |= 1;
            }

            return arrayIndex;
        }

        #region Protected methods

        /// <summary>
        /// Gets a value indicating whether is synchronized base.
        /// </summary>
        protected override bool IsSynchronizedBase
        {
            get { return false; }
        }

        #endregion
    }
}