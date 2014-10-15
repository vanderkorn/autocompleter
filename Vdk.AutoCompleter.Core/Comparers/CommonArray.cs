namespace Vdk.AutoCompleter.Core.Comparers
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The common array.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct CommonArray
    {
        /// <summary>
        /// The byte array.
        /// </summary>
        [FieldOffset(0)]
        public byte[] ByteArray;

        /// <summary>
        /// The int 16 array.
        /// </summary>
        [FieldOffset(0)]
        public short[] Int16Array;

        /// <summary>
        /// The u int 16 array.
        /// </summary>
        [FieldOffset(0)]
        public ushort[] UInt16Array;

        /// <summary>
        /// The int 32 array.
        /// </summary>
        [FieldOffset(0)]
        public int[] Int32Array;

        /// <summary>
        /// The u int 32 array.
        /// </summary>
        [FieldOffset(0)]
        public uint[] UInt32Array;

        /// <summary>
        /// The int 64 array.
        /// </summary>
        [FieldOffset(0)]
        public long[] Int64Array;

        /// <summary>
        /// The u int 64 array.
        /// </summary>
        [FieldOffset(0)]
        public ulong[] UInt64Array;

        /// <summary>
        /// The single array.
        /// </summary>
        [FieldOffset(0)]
        public float[] SingleArray;

        /// <summary>
        /// The double array.
        /// </summary>
        [FieldOffset(0)]
        public double[] DoubleArray;
    }
}