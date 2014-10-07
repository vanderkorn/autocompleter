using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Vdk.AutoCompleter.Core.Models
{
    public sealed class AsciiString : IEnumerable<AsciiChar>, IComparable<AsciiString>, IEquatable<AsciiString>
    {
        public static readonly AsciiString Empty;

        static AsciiString()
        {
            Empty = new AsciiString(new byte[] { });
        }

        public static implicit operator String(AsciiString value)
        {
            return value.ToString();
        }
        public static implicit operator AsciiString(String value)
        {
            return AsciiString.Parse(value);
        }
        public static AsciiString operator +(AsciiString strA, AsciiString strB)
        {
            return AsciiString.Concat(strA, strB);
        }
        public static AsciiString operator +(AsciiString str, AsciiChar chr)
        {
            if (str == null) throw new ArgumentNullException("str");

            int totalBytes = str.data.Length + 1;

            byte[] data = new byte[totalBytes];

            Buffer.BlockCopy(str.data, 0, data, 0, str.data.Length);
            data[totalBytes - 1] = chr.ToByte();

            return new AsciiString(data);
        }
        public static bool operator ==(AsciiString strA, AsciiString strB)
        {
            return AsciiString.Compare(strA, strB) == 0;
        }
        public static bool operator !=(AsciiString strA, AsciiString strB)
        {
            return AsciiString.Compare(strA, strB) != 0;
        }

        public static int Compare(AsciiString strA, AsciiString strB)
        {
            return Compare(strA, strB, false);
        }
        public static int Compare(AsciiString strA, AsciiString strB, bool ignoreCase)
        {
            if (ReferenceEquals(strA, null) && ReferenceEquals(strB, null))
                return 0;

            if (ReferenceEquals(strA, null))
                return -1;

            if (ReferenceEquals(strB, null))
                return 1;

            //if (strA == null) throw new ArgumentNullException("strA");
            //if (strB == null) throw new ArgumentNullException("strB");

            return SafeCompare(strA, 0, strB, 0, Math.Max(strA.data.Length, strB.data.Length), ignoreCase);
        }
        public static int Compare(AsciiString strA, int indexA, AsciiString strB, int indexB, int length)
        {
            return Compare(strA, indexA, strB, indexB, length, false);
        }
        public static int Compare(AsciiString strA, int indexA, AsciiString strB, int indexB, int length, bool ignoreCase)
        {
            if (strA == null) throw new ArgumentNullException("strA");
            if (strB == null) throw new ArgumentNullException("strB");
            if (indexA < 0 || indexA > strA.data.Length) throw new ArgumentOutOfRangeException("indexA");
            if (indexB < 0 || indexB > strB.data.Length) throw new ArgumentOutOfRangeException("indexB");
            if (length < 0 || indexA + length > strA.data.Length || indexB + length > strB.data.Length) throw new ArgumentOutOfRangeException("length");

            return SafeCompare(strA, indexA, strB, indexB, length, ignoreCase);
        }
        private static int SafeCompare(AsciiString strA, int indexA, AsciiString strB, int indexB, int length, bool ignoreCase)
        {
            for (int i = 0; i < length; i++)
            {
                int iA = i + indexA;
                int iB = i + indexB;

                if (iA == strA.data.Length && iB == strB.data.Length)
                    return 0;
                if (iA == strA.data.Length) return -1;
                if (iB == strB.data.Length) return 1;

                byte byteA = strA.data[iA];
                byte byteB = strB.data[iB];

                if (ignoreCase)
                {
                    byteA = AsciiChar.ToLower(byteA);
                    byteB = AsciiChar.ToLower(byteB);
                }

                if (byteA < byteB) return -1;
                if (byteB < byteA) return 1;
            }

            return 0;
        }
        public static AsciiString Copy(AsciiString value)
        {
            byte[] data = new byte[value.data.Length];
            Buffer.BlockCopy(value.data, 0, data, 0, value.data.Length);
            return new AsciiString(data);
        }
        public static AsciiString Concat(params AsciiString[] values)
        {
            return Concat((IEnumerable<AsciiString>)values);
        }
        public static AsciiString Concat(IEnumerable<AsciiString> values)
        {
            if (values == null) throw new ArgumentNullException("values");

            int totalBytes = 0;
            int offset = 0;

            foreach (AsciiString asciiString in values)
            {
                if (asciiString == null) continue;
                totalBytes += asciiString.data.Length;
            }

            byte[] data = new byte[totalBytes];

            foreach (AsciiString asciiString in values)
            {
                if (asciiString == null) continue;
                Buffer.BlockCopy(asciiString.data, 0, data, offset, asciiString.data.Length);
                offset += asciiString.data.Length;
            }

            return new AsciiString(data);
        }
        public static bool IsNullOrEmpty(AsciiString value)
        {
            return value == null || value.data.Length == 0;
        }
        public static bool IsNullOrWhitespace(AsciiString value)
        {
            if (value == null || value.data.Length == 0)
            {
                return true;
            }

            foreach (byte b in value.data)
            {
                if (!AsciiChar.IsWhitespace(b))
                {
                    return false;
                }
            }

            return true;
        }
        public static AsciiString Join(AsciiString seperator, params AsciiString[] values)
        {
            return Join(seperator, (IEnumerable<AsciiString>)values);
        }
        public static AsciiString Join(AsciiString seperator, IEnumerable<AsciiString> values)
        {
            if (seperator == null) throw new ArgumentNullException("seperator");
            if (values == null) throw new ArgumentNullException("values");

            int totalBytes = 0;
            int offset = 0;

            foreach (AsciiString asciiString in values)
            {
                if (asciiString == null) continue;
                totalBytes += asciiString.data.Length;
                totalBytes += seperator.data.Length;
            }

            if (totalBytes > 0) totalBytes -= seperator.data.Length;

            byte[] data = new byte[totalBytes];

            foreach (AsciiString asciiString in values)
            {
                if (asciiString == null) continue;

                Buffer.BlockCopy(asciiString.data, 0, data, offset, asciiString.data.Length);
                offset += asciiString.data.Length;

                if (offset < totalBytes)
                {
                    Buffer.BlockCopy(seperator.data, 0, data, offset, seperator.data.Length);
                    offset += seperator.data.Length;
                }
            }

            return new AsciiString(data);
        }
        public static AsciiString Parse(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            return new AsciiString(Encoding.ASCII.GetBytes(value));
        }

        private readonly byte[] data;

        public int Length
        {
            get
            {
                return data.Length;
            }
        }

        public AsciiString(byte[] data, int startIndex, int length)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (startIndex < 0 || startIndex > data.Length) throw new ArgumentOutOfRangeException("startIndex");
            if (length < 0 || startIndex + length > data.Length) throw new ArgumentOutOfRangeException("length");

            foreach (byte b in data)
            {
                if (!AsciiChar.ValidateByte(b))
                {
                    throw new ArgumentOutOfRangeException("data");
                }
            }

            this.data = new byte[length];
            Buffer.BlockCopy(data, startIndex, this.data, 0, length);
        }
        private AsciiString(byte[] data)
        {
            this.data = data;
        }

        public AsciiChar this[int index]
        {
            get
            {
                return new AsciiChar(this.data[index]);
            }
        }

        public AsciiString Clone()
        {
            return this;
        }
        public int CompareTo(AsciiString value)
        {
            if (value == null) throw new ArgumentNullException("value");
            return AsciiString.Compare(this, value);
        }
        public bool Contains(AsciiString value)
        {
            if (value == null) throw new ArgumentNullException("value");
            return this.IndexOf(value) >= 0;
        }
        public bool Contains(AsciiChar value)
        {
            byte valueByte = value.ToByte();

            foreach (byte b in this.data)
            {
                if (b.Equals(valueByte))
                {
                    return true;
                }
            }

            return false;
        }
        public bool EndsWith(AsciiString value)
        {
            return this.EndsWith(value, false);
        }
        public bool EndsWith(AsciiString value, bool ignoreCase)
        {
            if (value == null) throw new ArgumentNullException("value");
            return AsciiString.Compare(this, this.data.Length - value.data.Length, value, 0, value.Length, ignoreCase) == 0;
        }
        public override bool Equals(object obj)
        {
            if (obj is AsciiString)
            {
                return base.Equals((AsciiString)obj);
            }
            else
            {
                return false;
            }
        }
        public bool Equals(AsciiString value)
        {
            return this.CompareTo(value) == 0;
        }
        public override int GetHashCode()
        {
            return ComputeHash(this.data);
        }
        public static int ComputeHash(params byte[] data)
        {
            unchecked
            {
                const int p = 16777619;
                int hash = (int)2166136261;

                for (int i = 0; i < data.Length; i++)
                    hash = (hash ^ data[i]) * p;

                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;
                return hash;
            }
        }

        public int IndexOf(AsciiString value)
        {
            return this.IndexOf(value, 0, this.Length, false);
        }
        public int IndexOf(AsciiString value, bool ignoreCase)
        {
            return this.IndexOf(value, 0, this.Length, ignoreCase);
        }
        public int IndexOf(AsciiString value, int startIndex)
        {
            return this.IndexOf(value, startIndex, this.Length - startIndex, false);
        }
        public int IndexOf(AsciiString value, int startIndex, bool ignoreCase)
        {
            return this.IndexOf(value, startIndex, this.Length - startIndex, ignoreCase);
        }
        public int IndexOf(AsciiString value, int startIndex, int count)
        {
            return this.IndexOf(value, startIndex, count, false);
        }
        public int IndexOf(AsciiString value, int startIndex, int count, bool ignoreCase)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (startIndex < 0 || startIndex > this.data.Length) throw new ArgumentOutOfRangeException("startIndex");
            if (count < 0 || startIndex + count > this.data.Length || count < value.data.Length) throw new ArgumentOutOfRangeException("count");

            int charactersFound = 0;

            for (int i = startIndex; i < startIndex + count; i++)
            {
                if (i + (value.data.Length - charactersFound) > this.data.Length) return -1;

                byte byteA = this.data[i];
                byte byteB = value.data[charactersFound];

                if (ignoreCase)
                {
                    byteA = AsciiChar.ToLower(byteA);
                    byteB = AsciiChar.ToLower(byteB);
                }

                if (byteA == byteB) charactersFound++;
                else charactersFound = 0;

                if (charactersFound == value.data.Length) return (i - charactersFound + 1);
            }

            return -1;
        }
        public int IndexOfAny(params AsciiChar[] values)
        {
            return this.IndexOfAny(values, 0, this.data.Length, false);
        }
        public int IndexOfAny(IEnumerable<AsciiChar> values)
        {
            return this.IndexOfAny(values, 0, this.data.Length, false);
        }
        public int IndexOfAny(IEnumerable<AsciiChar> values, bool ignoreCase)
        {
            return this.IndexOfAny(values, 0, this.data.Length, ignoreCase);
        }
        public int IndexOfAny(IEnumerable<AsciiChar> values, int startIndex)
        {
            return this.IndexOfAny(values, startIndex, this.data.Length - startIndex, false);
        }
        public int IndexOfAny(IEnumerable<AsciiChar> values, int startIndex, bool ignoreCase)
        {
            return this.IndexOfAny(values, startIndex, this.data.Length - startIndex, ignoreCase);
        }
        public int IndexOfAny(IEnumerable<AsciiChar> values, int startIndex, int count)
        {
            return this.IndexOfAny(values, startIndex, count, false);
        }
        public int IndexOfAny(IEnumerable<AsciiChar> values, int startIndex, int count, bool ignoreCase)
        {
            if (values == null) throw new ArgumentNullException("values");
            if (startIndex < 0 || startIndex > this.data.Length) throw new ArgumentOutOfRangeException("startIndex");
            if (count < 0 || startIndex + count > this.data.Length) throw new ArgumentOutOfRangeException("count");

            List<byte> valueBytes = new List<byte>();

            foreach (AsciiChar c in values)
            {
                if (ignoreCase) valueBytes.Add(AsciiChar.ToLower(c.ToByte()));
                else valueBytes.Add(c.ToByte());
            }

            for (int i = 0; i < this.data.Length; i++)
            {
                byte b = this.data[i];
                if (ignoreCase) b = AsciiChar.ToLower(b);
                if (valueBytes.Contains(this.data[i])) return i;
            }

            return -1;
        }
        public AsciiString Insert(AsciiString value, int index)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (index < 0 || index > this.data.Length) throw new ArgumentOutOfRangeException("index");

            int totalBytes = this.data.Length + value.data.Length;
            byte[] data = new byte[totalBytes];

            Buffer.BlockCopy(this.data, 0, data, 0, index);
            Buffer.BlockCopy(value.data, 0, data, index, value.data.Length);
            Buffer.BlockCopy(this.data, index, data, index + value.data.Length, this.data.Length - index);

            return new AsciiString(data);
        }
        public int LastIndexOf(AsciiString value)
        {
            return this.LastIndexOf(value, 0, this.Length, false);
        }
        public int LastIndexOf(AsciiString value, bool ignoreCase)
        {
            return this.LastIndexOf(value, 0, this.Length, ignoreCase);
        }
        public int LastIndexOf(AsciiString value, int startIndex)
        {
            return this.LastIndexOf(value, startIndex, this.Length - startIndex, false);
        }
        public int LastIndexOf(AsciiString value, int startIndex, bool ignoreCase)
        {
            return this.LastIndexOf(value, startIndex, this.Length - startIndex, ignoreCase);
        }
        public int LastIndexOf(AsciiString value, int startIndex, int count)
        {
            return this.LastIndexOf(value, startIndex, count, false);
        }
        public int LastIndexOf(AsciiString value, int startIndex, int count, bool ignoreCase)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (startIndex < 0 || startIndex > this.data.Length) throw new ArgumentOutOfRangeException("startIndex");
            if (count < 0 || startIndex + count > this.data.Length) throw new ArgumentOutOfRangeException("count");

            int lastIndexFound = -1;
            int result = startIndex - 1;

            do
            {
                result = this.IndexOf(value, result + 1, count - (result + 1), ignoreCase);

                if (result >= 0)
                {
                    lastIndexFound = result;
                }
            }
            while (result >= 0 && result + 1 < this.data.Length - value.data.Length);

            return lastIndexFound;
        }
        public int LastIndexOfAny(params AsciiChar[] values)
        {
            return this.LastIndexOfAny(values, 0, this.data.Length, false);
        }
        public int LastIndexOfAny(IEnumerable<AsciiChar> values)
        {
            return this.LastIndexOfAny(values, 0, this.data.Length, false);
        }
        public int LastIndexOfAny(IEnumerable<AsciiChar> values, bool ignoreCase)
        {
            return this.LastIndexOfAny(values, 0, this.data.Length, ignoreCase);
        }
        public int LastIndexOfAny(IEnumerable<AsciiChar> values, int startIndex)
        {
            return this.LastIndexOfAny(values, startIndex, this.data.Length - startIndex, false);
        }
        public int LastIndexOfAny(IEnumerable<AsciiChar> values, int startIndex, bool ignoreCase)
        {
            return this.LastIndexOfAny(values, startIndex, this.data.Length - startIndex, ignoreCase);
        }
        public int LastIndexOfAny(IEnumerable<AsciiChar> values, int startIndex, int count)
        {
            return this.LastIndexOfAny(values, startIndex, count, false);
        }
        public int LastIndexOfAny(IEnumerable<AsciiChar> values, int startIndex, int count, bool ignoreCase)
        {
            if (values == null) throw new ArgumentNullException("values");
            if (startIndex < 0 || startIndex > this.data.Length) throw new ArgumentOutOfRangeException("startIndex");
            if (count < 0 || startIndex + count > this.data.Length) throw new ArgumentOutOfRangeException("count");

            List<byte> valueBytes = new List<byte>();

            foreach (AsciiChar c in values)
            {
                if (ignoreCase) valueBytes.Add(AsciiChar.ToLower(c.ToByte()));
                else valueBytes.Add(c.ToByte());
            }

            int lastIndex = -1;

            for (int i = 0; i < this.data.Length; i++)
            {
                byte b = this.data[i];
                if (ignoreCase) b = AsciiChar.ToLower(b);
                if (valueBytes.Contains(this.data[i])) lastIndex = i;
            }

            return lastIndex;
        }
        public AsciiString PadLeft(int totalLength)
        {
            return this.PadLeft(totalLength, AsciiChars.Space);
        }
        public AsciiString PadLeft(int totalLength, AsciiChar c)
        {
            if (totalLength < this.data.Length) throw new ArgumentOutOfRangeException("totalLength");

            byte[] data = new byte[totalLength];
            byte charByte = c.ToByte();

            int i = 0;

            for (; i + this.data.Length < totalLength; i++)
            {
                data[i] = charByte;
            }

            Buffer.BlockCopy(this.data, 0, data, i, this.data.Length);

            return new AsciiString(data);
        }
        public AsciiString PadRight(int totalLength)
        {
            return this.PadRight(totalLength, AsciiChars.Space);
        }
        public AsciiString PadRight(int totalLength, AsciiChar c)
        {
            if (totalLength < this.data.Length) throw new ArgumentOutOfRangeException("totalLength");

            byte[] data = new byte[totalLength];
            byte charByte = c.ToByte();

            Buffer.BlockCopy(this.data, 0, data, 0, this.data.Length);

            for (int i = this.data.Length; i < totalLength; i++)
            {
                data[i] = charByte;
            }

            return new AsciiString(data);
        }
        public AsciiString Remove(int startIndex)
        {
            return this.Remove(startIndex, this.data.Length - startIndex);
        }
        public AsciiString Remove(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex > this.data.Length) throw new ArgumentOutOfRangeException("startIndex");
            if (count < 0 || startIndex + count > this.data.Length) throw new ArgumentOutOfRangeException("count");

            byte[] data = new byte[this.data.Length - count];

            Buffer.BlockCopy(this.data, 0, data, 0, startIndex);
            Buffer.BlockCopy(this.data, startIndex + count, data, startIndex, this.data.Length - count - startIndex);

            return new AsciiString(data);
        }
        public AsciiString Replace(AsciiString oldString, AsciiString newString)
        {
            if (oldString == null) throw new ArgumentNullException("oldString");
            if (newString == null) throw new ArgumentNullException("newString");

            List<int> indexes = new List<int>();
            int index = 0;

            do
            {
                index = this.IndexOf(oldString, index, false);

                if (index >= 0)
                {
                    indexes.Add(index);
                    index += oldString.data.Length;
                }
            }
            while (index >= 0 && index + oldString.Length < this.data.Length);

            if (indexes.Count == 0)
            {
                return this.Clone();
            }

            byte[] data = new byte[this.data.Length - (oldString.data.Length * indexes.Count) + (newString.data.Length * indexes.Count)];

            int oldIndex = 0;
            int newIndex = 0;

            foreach (int stringIndex in indexes)
            {
                Buffer.BlockCopy(this.data, oldIndex, data, newIndex, stringIndex - oldIndex);
                newIndex += stringIndex - oldIndex;
                oldIndex = stringIndex + oldString.data.Length;
                Buffer.BlockCopy(newString.data, 0, data, newIndex, newString.data.Length);
                newIndex += newString.data.Length;
            }

            Buffer.BlockCopy(this.data, oldIndex, data, newIndex, this.data.Length - oldIndex);

            return new AsciiString(data);
        }
        public AsciiString Replace(AsciiChar oldChar, AsciiChar newChar)
        {
            if (oldChar == newChar) return this.Clone();

            AsciiChar[] oldChars = new AsciiChar[] { oldChar };

            List<int> indexes = new List<int>();
            int index = 0;

            do
            {
                index = this.IndexOfAny(oldChars, index, false);

                if (index >= 0)
                {
                    indexes.Add(index);
                    index++;
                }
            }
            while (index >= 0 && index + 1 < this.data.Length);

            if (indexes.Count == 0) return this.Clone();

            byte[] data = new byte[this.data.Length];

            int oldIndex = 0;
            int newIndex = 0;

            foreach (int stringIndex in indexes)
            {
                Buffer.BlockCopy(this.data, oldIndex, data, newIndex, stringIndex - oldIndex);
                newIndex += stringIndex - oldIndex;
                oldIndex = stringIndex + 1;
                data[newIndex] = newChar.ToByte();
                newIndex++;
            }

            Buffer.BlockCopy(this.data, oldIndex, data, newIndex, this.data.Length - oldIndex);

            return new AsciiString(data);
        }
        public AsciiString[] Split(params AsciiString[] seperators)
        {
            return this.Split(seperators, int.MaxValue, StringSplitOptions.None);
        }
        public AsciiString[] Split(IEnumerable<AsciiString> seperators)
        {
            return this.Split(seperators, int.MaxValue, StringSplitOptions.None);
        }
        public AsciiString[] Split(IEnumerable<AsciiString> seperators, StringSplitOptions options)
        {
            return this.Split(seperators, int.MaxValue, options);
        }
        public AsciiString[] Split(IEnumerable<AsciiString> seperators, int count)
        {
            return this.Split(seperators, count, StringSplitOptions.None);
        }
        public AsciiString[] Split(IEnumerable<AsciiString> seperators, int count, StringSplitOptions options)
        {
            List<AsciiString> parts = new List<AsciiString>();

            int startIndex = 0;

            for (int dataIndex = 0; dataIndex < this.data.Length; dataIndex++)
            {
                int charsFound = 0;
                bool found = false;

                foreach (AsciiString seperator in seperators)
                {
                    charsFound = 0;

                    if (dataIndex + seperator.data.Length > this.data.Length) break;

                    for (int sepIndex = 0; sepIndex < seperator.Length; sepIndex++)
                    {
                        if (this.data[dataIndex + sepIndex] == seperator[sepIndex]) charsFound++;
                        else charsFound = 0;
                    }

                    if (charsFound == seperator.data.Length) found = true;
                }

                if (found)
                {
                    AsciiString part = this.Substring(startIndex, dataIndex - startIndex);
                    if (part.data.Length > 0 || options == StringSplitOptions.None)
                    {
                        parts.Add(part);
                    }
                    startIndex = dataIndex + charsFound;
                    dataIndex += charsFound - 1;

                    if (parts.Count + 1 == count) break;
                }
            }

            AsciiString remainingPart = this.Substring(startIndex);
            if (remainingPart.data.Length > 0 || options == StringSplitOptions.None)
            {
                parts.Add(remainingPart);
            }

            return parts.ToArray();
        }
        public AsciiString[] Split(params AsciiChar[] seperators)
        {
            return this.Split(seperators, int.MaxValue, StringSplitOptions.None);
        }
        public AsciiString[] Split(IEnumerable<AsciiChar> seperators)
        {
            return this.Split(seperators, int.MaxValue, StringSplitOptions.None);
        }
        public AsciiString[] Split(IEnumerable<AsciiChar> seperators, StringSplitOptions options)
        {
            return this.Split(seperators, int.MaxValue, options);
        }
        public AsciiString[] Split(IEnumerable<AsciiChar> seperators, int count)
        {
            return this.Split(seperators, count, StringSplitOptions.None);
        }
        public AsciiString[] Split(IEnumerable<AsciiChar> seperators, int count, StringSplitOptions options)
        {
            List<AsciiString> parts = new List<AsciiString>();

            int startIndex = 0;

            for (int dataIndex = 0; dataIndex < this.data.Length; dataIndex++)
            {
                bool found = false;

                foreach (AsciiChar seperator in seperators)
                {
                    if (this.data[dataIndex] == seperator)
                    {
                        found = true;
                    }
                }

                if (found)
                {
                    AsciiString part = this.Substring(startIndex, dataIndex - startIndex);
                    if (part.data.Length > 0 || options == StringSplitOptions.None)
                    {
                        parts.Add(part);
                    }

                    startIndex = dataIndex + 1;

                    if (parts.Count + 1 == count) break;
                }
            }

            AsciiString remainingPart = this.Substring(startIndex);
            if (remainingPart.data.Length > 0 || options == StringSplitOptions.None)
            {
                parts.Add(remainingPart);
            }

            return parts.ToArray();
        }
        public bool StartsWith(AsciiString value)
        {
            return this.StartsWith(value, false);
        }
        public bool StartsWith(AsciiString value, bool ignoreCase)
        {
            if (value == null) throw new ArgumentNullException("value");
            return AsciiString.Compare(this, 0, value, 0, value.Length, ignoreCase) == 0;
        }
        public AsciiString Substring(int startIndex)
        {
            return this.Substring(startIndex, this.data.Length - startIndex);
        }
        public AsciiString Substring(int startIndex, int length)
        {
            if (startIndex < 0 || startIndex > data.Length) throw new ArgumentOutOfRangeException("startIndex");
            if (length < 0 || startIndex + length > data.Length) throw new ArgumentOutOfRangeException("length");

            byte[] newData = new byte[length];
            Buffer.BlockCopy(data, startIndex, newData, 0, length);
            return new AsciiString(newData);
        }
        public AsciiChar[] ToCharArray()
        {
            AsciiChar[] chars = new AsciiChar[this.data.Length];
            for (int i = 0; i < this.data.Length; i++)
            {
                chars[i] = new AsciiChar(this.data[i]);
            }
            return chars;
        }
        public AsciiString ToLower()
        {
            AsciiString s = AsciiString.Copy(this);

            for (int i = 0; i < s.data.Length; i++)
            {
                byte b = s.data[i];
                if (AsciiChar.IsUpper(b))
                {
                    s.data[i] = AsciiChar.ToLower(b);
                }
            }

            return s;
        }
        public AsciiString ToUpper()
        {
            AsciiString s = AsciiString.Copy(this);

            for (int i = 0; i < s.data.Length; i++)
            {
                byte b = s.data[i];
                if (AsciiChar.IsLower(b))
                {
                    s.data[i] = AsciiChar.ToUpper(b);
                }
            }

            return s;
        }
        public AsciiString Trim()
        {
            int charsAtStart = 0;
            int charsAtEnd = 0;

            for (int i = 0; i < this.data.Length; i++)
            {
                if (AsciiChar.IsWhitespace(this.data[i]))
                {
                    charsAtStart++;
                }
                else
                {
                    break;
                }
            }

            for (int i = this.data.Length - 1; i >= charsAtStart; i--)
            {
                if (AsciiChar.IsWhitespace(this.data[i]))
                {
                    charsAtEnd++;
                }
                else
                {
                    break;
                }
            }

            byte[] data = new byte[this.data.Length - charsAtStart - charsAtEnd];
            Buffer.BlockCopy(this.data, charsAtStart, data, 0, data.Length);
            return new AsciiString(data);
        }
        public AsciiString Trim(params AsciiChar[] values)
        {
            int charsAtStart = 0;
            int charsAtEnd = 0;

            for (int i = 0; i < this.data.Length; i++)
            {
                bool found = false;

                foreach (AsciiChar c in values)
                {
                    if (this.data[i].Equals(c.ToByte()))
                    {
                        charsAtStart++;
                        found = true;
                        break;
                    }
                }

                if (!found) break;
            }

            for (int i = this.data.Length - 1; i >= charsAtStart; i--)
            {
                bool found = false;

                foreach (AsciiChar c in values)
                {
                    if (this.data[i].Equals(c.ToByte()))
                    {
                        charsAtEnd++;
                        found = true;
                        break;
                    }
                }

                if (!found) break;
            }

            byte[] data = new byte[this.data.Length - charsAtStart - charsAtEnd];
            Buffer.BlockCopy(this.data, charsAtStart, data, 0, data.Length);
            return new AsciiString(data);
        }
        public AsciiString TrimEnd()
        {
            int charsAtEnd = 0;

            for (int i = this.data.Length - 1; i >= 0; i--)
            {
                if (AsciiChar.IsWhitespace(this.data[i]))
                {
                    charsAtEnd++;
                }
                else
                {
                    break;
                }
            }

            byte[] data = new byte[this.data.Length - charsAtEnd];
            Buffer.BlockCopy(this.data, 0, data, 0, data.Length);
            return new AsciiString(data);
        }
        public AsciiString TrimEnd(params AsciiChar[] values)
        {
            int charsAtEnd = 0;

            for (int i = this.data.Length - 1; i >= 0; i--)
            {
                bool found = false;

                foreach (AsciiChar c in values)
                {
                    if (this.data[i].Equals(c.ToByte()))
                    {
                        charsAtEnd++;
                        found = true;
                        break;
                    }
                }

                if (!found) break;
            }

            byte[] data = new byte[this.data.Length - charsAtEnd];
            Buffer.BlockCopy(this.data, 0, data, 0, data.Length);
            return new AsciiString(data);
        }
        public AsciiString TrimStart()
        {
            int charsAtStart = 0;

            for (int i = 0; i < this.data.Length; i++)
            {
                if (AsciiChar.IsWhitespace(this.data[i]))
                {
                    charsAtStart++;
                }
                else
                {
                    break;
                }
            }

            byte[] data = new byte[this.data.Length - charsAtStart];
            Buffer.BlockCopy(this.data, charsAtStart, data, 0, data.Length);
            return new AsciiString(data);
        }
        public AsciiString TrimStart(params AsciiChar[] values)
        {
            int charsAtStart = 0;

            for (int i = 0; i < this.data.Length; i++)
            {
                bool found = false;

                foreach (AsciiChar c in values)
                {
                    if (this.data[i].Equals(c.ToByte()))
                    {
                        charsAtStart++;
                        found = true;
                        break;
                    }
                }

                if (!found) break;
            }

            byte[] data = new byte[this.data.Length - charsAtStart];
            Buffer.BlockCopy(this.data, charsAtStart, data, 0, data.Length);
            return new AsciiString(data);
        }

        public override string ToString()
        {
            return Encoding.ASCII.GetString(this.data);
        }

        public IEnumerator<AsciiChar> GetEnumerator()
        {
            foreach (byte b in this.data)
            {
                yield return new AsciiChar(b);
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        int IComparable<AsciiString>.CompareTo(AsciiString other)
        {
            return this.CompareTo(other);
        }
    }
}