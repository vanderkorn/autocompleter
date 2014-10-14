using System;
using System.Linq;
using System.Text;
using Vdk.AutoCompleter.Core.Comparers;

namespace Vdk.AutoCompleter.Core.Extensions
{
    /// <summary>
    /// The byte array extensions.
    /// </summary>
    public static class ByteArrayExtensions
    {
        private static readonly string[] HexValues = BitConverter.ToString(Enumerable.Range(0, 256).Select(x => (byte)x).ToArray()).Split('-');

        /// <summary>
        /// The get hash code ex.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetHashCodeEx(this byte[] buffer)
        {
            const int CONSTANT = 17;
            int hashCode = 37;

            CommonArray common = new CommonArray();
            common.ByteArray = buffer;
            int[] array = common.Int32Array;

            int length = buffer.Length;
            int remainder = length & 3;
            int len = length >> 2;

            int i = 0;

            while (i < len)
            {
                hashCode = CONSTANT * hashCode + array[i];
                i++;
            }

            if (remainder > 0)
            {
                int shift = sizeof(uint) - remainder;
                hashCode = CONSTANT * hashCode + ((array[i] << shift) >> shift);
            }

            return hashCode;
        }

        /// <summary>
        /// The mur mur hash 3. http://en.wikipedia.org/wiki/MurmurHash
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="seed">
        /// The seed.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int MurMurHash3(this byte[] buffer, int seed = 37)
        {
            const uint c1 = 0xcc9e2d51;
            const uint c2 = 0x1b873593;
            const int r1 = 15;
            const int r2 = 13;
            const uint m = 5;
            const uint n = 0xe6546b64;

            uint hash = (uint)seed;

            CommonArray common = new CommonArray();
            common.ByteArray = buffer;
            uint[] array = common.UInt32Array;

            int length = buffer.Length;
            int remainder = length & 3;
            int len = length >> 2;

            int i = 0;

            while (i < len)
            {
                uint k = array[i];

                k *= c1;
                k = (k << r1) | (k >> (32 - r1)); //k = rotl32(k, r1);
                k *= c2;

                hash ^= k;
                hash = (hash << r2) | (hash >> (32 - r2)); //hash = rotl32(hash, r2);
                hash = hash * m + n;

                i++;
            }

            if (remainder > 0)
            {
                int shift = sizeof(uint) - remainder;
                uint k = (array[i] << shift) >> shift;

                k *= c1;
                k = (k << r1) | (k >> (32 - r1)); //k = rotl32(k, r1);
                k *= c2;

                hash ^= k;
            }

            hash ^= (uint)length;

            //hash = fmix(hash);
            hash ^= hash >> 16;
            hash *= 0x85ebca6b;
            hash ^= hash >> 13;
            hash *= 0xc2b2ae35;
            hash ^= hash >> 16;

            return (int)hash;
        }

        /// <summary>
        /// The middle.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public static byte[] Middle(this byte[] buffer, int offset, int length)
        {
            byte[] middle = new byte[length];
            Buffer.BlockCopy(buffer, offset, middle, 0, length);
            return middle;
        }

        /// <summary>
        /// The left.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public static byte[] Left(this byte[] buffer, int length)
        {
            return buffer.Middle(0, length);
        }

        /// <summary>
        /// The right.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public static byte[] Right(this byte[] buffer, int length)
        {
            return buffer.Middle(buffer.Length - length, length);
        }

        /// <summary>
        /// Convert byte array to hex string
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string ToHex(this byte[] buffer)
        {
            StringBuilder sb = new StringBuilder(2 * buffer.Length);

            for (int i = 0; i < buffer.Length; i++)
                sb.Append(HexValues[buffer[i]]);

            return sb.ToString();
        }

        /// <summary>
        /// The get bit.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="bitIndex">
        /// The bit index.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetBit(this byte[] map, int bitIndex)
        {
            return (map[bitIndex >> 3] >> (bitIndex & 7)) & 1;
        }

        /// <summary>
        /// The set bit.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="bitIndex">
        /// The bit index.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void SetBit(this byte[] map, int bitIndex, int value)
        {
            int bitMask = 1 << (bitIndex & 7);
            if (value != 0)
                map[bitIndex >> 3] |= (byte)bitMask;
            else
                map[bitIndex >> 3] &= (byte)(~bitMask);
        }
    }
}