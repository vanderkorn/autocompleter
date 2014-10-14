// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsciiChar.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the AsciiChar type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Models
{
    using System;

    /// <summary>
    /// The ASCII char.
    /// </summary>
    public struct AsciiChar
    {
        /// <summary>
        /// The op_ implicit.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// </returns>
        public static implicit operator Char(AsciiChar value)
        {
            return (char)value.asciiCode;
        }

        /// <summary>
        /// The validate byte.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool ValidateByte(byte value)
        {
            return value >= 0 && value <= 127;
        }

        /// <summary>
        /// The is control.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsControl(byte value)
        {
            return (value >= 0 && value <= 31) ||
                value == 127;
        }

        /// <summary>
        /// The is digit.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsDigit(byte value)
        {
            return value >= 48 && value <= 57;
        }

        /// <summary>
        /// The is letter.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsLetter(byte value)
        {
            return (value >= 65 && value <= 90) ||
                (value >= 97 && value <= 122);
        }

        /// <summary>
        /// The is letter or digit.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsLetterOrDigit(byte value)
        {
            return (value >= 48 && value <= 57) ||
                (value >= 65 && value <= 90) ||
                (value >= 97 && value <= 122);
        }

        /// <summary>
        /// The is lower.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsLower(byte value)
        {
            return value >= 97 && value <= 122;
        }

        /// <summary>
        /// The is punctuation.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
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

        /// <summary>
        /// The is symbol.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
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

        /// <summary>
        /// The is upper.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsUpper(byte value)
        {
            return value >= 65 && value <= 90;
        }

        /// <summary>
        /// The is whitespace.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsWhitespace(byte value)
        {
            return value == 0 || (value >= 9 && value <= 13) || value == 32;

        }

        /// <summary>
        /// The to lower.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        public static byte ToLower(byte value)
        {
            if (AsciiChar.IsUpper(value)) return (byte)(value - 32);
            return value;
        }

        /// <summary>
        /// The to upper.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        public static byte ToUpper(byte value)
        {
            if (AsciiChar.IsLower(value)) return (byte)(value + 32);
            return value;
        }

        /// <summary>
        /// The ascii code.
        /// </summary>
        private readonly byte asciiCode;

        /// <summary>
        /// The compare to.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CompareTo(AsciiChar value)
        {
            return this.asciiCode.CompareTo(value.asciiCode);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Equals(AsciiChar value)
        {
            return this.asciiCode.Equals(value.asciiCode);
        }

        /// <summary>
        /// The is control.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsControl()
        {
            return AsciiChar.IsControl(this.asciiCode);
        }

        /// <summary>
        /// The is digit.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsDigit()
        {
            return AsciiChar.IsDigit(this.asciiCode);
        }

        /// <summary>
        /// The is letter.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsLetter()
        {
            return AsciiChar.IsLetter(this.asciiCode);
        }

        /// <summary>
        /// The is letter or digit.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsLetterOrDigit()
        {
            return AsciiChar.IsLetterOrDigit(this.asciiCode);
        }

        /// <summary>
        /// The is lower.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsLower()
        {
            return AsciiChar.IsLower(this.asciiCode);
        }

        /// <summary>
        /// The is punctuation.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsPunctuation()
        {
            return AsciiChar.IsPunctuation(this.asciiCode);
        }

        /// <summary>
        /// The is symbol.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSymbol()
        {
            return AsciiChar.IsSymbol(this.asciiCode);
        }

        /// <summary>
        /// The is upper.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsUpper()
        {
            return AsciiChar.IsUpper(this.asciiCode);
        }

        /// <summary>
        /// The is whitespace.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsWhitespace()
        {
            return AsciiChar.IsWhitespace(this.asciiCode);

        }

        /// <summary>
        /// The to lower.
        /// </summary>
        /// <returns>
        /// The <see cref="AsciiChar"/>.
        /// </returns>
        public AsciiChar ToLower()
        {
            if (this.IsUpper())
            {
                return new AsciiChar((byte)(this.asciiCode + 32));
            }

            return this;
        }

        /// <summary>
        /// The to byte.
        /// </summary>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        public byte ToByte()
        {
            return this.asciiCode;
        }

        /// <summary>
        /// The to char.
        /// </summary>
        /// <returns>
        /// The <see cref="char"/>.
        /// </returns>
        public char ToChar()
        {
            return (char)this.asciiCode;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return this.ToChar().ToString();
        }

        /// <summary>
        /// The to ascii string.
        /// </summary>
        /// <returns>
        /// The <see cref="AsciiString"/>.
        /// </returns>
        public AsciiString ToAsciiString()
        {
            return new AsciiString(new byte[] { this.asciiCode }, 0, 1);
        }

        /// <summary>
        /// The to upper.
        /// </summary>
        /// <returns>
        /// The <see cref="AsciiChar"/>.
        /// </returns>
        public AsciiChar ToUpper()
        {
            if (this.IsLower())
            {
                return new AsciiChar((byte)(this.asciiCode - 32));
            }

            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsciiChar"/> struct.
        /// </summary>
        /// <param name="asciiCode">
        /// The ascii code.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public AsciiChar(byte asciiCode)
        {
            if (!AsciiChar.ValidateByte(asciiCode))
            {
                throw new ArgumentOutOfRangeException("asciiCode");
            }

            this.asciiCode = asciiCode;
        }

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="AsciiChar"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static AsciiChar Parse(char value)
        {
            if (value < 0 || value > 127)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return new AsciiChar((byte)value);
        }

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="AsciiChar"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="FormatException">
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
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
