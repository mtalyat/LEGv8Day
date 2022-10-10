using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    /// <summary>
    /// Represents an int that can be packed with smaller data types, such as shorts, bytes, or bits.
    /// </summary>
    public struct PackedInt : IComparable<PackedInt>, IEquatable<PackedInt>
    {
        /// <summary>
        /// The raw stored integer data.
        /// </summary>
        private int _data;

        /// <summary>
        /// The int form of this PackedInt.
        /// </summary>
        public int Int
        {
            get => _data;
            set => _data = value;
        }

        /// <summary>
        /// Gets or sets the byte at the corresponding index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte this[int index]
        {
            get => (byte)GetByte(index);
            set => SetByte(index, value);
        }

        #region Constructors

        /// <summary>
        /// Creates a new PackedInt using the given int as the initial data.
        /// </summary>
        /// <param name="i"></param>
        public PackedInt(int i)
        {
            _data = i;
        }

        /// <summary>
        /// Creates a new PackedInt using the given PackedInt as the initial data.
        /// </summary>
        /// <param name="other"></param>
        public PackedInt(PackedInt other)
        {
            _data = other.Int;
        }

        /// <summary>
        /// Creates a new PackedInt using the given bytes as the initial data.
        /// </summary>
        /// <param name="b3">The most significant byte.</param>
        /// <param name="b2">The second most significant byte.</param>
        /// <param name="b1">The third most significant byte.</param>
        /// <param name="b0">The least significant byte.</param>
        public PackedInt(byte b3, byte b2, byte b1, byte b0)
        {
            _data = (b3 << 24) | (b2 << 16) | (b1 << 8) | b0;
        }

        #endregion

        #region Operators

        public static implicit operator int(PackedInt i) => i.Int;
        public static implicit operator PackedInt(int i) => new PackedInt(i);

        #endregion

        #region Getting and Setting

        /// <summary>
        /// Gets data stored within the integer, using the given offset and size in bits.
        /// </summary>
        /// <param name="offset">The offset in bits, from the least significant bit side.</param>
        /// <param name="size">The size in bits.</param>
        /// <returns></returns>
        public int Get(int offset, int size)
        {
            //return what we want at the least significant bits side, with everything else zero'd
            return (_data >> offset) & ((1 << size) - 1);
        }

        /// <summary>
        /// Gets data stored within the integer, using the given inclusive start and stop offsets in bits.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public int GetRange(int start, int stop) => Get(start, stop - start + 1);

        /// <summary>
        /// Gets a byte stored within the integer, using the given offset and size in bytes.
        /// </summary>
        /// <param name="byteOffset">The offset in bytes, from the least significant byte side.</param>
        /// <param name="sizeInBytes">The size in bytes.</param>
        /// <returns>The requested data, stored within an integer, shifted to the least significant side.</returns>
        public int GetByte(int byteOffset, int sizeInBytes = 1) => (byte)Get(byteOffset * 8, sizeInBytes * 8);

        /// <summary>
        /// Gets a bool (bit) stored within the integer, using the given offset in bits.
        /// </summary>
        /// <param name="offset">The offset in bits, from the least significant bit side.</param>
        /// <returns>True if the bit is 1, false if the bit is 0.</returns>
        public bool GetBit(int offset) => Get(offset, 1) == 1;

        /// <summary>
        /// Sets the given data within this integer, using the given offset and size in bits.
        /// </summary>
        /// <param name="offset">The offset in bits, from the least significant bit side.</param>
        /// <param name="data">The data to be stored.</param>
        /// <param name="size">The size in bits.</param>
        public void Set(int offset, int data, int size)
        {
            /*
             * Example of what Set does:
             * 
             * data: 11110011_10011010, byteOffset = 1, sizeInBytes = 2
             * 
             * old data:
             * 0b01001101_10100011_10001111_10100110
             * cleared:
             * 0b01001101_00000000_00000000_10100110
             * new data:
             * 0b01001101_11110011_10011010_10100110
             * 
             */

            int c = ((1 << size) - 1);

            //clear out old data
            _data &= ~(c << offset);

            //add in new data
            _data |= (c & data) << offset;

            //_data = (_data & ~(1 << bit)) | (value ? 1 : 0) << bit;
        }

        /// <summary>
        /// Sets the given data within this integer, using the given inclusive start and stop bits, and the given data.
        /// </summary>
        /// <param name="start">The offset in bits, from the least significant bit side.</param>
        /// <param name="stop">The stop offset in bits. Inclusive.</param>
        /// <param name="data">The data to be stored.</param>
        public void SetRange(int start, int stop, int data) => Set(start, data, stop - start + 1);

        /// <summary>
        /// Sets the given data within this integer, using the given offset and size in bytes.
        /// </summary>
        /// <param name="byteOffset">The offset in bytes, from the least significant byte side.</param>
        /// <param name="data">The data to be stored.</param>
        /// <param name="sizeInBytes">The size in bytes.</param>
        public void SetByte(int byteOffset, byte data, int sizeInBytes = 1) => Set(byteOffset * 8, data, sizeInBytes * 8);

        /// <summary>
        /// Sets the given bool (bit) within this integer, using the given offset in bites.
        /// </summary>
        /// <param name="offset">The offset in bits, from the least significant bit side.</param>
        /// <param name="data">The data (bit) to be stored.</param>
        public void SetBit(int offset, bool data) => Set(offset, data ? 1 : 0, 1);

        #endregion

        #region Comparisons

        public int CompareTo(PackedInt other)
        {
            return _data.CompareTo(other._data);
        }

        public bool Equals(PackedInt other)
        {
            return _data == other._data;
        }

        public override bool Equals(object obj)
        {
            return (obj is int || obj is PackedInt) && Equals((PackedInt)obj);
        }

        public override int GetHashCode()
        {
            return _data.GetHashCode();
        }

        #endregion

        /// <summary>
        /// Returns this PackedInt represented in binary form.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Convert.ToString(_data, 2).PadLeft(sizeof(int) * 8, '0');//convert to base 2
        }
    }
}
