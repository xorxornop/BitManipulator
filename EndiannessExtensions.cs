#region License

//  	Copyright 2013-2014 Matthew Ducker
//  	
//  	Licensed under the Apache License, Version 2.0 (the "License");
//  	you may not use this file except in compliance with the License.
//  	
//  	You may obtain a copy of the License at
//  		
//  		http://www.apache.org/licenses/LICENSE-2.0
//  	
//  	Unless required by applicable law or agreed to in writing, software
//  	distributed under the License is distributed on an "AS IS" BASIS,
//  	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  	See the License for the specific language governing permissions and 
//  	limitations under the License.

#endregion

using System;
using System.Runtime.CompilerServices;

namespace BitManipulationExtensions
{
    /// <summary>
    ///     Extension methods for packing/unpacking integral numbers into/out of byte arrays, in big-or-little endian formats.
    /// </summary>
    public static class EndiannessExtensions
    {
        #region Big-endian Int32

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into a byte array
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBigEndian(this Int32 n)
        {
            var bs = new byte[sizeof(Int32)];
            n.ToBigEndian(bs);
            return bs;
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int32 n, byte[] ba)
        {
            ValidateArguments32(ba);
            ba[0] = (byte)(n >> 24);
            ba[1] = (byte)(n >> 16);
            ba[2] = (byte)(n >> 8);
            ba[3] = (byte)(n);
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int32 n, byte[] ba, int baOff)
        {
            ValidateArguments32(ba, baOff);
            ba[baOff + 0] = (byte)(n >> 24);
            ba[baOff + 1] = (byte)(n >> 16);
            ba[baOff + 2] = (byte)(n >> 8);
            ba[baOff + 3] = (byte)(n);
        }

        /// <summary>
        ///     Unpacks a signed integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BigEndianToInt32(this byte[] ba)
        {
            ValidateArguments32(ba);
            return ba[0] << 24
                   | ba[1] << 16
                   | ba[2] << 8
                   | ba[3];
        }

        /// <summary>
        ///     Unpacks a signed integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BigEndianToInt32(this byte[] ba, int baOff)
        {
            ValidateArguments32(ba, baOff);
            return ba[baOff] << 24
                   | ba[baOff + 1] << 16
                   | ba[baOff + 2] << 8
                   | ba[baOff + 3];
        }

        #endregion

        #region Big-endian Int64

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into a byte array
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBigEndian(this Int64 n)
        {
            var bs = new byte[sizeof(Int64)];
            n.ToBigEndian(bs);
            return bs;
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int64 n, byte[] ba)
        {
            ValidateArguments64(ba);
            ((Int32)(n >> 32)).ToBigEndian(ba, 0);
            ((Int32)(n)).ToBigEndian(ba, 4);
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int64 n, byte[] ba, int baOff)
        {
            ValidateArguments64(ba, baOff);
            ((Int32)(n >> 32)).ToBigEndian(ba, baOff);
            ((Int32)(n)).ToBigEndian(ba, baOff + 4);
        }

