using System;

namespace Vdk.AutoCompleter.Core.Models
{
    public struct AsciiChar
    {
        public static implicit operator Char(AsciiChar value)
        {
            return (char)value.asciiCode;
        }

        public static bool ValidateByte(byte value)
        {
            return value >= 0 && value <= 127;
        }
        public static bool IsControl(byte value)
        {
            return (value >= 0 && value <= 31) ||
                value == 127;
        }
        public static bool IsDigit(byte value)
        {
            return value >= 48 && value <= 57;
        }
        public static bool IsLetter(byte value)
        {
            return (value >= 65 && value <= 90) ||
                (value >= 97 && value <= 122);
        }
        public static bool IsLetterOrDigit(byte value)
        {
            return (value >= 48 && value <= 57) ||
                (value >= 65 && value <= 90) ||
                (value >= 97 && value <= 122);
        }
        public static bool IsLower(byte value)
        {
            return value >= 97 && value <= 122;
        }
        public static bool IsPunctuation(byte value)
        {
            return (value >= 33 && value <= 35) ||
                (value >= 37 && value <= 42) ||
                (value >= 44 && value <= 47) ||
                (value >= 58 && value <= 59) ||
                (value >= 63 && value <= 64) ||
                (value >= 91 && value <= 93) ||
                value == 95 ||
                value == 123 ||
                value == 125;
        }
        public static bool IsSymbol(byte value)
        {
            return value == 36 ||
                value == 43 ||
                (value >= 60 && value <= 62) ||
                value == 94 ||
                value == 96 ||
                value == 124 ||
                value == 126;
        }
        public static bool IsUpper(byte value)
        {
            return value >= 65 && value <= 90;
        }
        public static bool IsWhitespace(byte value)
        {
            return value == 0 || (value >= 9 && value <= 13) || value == 32;

        }
        public static byte ToLower(byte value)
        {
            if (AsciiChar.IsUpper(value)) return (byte)(value - 32);
            return value;
        }
        public static byte ToUpper(byte value)
        {
            if (AsciiChar.IsLower(value)) return (byte)(value + 32);
            return value;
        }

        private readonly byte asciiCode;

        public int CompareTo(AsciiChar value)
        {
            return this.asciiCode.CompareTo(value.asciiCode);
        }
        public bool Equals(AsciiChar value)
        {
            return this.asciiCode.Equals(value.asciiCode);
        }
        public bool IsControl()
        {
            return AsciiChar.IsControl(this.asciiCode);
        }
        public bool IsDigit()
        {
            return AsciiChar.IsDigit(this.asciiCode);
        }
        public bool IsLetter()
        {
            return AsciiChar.IsLetter(this.asciiCode);
        }
        public bool IsLetterOrDigit()
        {
            return AsciiChar.IsLetterOrDigit(this.asciiCode);
        }
        public bool IsLower()
        {
            return AsciiChar.IsLower(this.asciiCode);
        }
        public bool IsPunctuation()
        {
            return AsciiChar.IsPunctuation(this.asciiCode);
        }
        public bool IsSymbol()
        {
            return AsciiChar.IsSymbol(this.asciiCode);
        }
        public bool IsUpper()
        {
            return AsciiChar.IsUpper(this.asciiCode);
        }
        public bool IsWhitespace()
        {
            return AsciiChar.IsWhitespace(this.asciiCode);

        }
        public AsciiChar ToLower()
        {
            if (this.IsUpper())
            {
                return new AsciiChar((byte)(this.asciiCode + 32));
            }

            return this;
        }
        public byte ToByte()
        {
            return this.asciiCode;
        }
        public char ToChar()
        {
            return (char)this.asciiCode;
        }
        public override string ToString()
        {
            return this.ToChar().ToString();
        }
        public AsciiString ToAsciiString()
        {
            return new AsciiString(new byte[] { this.asciiCode }, 0, 1);
        }
        public AsciiChar ToUpper()
        {
            if (this.IsLower())
            {
                return new AsciiChar((byte)(this.asciiCode - 32));
            }

            return this;
        }

        public AsciiChar(byte asciiCode)
        {
            if (!AsciiChar.ValidateByte(asciiCode))
            {
                throw new ArgumentOutOfRangeException("asciiCode");
            }

            this.asciiCode = asciiCode;
        }

        public static AsciiChar Parse(char value)
        {
            if (value < 0 || value > 127)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return new AsciiChar((byte)value);
        }
        public static AsciiChar Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (value.Length != 1)
            {
                throw new FormatException();
            }

            if (value[0] > 127)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return new AsciiChar((byte)value[0]);
        }
    }
}