        /// <summary>
        ///     Unpacks a signed integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to read integer from.</param>
        /// <returns>64-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 BigEndianToInt64(this byte[] ba)
        {
            ValidateArguments64(ba);
            Int32 hi = ba.BigEndianToInt32();
            Int32 lo = ba.BigEndianToInt32(4);
            return ((Int64)hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks a signed integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to read integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>64-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 BigEndianToInt64(this byte[] ba, int baOff)
        {
            ValidateArguments64(ba, baOff);
            Int32 hi = ba.BigEndianToInt32(baOff);
            Int32 lo = ba.BigEndianToInt32(baOff + 4);
            return ((Int64)hi << 32) | lo;
        }

        #endregion

        #region Big-endian UInt16

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into a byte array
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBigEndian(this UInt16 n)
        {
            var bs = new byte[sizeof(UInt16)];
            n.ToBigEndian(bs);
            return bs;
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this UInt16 n, byte[] ba)
        {
            ValidateArguments16(ba);
            ba[0] = (byte)(n >> 8);
            ba[1] = (byte)(n);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this UInt16 n, byte[] ba, int baOff)
        {
            ValidateArguments16(ba, baOff);
            ba[baOff] = (byte)(n >> 8);
            ba[baOff + 1] = (byte)(n);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to read integer from.</param>
        /// <returns>16-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 BigEndianToUInt16(this byte[] ba)
        {
            ValidateArguments16(ba);
            uint n = (uint)ba[0] << 8
                     | (uint)ba[1];
            return (ushort)n;
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to read integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>16-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 BigEndianToUInt16(this byte[] ba, int baOff)
        {
            ValidateArguments16(ba, baOff);
            uint n = (uint)ba[baOff] << 8
                     | (uint)ba[baOff + 1];
            return (ushort)n;
        }

        #endregion

        #region Big-endian UInt32

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into a byte array
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBigEndian(this UInt32 n)
        {
            var bs = new byte[sizeof(UInt32)];
            n.ToBigEndian(bs);
            return bs;
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this UInt32 n, byte[] ba)
        {
            ValidateArguments32(ba);
            ba[0] = (byte)(n >> 24);
            ba[1] = (byte)(n >> 16);
            ba[2] = (byte)(n >> 8);
            ba[3] = (byte)(n);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this UInt32 n, byte[] ba, int baOff)
        {
            ValidateArguments32(ba, baOff);
            ba[baOff + 0] = (byte)(n >> 24);
            ba[baOff + 1] = (byte)(n >> 16);
            ba[baOff + 2] = (byte)(n >> 8);
            ba[baOff + 3] = (byte)(n);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to read integer from.</param>
        /// <returns>32-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 BigEndianToUInt32(this byte[] ba)
        {
            ValidateArguments32(ba);
            return (UInt32)ba[0] << 24
                   | (UInt32)ba[1] << 16
                   | (UInt32)ba[2] << 8
                   | ba[3];
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to read integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>32-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 BigEndianToUInt32(this byte[] ba, int baOff)
        {
            ValidateArguments32(ba, baOff);
            return (UInt32)ba[baOff] << 24
                   | (UInt32)ba[baOff + 1] << 16
                   | (UInt32)ba[baOff + 2] << 8
                   | ba[baOff + 3];
        }



        public static void BigEndianToUInt32(this byte[] ba, int baOff, UInt32[] uintArray, int countUints, int uaOff = 0)
        {

        }

        public static void BigEndianToUInt32Internal(this byte[] ba, int baOff, UInt32[] ua, int countUints, int uaOff = 0)
        {
            int uintBytesCount = countUints * sizeof(UInt32);
            int endOff = baOff + uintBytesCount;

            for (int bai = baOff, uai = 0; bai < endOff; bai += 4, uai++) {
                ua[uai] = (UInt32)ba[baOff] << 24
                   | (UInt32)ba[baOff + 1] << 16
                   | (UInt32)ba[baOff + 2] << 8
                   | ba[baOff + 3];
            }
        }

        #endregion

        #region Big-endian UInt64

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into a byte array
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBigEndian(this UInt64 n)
        {
            var bs = new byte[sizeof(UInt64)];
            n.ToBigEndian(bs);
            return bs;
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this UInt64 n, byte[] ba)
        {
            ValidateArguments64(ba);
            ((UInt32)(n >> 32)).ToBigEndian(ba, 0);
            ((UInt32)(n)).ToBigEndian(ba, 4);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this UInt64 n, byte[] ba, int baOff)
        {
            ValidateArguments64(ba, baOff);
            ((UInt32)(n >> 32)).ToBigEndian(ba, baOff);
            ((UInt32)(n)).ToBigEndian(ba, baOff + 4);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to read integer from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 BigEndianToUInt64(this byte[] ba)
        {
            ValidateArguments64(ba);
            UInt32 hi = ba.BigEndianToUInt32();
            UInt32 lo = ba.BigEndianToUInt32(4);
            return ((UInt64)hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to read integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 BigEndianToUInt64(this byte[] ba, int baOff)
        {
            ValidateArguments64(ba, baOff);
            UInt32 hi = ba.BigEndianToUInt32(baOff);
            UInt32 lo = ba.BigEndianToUInt32(baOff + 4);
            return ((UInt64)hi << 32) | lo;
        }

        #endregion

        #region Little-endian Int32

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into a byte array
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToLittleEndian(this Int32 n)
        {
            var bs = new byte[sizeof(Int32)];
            n.ToLittleEndian(bs);
            return bs;
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this Int32 n, byte[] ba)
        {
            ValidateArguments32(ba);
            ba[0] = (byte)(n);
            ba[1] = (byte)(n >> 8);
            ba[2] = (byte)(n >> 16);
            ba[3] = (byte)(n >> 24);
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this Int32 n, byte[] ba, int baOff)
        {
            ValidateArguments32(ba, baOff);
            ba[baOff + 0] = (byte)(n);
            ba[baOff + 1] = (byte)(n >> 8);
            ba[baOff + 2] = (byte)(n >> 16);
            ba[baOff + 3] = (byte)(n >> 24);
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to read integer from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LittleEndianToInt32(this byte[] ba)
        {
            ValidateArguments32(ba);
            return ba[0]
                   | ba[1] << 8
                   | ba[2] << 16
                   | ba[3] << 24;
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to read integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LittleEndianToInt32(this byte[] ba, int baOff)
        {
            ValidateArguments32(ba, baOff);
            return ba[baOff]
                   | ba[baOff + 1] << 8
                   | ba[baOff + 2] << 16
                   | ba[baOff + 3] << 24;
        }

        #endregion

        #region Little-endian Int64

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into a byte array
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToLittleEndian(this Int64 n)
        {
            var bs = new byte[sizeof(Int64)];
            n.ToLittleEndian(bs);
            return bs;
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this Int64 n, byte[] ba)
        {
            ValidateArguments64(ba);
            ((Int32)n).ToLittleEndian(ba, 0);
            ((Int32)(n >> 32)).ToLittleEndian(ba, 4);
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this Int64 n, byte[] ba, int baOff)
        {
            ValidateArguments64(ba, baOff);
            ((Int32)n).ToLittleEndian(ba, baOff);
            ((Int32)(n >> 32)).ToLittleEndian(ba, baOff + 4);
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>64-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 LittleEndianToInt64(this byte[] ba)
        {
            ValidateArguments64(ba);
            Int32 lo = ba.LittleEndianToInt32(0);
            Int32 hi = ba.LittleEndianToInt32(4);
            return ((Int64)hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>64-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 LittleEndianToInt64(this byte[] ba, int baOff)
        {
            ValidateArguments64(ba, baOff);
            Int32 lo = ba.LittleEndianToInt32(baOff);
            Int32 hi = ba.LittleEndianToInt32(baOff + 4);
            return ((Int64)hi << 32) | lo;
        }

        #endregion

        #region Little-endian UInt16

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into a byte array
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToLittleEndian(this UInt16 n)
        {
            var bs = new byte[sizeof(UInt16)];
            n.ToLittleEndian(bs);
            return bs;
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this UInt16 n, byte[] ba)
        {
            ValidateArguments16(ba);
            ba[0] = (byte)(n);
            ba[1] = (byte)(n >> 8);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ToLittleEndian(this UInt16 n, byte[] ba, int baOff)
        {
            ValidateArguments16(ba, baOff);
            ba[baOff] = (byte)(n);
            ba[baOff + 1] = (byte)(n >> 8);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>16-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static UInt16 LittleEndianToUInt16(this byte[] ba)
        {
            ValidateArguments16(ba);
            UInt32 n = (UInt32)ba[0]
                     | (UInt32)ba[1] << 8;
            return (UInt16)n;
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>16-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static UInt16 LittleEndianToUInt16(this byte[] ba, int baOff)
        {
            ValidateArguments16(ba, baOff);
            UInt32 n = (UInt32)ba[baOff]
                     | (UInt32)ba[baOff + 1] << 8;
            return (UInt16)n;
        }

        #endregion

        #region Little-endian UInt32

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into a byte array
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToLittleEndian(this UInt32 n)
        {
            var bs = new byte[sizeof(UInt32)];
            n.ToLittleEndian(bs);
            return bs;
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this UInt32 n, byte[] ba)
        {
            ValidateArguments32(ba);
            ba[0] = (byte)(n);
            ba[1] = (byte)(n >> 8);
            ba[2] = (byte)(n >> 16);
            ba[3] = (byte)(n >> 24);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this UInt32 n, byte[] ba, int baOff)
        {
            ValidateArguments32(ba, baOff);
            ba[baOff + 0] = (byte)(n);
            ba[baOff + 1] = (byte)(n >> 8);
            ba[baOff + 2] = (byte)(n >> 16);
            ba[baOff + 3] = (byte)(n >> 24);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>32-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 LittleEndianToUInt32(this byte[] ba)
        {
            ValidateArguments32(ba);
            return ba[0]
                   | (UInt32)ba[1] << 8
                   | (UInt32)ba[2] << 16
                   | (UInt32)ba[3] << 24;
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>32-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 LittleEndianToUInt32(this byte[] ba, int baOff)
        {
            ValidateArguments32(ba, baOff);
            return ba[baOff]
                   | (UInt32)ba[baOff + 1] << 8
                   | (UInt32)ba[baOff + 2] << 16
                   | (UInt32)ba[baOff + 3] << 24;
        }

        internal static void LittleEndianToUInt32(this byte[] ba, int baOff, uint[] ns)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt32);
            if (ba.Length >= umLimit) {
                unsafe {
                    fixed (byte* inPtr = ba) {
                        var inUintPtr = (UInt32*)inPtr + baOff;
                        fixed (UInt32* outPtr = ns) {
                            var nsLen = ns.Length;
                            for (var i = 0; i < nsLen; i++) {
                                outPtr[i] = inUintPtr[i];
                            }
                        }
                    }
                }
            } else {  
#endif
            for (int i = 0; i < ns.Length; i++) {
                ns[i] = ba.LittleEndianToUInt32(baOff + (i * sizeof(UInt32)));
            }
#if INCLUDE_UNSAFE
            }
#endif
        }

        #endregion

        #region Little-endian UInt64

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into a byte array
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToLittleEndian(this UInt64 n)
        {
            var bs = new byte[sizeof(UInt64)];
            n.ToLittleEndian(bs);
            return bs;
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this UInt64 n, byte[] ba)
        {
            ValidateArguments64(ba);
            ((UInt32)n).ToLittleEndian(ba, 0);
            ((UInt32)(n >> 32)).ToLittleEndian(ba, 4);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this UInt64 n, byte[] ba, int baOff)
        {
            ValidateArguments64(ba, baOff);
            ((UInt32)n).ToLittleEndian(ba, baOff);
            ((UInt32)(n >> 32)).ToLittleEndian(ba, baOff + 4);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 LittleEndianToUInt64(this byte[] ba)
        {
            ValidateArguments64(ba);
            UInt32 lo = ba.LittleEndianToUInt32(0);
            UInt32 hi = ba.LittleEndianToUInt32(4);
            return ((UInt64)hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 LittleEndianToUInt64(this byte[] ba, int baOff)
        {
            ValidateArguments64(ba, baOff);
            UInt32 lo = ba.LittleEndianToUInt32(baOff);
            UInt32 hi = ba.LittleEndianToUInt32(baOff + 4);
            return ((UInt64)hi << 32) | lo;
        }

        #endregion


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments16(byte[] ba, int baOff = 0)
        {
            ValidateArgumentsImpl(ba, baOff, 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments32(byte[] ba, int baOff = 0)
        {
            ValidateArgumentsImpl(ba, baOff, 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments64(byte[] ba, int baOff = 0)
        {
            ValidateArgumentsImpl(ba, baOff, 8);
        }

        /// <summary>
        ///     Validate arguments/parameters to prevent unexpected exceptions. 
        ///     Throws well-defined exceptions for different types of invalid arguments(s). 
        /// </summary>
        /// <param name="ba">Byte array.</param>
        /// <param name="baOff">Offset in <paramref name="ba"/>.</param>
        /// <param name="sizeOfT">
        ///     Size of the type associated with the calling method that is verifying arguments (e.g. uint type size = 4).
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="ba"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Length of <paramref name="ba"/> less than 0, 
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="baOff"/> less than 0, or <paramref name="baOff"/> more than 
        ///     length of <paramref name="ba"/> - <paramref name="sizeOfT"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArgumentsImpl(byte[] ba, int baOff, int sizeOfT)
        {
            if (ba == null) {
                throw new ArgumentNullException("ba");
            }
            if (ba.Length < 0) {
                throw new ArgumentException("ba.Length < 0", "ba");
            }
            if (baOff < 0 || baOff > ba.Length - sizeOfT) {
                throw new ArgumentOutOfRangeException("baOff", "baOff < 0 , or baOff > ba.Length - value type size");
            }
        }
    }
}
