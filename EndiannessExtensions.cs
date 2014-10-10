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
using System.Diagnostics;
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
        ///     Packs an signed integer <paramref name="n" /> into a byte array
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
        ///     Packs an signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int32 n, byte[] ba)
        {
            ValidateArguments32(ba);
            n.ToBigEndian_NoChecks(ba);
        }

        /// <summary>
        ///     Packs an signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian_NoChecks(this Int32 n, byte[] ba)
        {
            ba[0] = (byte)(n >> 24);
            ba[1] = (byte)(n >> 16);
            ba[2] = (byte)(n >> 8);
            ba[3] = (byte)(n);
        }

        /// <summary>
        ///     Packs an signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int32 n, byte[] ba, int baOff)
        {
            ValidateArguments32(ba);
            n.ToBigEndian_NoChecks(ba, baOff);
        }

        /// <summary>
        ///     Packs an signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian_NoChecks(this Int32 n, byte[] ba, int baOff)
        {
            ba[baOff + 0] = (byte)(n >> 24);
            ba[baOff + 1] = (byte)(n >> 16);
            ba[baOff + 2] = (byte)(n >> 8);
            ba[baOff + 3] = (byte)(n);
        }

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BigEndianToInt32(this byte[] ba)
        {
            ValidateArguments32(ba);
            return ba.BigEndianToInt32_NoChecks();
        }

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BigEndianToInt32_NoChecks(this byte[] ba)
        {
            return ba[0] << 24
                   | ba[1] << 16
                   | ba[2] << 8
                   | ba[3];
        }

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BigEndianToInt32(this byte[] ba, int baOff)
        {
            ValidateArguments32(ba, baOff);
            return ba.BigEndianToInt32_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BigEndianToInt32_NoChecks(this byte[] ba, int baOff)
        {
            return ba[baOff] << 24
                   | ba[baOff + 1] << 16
                   | ba[baOff + 2] << 8
                   | ba[baOff + 3];
        }

        /// <summary>
        ///     Unpacks signed integers in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void BigEndianToInt32(this byte[] ba, int baOff, Int32[] ia, int iaOff, int count)
        {
            ValidateArguments32(ba, baOff, ia, 0, count);
            BigEndianToInt32_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks signed integers in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void BigEndianToInt32_NoChecks(this byte[] ba, int baOff, Int32[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int32);
            if (count >= umLimit) {
                BigEndianToInt32_Unsafe(ba, baOff, ia, iaOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.BigEndianToInt32_NoChecks(baOff + (i * sizeof(Int32)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

        [Conditional("INCLUDE_UNSAFE")]
        private static unsafe void BigEndianToInt32_Unsafe(byte[] ba, int baOff, Int32[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int32* iaPtr = &ia[iaOff]) {
                    for (int i = 0; i < integerCount; i++) {
                        UInt32 val =
                            *(baPtr + (i * sizeof(Int32)));
                        *(iaPtr + i) = (Int32)(
                            (val << 24) |
                            ((val & 0x00ff0000) << 16) |
                            ((val & 0x0000ff00) << 8) |
                            (val & 0x000000ff));
                    }
                }
            }
        }

        #endregion

        #region Big-endian Int64

        /// <summary>
        ///     Packs an signed integer <paramref name="n" /> into a byte array
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
        ///     Packs an signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int64 n, byte[] ba)
        {
            ValidateArguments64(ba);
            n.ToBigEndian_NoChecks(ba);
        }

        /// <summary>
        ///     Packs an signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian_NoChecks(this Int64 n, byte[] ba)
        {
            ((Int32)(n >> 32)).ToBigEndian(ba, 0);
            ((Int32)(n)).ToBigEndian(ba, 4);
        }

        /// <summary>
        ///     Packs an signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int64 n, byte[] ba, int baOff)
        {
            ValidateArguments64(ba);
            n.ToBigEndian_NoChecks(ba, baOff);
        }

        /// <summary>
        ///     Packs an signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian_NoChecks(this Int64 n, byte[] ba, int baOff)
        {
            ((Int32)(n >> 32)).ToBigEndian(ba, baOff);
            ((Int32)(n)).ToBigEndian(ba, baOff + 4);
        }

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>64-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 BigEndianToInt64(this byte[] ba)
        {
            ValidateArguments64(ba);
            return ba.BigEndianToInt64_NoChecks();
        }

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>64-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 BigEndianToInt64_NoChecks(this byte[] ba)
        {
            Int32 hi = ba.BigEndianToInt32();
            Int32 lo = ba.BigEndianToInt32(4);
            return ((Int64)hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>64-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 BigEndianToInt64(this byte[] ba, int baOff)
        {
            ValidateArguments64(ba, baOff);
            return ba.BigEndianToInt64_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>64-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 BigEndianToInt64_NoChecks(this byte[] ba, int baOff)
        {
            Int32 hi = ba.BigEndianToInt32(baOff);
            Int32 lo = ba.BigEndianToInt32(baOff + 4);
            return ((Int64)hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks signed integers in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void BigEndianToInt64(this byte[] ba, int baOff, Int64[] ia, int iaOff, int count)
        {
            ValidateArguments64(ba, baOff, ia, 0, count);
            BigEndianToInt64_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks signed integers in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void BigEndianToInt64_NoChecks(this byte[] ba, int baOff, Int64[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int64);
            if (count >= umLimit) {
                BigEndianToInt64_Unsafe(ba, baOff, ia, iaOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.BigEndianToInt64_NoChecks(baOff + (i * sizeof(Int64)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

        private static unsafe void BigEndianToInt64_Unsafe(byte[] ba, int baOff, Int64[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int64* iaPtr = &ia[iaOff]) {
                    for (int i = 0; i < integerCount; i++) {
                        int iterOff = i * sizeof(UInt64);
                        UInt32 hi =
                            *(baPtr + (i * iterOff));
                        UInt32 lo =
                            *(baPtr + (i * iterOff) + sizeof(UInt32));
                        hi = (hi << 24) |
                            ((hi & 0x00ff0000) << 16) |
                            ((hi & 0x0000ff00) << 8) |
                            (hi & 0x000000ff);
                        lo = (lo << 24) |
                            ((lo & 0x00ff0000) << 16) |
                            ((lo & 0x0000ff00) << 8) |
                            (lo & 0x000000ff);
                        *(iaPtr + i) = (Int64)(((UInt64)hi << 32) | lo);
                    }
                }
            }
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
            n.ToBigEndian_NoChecks(ba);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian_NoChecks(this UInt16 n, byte[] ba)
        {
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
            ValidateArguments16(ba);
            n.ToBigEndian_NoChecks(ba, baOff);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian_NoChecks(this UInt16 n, byte[] ba, int baOff)
        {
            ba[baOff] = (byte)(n >> 8);
            ba[baOff + 1] = (byte)(n);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>16-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 BigEndianToUInt16(this byte[] ba)
        {
            ValidateArguments16(ba);
            return ba.BigEndianToUInt16_NoChecks();
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>16-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 BigEndianToUInt16_NoChecks(this byte[] ba)
        {
            uint n = (uint)ba[0] << 8
                     | (uint)ba[1];
            return (ushort)n;
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>16-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 BigEndianToUInt16(this byte[] ba, int baOff)
        {
            ValidateArguments16(ba, baOff);
            return ba.BigEndianToUInt16_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>16-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 BigEndianToUInt16_NoChecks(this byte[] ba, int baOff)
        {
            uint n = (uint)ba[baOff] << 8
                     | (uint)ba[baOff + 1];
            return (ushort)n;
        }

        /// <summary>
        ///     Unpacks unsigned integers in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void BigEndianToUInt16(this byte[] ba, int baOff, UInt16[] ia, int iaOff, int count)
        {
            ValidateArguments16(ba, baOff, ia, 0, count);
            BigEndianToUInt16_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks unsigned integers in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void BigEndianToUInt16_NoChecks(this byte[] ba, int baOff, UInt16[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt16);
            if (count >= umLimit) {
                BigEndianToUInt16_Unsafe(ba, baOff, ia, iaOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.BigEndianToUInt16_NoChecks(baOff + (i * sizeof(UInt16)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

        [Conditional("INCLUDE_UNSAFE")]
        private static unsafe void BigEndianToUInt16_Unsafe(byte[] ba, int baOff, UInt16[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt16* iaPtr = &ia[iaOff]) {
                    for (int i = 0; i < integerCount; i++) {
                        UInt16 val =
                            *(baPtr + (i * sizeof(UInt16)));
                        *(iaPtr + i) = (UInt16)(((val & 0x0000ff00) << 8) |
                            (val & 0x000000ff));
                    }
                }
            }
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
            n.ToBigEndian_NoChecks(ba);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian_NoChecks(this UInt32 n, byte[] ba)
        {
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
            ValidateArguments32(ba);
            n.ToBigEndian_NoChecks(ba, baOff);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian_NoChecks(this UInt32 n, byte[] ba, int baOff)
        {
            ba[baOff + 0] = (byte)(n >> 24);
            ba[baOff + 1] = (byte)(n >> 16);
            ba[baOff + 2] = (byte)(n >> 8);
            ba[baOff + 3] = (byte)(n);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>32-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 BigEndianToUInt32(this byte[] ba)
        {
            ValidateArguments32(ba);
            return ba.BigEndianToUInt32_NoChecks();
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>32-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 BigEndianToUInt32_NoChecks(this byte[] ba)
        {
            return (UInt32)ba[0] << 24
                   | (UInt32)ba[1] << 16
                   | (UInt32)ba[2] << 8
                   | ba[3];
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>32-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 BigEndianToUInt32(this byte[] ba, int baOff)
        {
            ValidateArguments32(ba, baOff);
            return ba.BigEndianToUInt32_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>32-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 BigEndianToUInt32_NoChecks(this byte[] ba, int baOff)
        {
            return (UInt32)ba[baOff] << 24
                   | (UInt32)ba[baOff + 1] << 16
                   | (UInt32)ba[baOff + 2] << 8
                   | ba[baOff + 3];
        }

        /// <summary>
        ///     Unpacks unsigned integers in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void BigEndianToUInt32(this byte[] ba, int baOff, UInt32[] ia, int iaOff, int count)
        {
            ValidateArguments32(ba, baOff, ia, 0, count);
            BigEndianToUInt32_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks unsigned integers in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void BigEndianToUInt32_NoChecks(this byte[] ba, int baOff, UInt32[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt32);
            if (count >= umLimit) {
                BigEndianToUInt32_Unsafe(ba, baOff, ia, iaOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.BigEndianToUInt32_NoChecks(baOff + (i * sizeof(UInt32)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

        [Conditional("INCLUDE_UNSAFE")]
        private static unsafe void BigEndianToUInt32_Unsafe(byte[] ba, int baOff, UInt32[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt32* iaPtr = &ia[iaOff]) {
                    for (int i = 0; i < integerCount; i++) {
                        UInt32 val = 
                            *(baPtr + (i * sizeof(UInt32)));
                        *(iaPtr + i) =
                            (val << 24) |
                            ((val & 0x00ff0000) << 16) |
                            ((val & 0x0000ff00) << 8) |
                            (val & 0x000000ff);
                    }
                }
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
            n.ToBigEndian_NoChecks(ba);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian_NoChecks(this UInt64 n, byte[] ba)
        {
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
            ValidateArguments64(ba);
            n.ToBigEndian_NoChecks(ba, baOff);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian_NoChecks(this UInt64 n, byte[] ba, int baOff)
        {
            ((UInt32)(n >> 32)).ToBigEndian(ba, baOff);
            ((UInt32)(n)).ToBigEndian(ba, baOff + 4);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 BigEndianToUInt64(this byte[] ba)
        {
            ValidateArguments64(ba);
            return ba.BigEndianToUInt64_NoChecks();
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 BigEndianToUInt64_NoChecks(this byte[] ba)
        {
            UInt32 hi = ba.BigEndianToUInt32();
            UInt32 lo = ba.BigEndianToUInt32(4);
            return ((UInt64)hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 BigEndianToUInt64(this byte[] ba, int baOff)
        {
            ValidateArguments64(ba, baOff);
            return ba.BigEndianToUInt64_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 BigEndianToUInt64_NoChecks(this byte[] ba, int baOff)
        {
            UInt32 hi = ba.BigEndianToUInt32(baOff);
            UInt32 lo = ba.BigEndianToUInt32(baOff + 4);
            return ((UInt64)hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks unsigned integers in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void BigEndianToUInt64(this byte[] ba, int baOff, UInt64[] ia, int iaOff, int count)
        {
            ValidateArguments64(ba, baOff, ia, 0, count);
            BigEndianToUInt64_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks unsigned integers in big-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void BigEndianToUInt64_NoChecks(this byte[] ba, int baOff, UInt64[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt64);
            if (count >= umLimit) {
                BigEndianToUInt64_Unsafe(ba, baOff, ia, iaOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.BigEndianToUInt64_NoChecks(baOff + (i * sizeof(UInt64)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

        [Conditional("INCLUDE_UNSAFE")]
        private static unsafe void BigEndianToUInt64_Unsafe(byte[] ba, int baOff, UInt64[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt64* iaPtr = &ia[iaOff]) {
                    for (int i = 0; i < integerCount; i++) {
                        int iterOff = i * sizeof(UInt64);
                        UInt32 hi =
                            *(baPtr + (i * iterOff));
                        UInt32 lo =
                            *(baPtr + (i * iterOff) + sizeof(UInt32));
                        hi = (hi << 24) |
                            ((hi & 0x00ff0000) << 16) |
                            ((hi & 0x0000ff00) << 8) |
                            (hi & 0x000000ff);
                        lo = (lo << 24) |
                            ((lo & 0x00ff0000) << 16) |
                            ((lo & 0x0000ff00) << 8) |
                            (lo & 0x000000ff);
                        *(iaPtr + i) = ((UInt64)hi << 32) | lo;
                    }
                }
            }
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
            n.ToLittleEndian_NoChecks(ba);
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian_NoChecks(this Int32 n, byte[] ba)
        {
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
            ValidateArguments32(ba);
            n.ToLittleEndian_NoChecks(ba, baOff);
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian_NoChecks(this Int32 n, byte[] ba, int baOff)
        {
            ba[baOff + 0] = (byte)(n);
            ba[baOff + 1] = (byte)(n >> 8);
            ba[baOff + 2] = (byte)(n >> 16);
            ba[baOff + 3] = (byte)(n >> 24);
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LittleEndianToInt32(this byte[] ba)
        {
            ValidateArguments32(ba);
            return ba.LittleEndianToInt32_NoChecks();
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LittleEndianToInt32_NoChecks(this byte[] ba)
        {
            return ba[0]
                   | ba[1] << 8
                   | ba[2] << 16
                   | ba[3] << 24;
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LittleEndianToInt32(this byte[] ba, int baOff)
        {
            ValidateArguments32(ba, baOff);
            return ba.LittleEndianToInt32_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>32-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LittleEndianToInt32_NoChecks(this byte[] ba, int baOff)
        {
            return ba[baOff]
                   | ba[baOff + 1] << 8
                   | ba[baOff + 2] << 16
                   | ba[baOff + 3] << 24;
        }

        /// <summary>
        ///     Unpacks signed integers in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void LittleEndianToInt32(this byte[] ba, int baOff, Int32[] ia, int iaOff, int count)
        {
            ValidateArguments32(ba, baOff, ia, 0, count);
            LittleEndianToInt32_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks signed integers in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void LittleEndianToInt32_NoChecks(this byte[] ba, int baOff, Int32[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int32);
            if (count >= umLimit) {
                LittleEndianToInt32_Unsafe(ba, baOff, ia, iaOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.LittleEndianToInt32_NoChecks(baOff + (i * sizeof(Int32)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

        [Conditional("INCLUDE_UNSAFE")]
        private static unsafe void LittleEndianToInt32_Unsafe(byte[] ba, int baOff, Int32[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = ba) {
                fixed (Int32* outPtr = &ia[iaOff]) {
                    var inPtr = (Int32*)(baPtr + baOff);
                    for (int i = 0; i < integerCount; i++) {
                        outPtr[i] = inPtr[i];
                    }
                }
            }
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
            n.ToLittleEndian_NoChecks(ba);
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian_NoChecks(this Int64 n, byte[] ba)
        {
            ((Int32)n).ToLittleEndian_NoChecks(ba, 0);
            ((Int32)(n >> 32)).ToLittleEndian_NoChecks(ba, 4);
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
            ValidateArguments64(ba);
            n.ToLittleEndian_NoChecks(ba, baOff);
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian_NoChecks(this Int64 n, byte[] ba, int baOff)
        {
            ((Int32)n).ToLittleEndian_NoChecks(ba, baOff);
            ((Int32)(n >> 32)).ToLittleEndian_NoChecks(ba, baOff + 4);
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
            return ba.LittleEndianToInt64_NoChecks();
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 LittleEndianToInt64_NoChecks(this byte[] ba)
        {
            Int32 lo = ba.LittleEndianToInt32_NoChecks();
            Int32 hi = ba.LittleEndianToInt32_NoChecks(4);
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
            return ba.LittleEndianToInt64_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 LittleEndianToInt64_NoChecks(this byte[] ba, int baOff)
        {
            Int32 lo = ba.LittleEndianToInt32_NoChecks(baOff);
            Int32 hi = ba.LittleEndianToInt32_NoChecks(baOff + 4);
            return ((Int64)hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks signed integers in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void LittleEndianToInt64(this byte[] ba, int baOff, long[] ia, int iaOff, int count)
        {
            ValidateArguments64(ba, baOff, ia, 0, count);
            LittleEndianToInt64_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks signed integers in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void LittleEndianToInt64_NoChecks(this byte[] ba, int baOff, long[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int64);
            if (count >= umLimit) {
                LittleEndianToInt64_Unsafe(ba, baOff, ia, iaOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.LittleEndianToInt64_NoChecks(baOff + (i * sizeof(Int64)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

        [Conditional("INCLUDE_UNSAFE")]
        private static unsafe void LittleEndianToInt64_Unsafe(byte[] ba, int baOff, Int64[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = ba) {
                fixed (Int64* outPtr = &ia[iaOff]) {
                    var inPtr = (Int64*)(baPtr + baOff);
                    for (int i = 0; i < integerCount; i++) {
                        outPtr[i] = inPtr[i];
                    }
                }
            }
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
            n.ToLittleEndian_NoChecks(ba);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian_NoChecks(this UInt16 n, byte[] ba)
        {
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
        public static void ToLittleEndian(this UInt16 n, byte[] ba, int baOff)
        {
            ValidateArguments16(ba, baOff);
            n.ToLittleEndian_NoChecks(ba, baOff);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ToLittleEndian_NoChecks(this UInt16 n, byte[] ba, int baOff)
        {
            ba[baOff] = (byte)(n);
            ba[baOff + 1] = (byte)(n >> 8);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>16-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 LittleEndianToUInt16(this byte[] ba)
        {
            ValidateArguments16(ba);
            return ba.LittleEndianToUInt16_NoChecks();
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>16-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static UInt16 LittleEndianToUInt16_NoChecks(this byte[] ba)
        {
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
        public static UInt16 LittleEndianToUInt16(this byte[] ba, int baOff)
        {
            ValidateArguments16(ba, baOff);
            return ba.LittleEndianToUInt16_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>16-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static UInt16 LittleEndianToUInt16_NoChecks(this byte[] ba, int baOff)
        {
            UInt32 n = (UInt32)ba[baOff]
                     | (UInt32)ba[baOff + 1] << 8;
            return (UInt16)n;
        }

        /// <summary>
        ///     Unpacks unsigned integers in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void LittleEndianToUInt16(this byte[] ba, int baOff, ushort[] ia, int iaOff, int count)
        {
            ValidateArguments16(ba, baOff, ia, 0, count);
            LittleEndianToUInt16_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks unsigned integers in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void LittleEndianToUInt16_NoChecks(this byte[] ba, int baOff, ushort[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt16);
            if (count >= umLimit) {
                LittleEndianToUInt16_Unsafe(ba, baOff, ia, iaOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.LittleEndianToUInt16_NoChecks(baOff + (i * sizeof(UInt16)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

        [Conditional("INCLUDE_UNSAFE")]
        private static unsafe void LittleEndianToUInt16_Unsafe(byte[] ba, int baOff, UInt16[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = ba) {
                fixed (UInt16* outPtr = &ia[iaOff]) {
                    var inPtr = (UInt16*)(baPtr + baOff);
                    for (int i = 0; i < integerCount; i++) {
                        outPtr[i] = inPtr[i];
                    }
                }
            }
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
            n.ToLittleEndian_NoChecks(bs);
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
            n.ToLittleEndian_NoChecks(ba);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian_NoChecks(this UInt32 n, byte[] ba)
        {
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
            n.ToLittleEndian_NoChecks(ba, baOff);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian_NoChecks(this UInt32 n, byte[] ba, int baOff)
        {
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
            return ba.LittleEndianToUInt32_NoChecks();
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>32-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 LittleEndianToUInt32_NoChecks(this byte[] ba)
        {
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
            return ba.LittleEndianToUInt32_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>32-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 LittleEndianToUInt32_NoChecks(this byte[] ba, int baOff)
        {
            return ba[baOff]
                   | (UInt32)ba[baOff + 1] << 8
                   | (UInt32)ba[baOff + 2] << 16
                   | (UInt32)ba[baOff + 3] << 24;
        }

        /// <summary>
        ///     Unpacks unsigned integers in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void LittleEndianToUInt32(this byte[] ba, int baOff, uint[] ia, int iaOff, int count)
        {
            ValidateArguments32(ba, baOff, ia, 0, count);
            LittleEndianToUInt32_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks unsigned integers in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void LittleEndianToUInt32_NoChecks(this byte[] ba, int baOff, uint[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt32);
            if (count >= umLimit) {
                LittleEndianToUInt32_Unsafe(ba, baOff, ia, iaOff, count);
            } else {  
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.LittleEndianToUInt32_NoChecks(baOff + (i * sizeof(UInt32)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

        [Conditional("INCLUDE_UNSAFE")]
        private static unsafe void LittleEndianToUInt32_Unsafe(byte[] ba, int baOff, UInt32[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = ba) {
                fixed (UInt32* outPtr = &ia[iaOff]) {
                    var inPtr = (UInt32*)(baPtr + baOff);
                    for (int i = 0; i < integerCount; i++) {
                        outPtr[i] = inPtr[i];
                    }
                }
            }
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
            n.ToLittleEndian_NoChecks(bs);
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
            n.ToLittleEndian_NoChecks(ba);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian_NoChecks(this UInt64 n, byte[] ba)
        {
            ((UInt32)n).ToLittleEndian_NoChecks(ba, 0);
            ((UInt32)(n >> 32)).ToLittleEndian_NoChecks(ba, 4);
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
            ValidateArguments64(ba);
            n.ToLittleEndian_NoChecks(ba, baOff);
        }

        /// <summary>
        ///     Packs an unsigned integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian_NoChecks(this UInt64 n, byte[] ba, int baOff)
        {
            ((UInt32)n).ToLittleEndian_NoChecks(ba, baOff);
            ((UInt32)(n >> 32)).ToLittleEndian_NoChecks(ba, baOff + 4);
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
            return ba.LittleEndianToUInt64_NoChecks();
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 LittleEndianToUInt64_NoChecks(this byte[] ba)
        {
            UInt32 lo = ba.LittleEndianToUInt32_NoChecks();
            UInt32 hi = ba.LittleEndianToUInt32_NoChecks(4);
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
            return ba.LittleEndianToUInt64_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks an unsigned integer in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 LittleEndianToUInt64_NoChecks(this byte[] ba, int baOff)
        {
            UInt32 lo = ba.LittleEndianToUInt32_NoChecks(baOff);
            UInt32 hi = ba.LittleEndianToUInt32_NoChecks(baOff + 4);
            return ((UInt64)hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks unsigned integers in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void LittleEndianToUInt64(this byte[] ba, int baOff, ulong[] ia, int iaOff, int count)
        {
            ValidateArguments64(ba, baOff, ia, 0, count);
            LittleEndianToUInt64_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks unsigned integers in little-endian format from <paramref name="ba" />. 
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia"/>.</param>
        public static void LittleEndianToUInt64_NoChecks(this byte[] ba, int baOff, ulong[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt64);
            if (count >= umLimit) {
                LittleEndianToUInt64_Unsafe(ba, baOff, ia, iaOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.LittleEndianToUInt64_NoChecks(baOff + (i * sizeof(UInt64)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

        [Conditional("INCLUDE_UNSAFE")]
        private static unsafe void LittleEndianToUInt64_Unsafe(byte[] ba, int baOff, UInt64[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = ba) {
                fixed (UInt64* outPtr = &ia[iaOff]) {
                    var inPtr = (UInt64*)(baPtr + baOff);
                    for (int i = 0; i < integerCount; i++) {
                        outPtr[i] = inPtr[i];
                    }
                }
            }
        }

        #endregion

        #region Argument validation utility methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments16(byte[] ba, int baOff = 0)
        {
            ValidateArgumentsImpl<ushort>(ba, baOff, 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments16(byte[] ba, int baOff, short[] dst, int dstOff, int count)
        {
            ValidateArgumentsImpl<short>(ba, baOff, dst, dstOff, count, 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments16(byte[] ba, int baOff, ushort[] dst, int dstOff, int count)
        {
            ValidateArgumentsImpl<ushort>(ba, baOff, dst, dstOff, count, 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments32(byte[] ba, int baOff = 0)
        {
            ValidateArgumentsImpl<uint>(ba, baOff, 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments32(byte[] ba, int baOff, int[] dst, int dstOff, int count)
        {
            ValidateArgumentsImpl<int>(ba, baOff, dst, dstOff, count, 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments32(byte[] ba, int baOff, uint[] dst, int dstOff, int count)
        {
            ValidateArgumentsImpl<uint>(ba, baOff, dst, dstOff, count, 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments64(byte[] ba, int baOff = 0)
        {
            ValidateArgumentsImpl<ulong>(ba, baOff, 8);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments64(byte[] ba, int baOff, long[] dst, int dstOff, int count)
        {
            ValidateArgumentsImpl<long>(ba, baOff, dst, dstOff, count, 8);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments64(byte[] ba, int baOff, ulong[] dst, int dstOff, int count)
        {
            ValidateArgumentsImpl<ulong>(ba, baOff, dst, dstOff, count, 8);
        }

        /// <summary>
        ///     Validate arguments/parameters to prevent unexpected exceptions. 
        ///     Throws well-defined exceptions for different types of invalid arguments(s). 
        /// </summary>
        /// <param name="ba">Byte array that output will be produced from.</param>
        /// <param name="baOff">Offset in <paramref name="ba"/> to read data from.</param>
        /// <param name="outputSizeRatio">
        ///     How many bytes are required to produce one unit of the output type <typeparamref name="T"/> 
        ///     (<paramref name="ba"/> : <typeparamref name="T"/>). Default is null - size will be determined algorithmically. 
        ///     It is recommended to supply the size ratio for better performance, instead of relying on this.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="ba"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Length of <paramref name="ba"/> less than 0, 
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <list type="bullet">
        ///         <item><description>
        ///             <paramref name="baOff"/> less than 0.
        ///         </description></item>
        ///         <item><description>
        ///             <paramref name="baOff"/> more than length of <paramref name="ba"/> - sizeof(<typeparamref name="T"/>).
        ///         </description></item>
        ///     </list>
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArgumentsImpl<T>(byte[] ba, int baOff, int? outputSizeRatio = null) where T : struct
        {
            if (ba == null) {
                throw new ArgumentNullException("ba");
            }
            if (ba.Length < 0) {
                throw new ArgumentException("ba.Length < 0", "ba");
            }
            if (baOff < 0) {
                throw new ArgumentOutOfRangeException("baOff", "baOff < 0");
            }
            if (outputSizeRatio != null && outputSizeRatio < 1) {
                throw new ArgumentOutOfRangeException("outputSizeRatio", "outputSizeRatio < 1");
            }
            if (baOff > ba.Length - (outputSizeRatio ?? Shared.SizeOf<T>())) {
                throw new ArgumentOutOfRangeException("baOff", "baOff > ba.Length - sizeof(T)");
            }
        }

        /// <summary>
        ///     Validate arguments/parameters to prevent unexpected exceptions. 
        ///     Throws well-defined exceptions for different types of invalid arguments(s). 
        /// </summary>
        /// <param name="ba">Byte array that output will be produced from.</param>
        /// <param name="baOff">Offset in <paramref name="ba"/> to read data from.</param>
        /// <param name="dst">Destination array of type <typeparamref name="T"/> for output.</param>
        /// <param name="dstOff">Offset in <paramref name="dst"/> to write data to.</param>
        /// <param name="count">Number of items (of the size of <typeparamref name="T"/>) to process.</param>
        /// <param name="outputSizeRatio">
        ///     How many bytes are required to produce one unit in <paramref name="dst"/> of the output type 
        ///     <typeparamref name="T"/> (<paramref name="ba"/> : <paramref name="dst"/>). Default is null - size will be 
        ///     determined algorithmically. It is recommended to supply the size ratio for better performance, 
        ///     instead of relying on this.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="ba"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Length of <paramref name="ba"/> less than 0. 
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <list type="bullet">
        ///         <item><description>
        ///             <paramref name="baOff"/> less than 0.
        ///         </description></item>
        ///         <item><description>
        ///             <paramref name="baOff"/> more than length of <paramref name="ba"/> - (sizeof(<typeparamref name="T"/>) * <paramref name="count"/>).
        ///         </description></item>
        ///         <item><description>
        ///             <paramref name="count"/> less than 1.
        ///         </description></item>
        ///         <item><description>
        ///             <paramref name="count"/> less than length of <paramref name="ba"/>.
        ///         </description></item>
        ///     </list>
        /// </exception>
        internal static void ValidateArgumentsImpl<T>(byte[] ba, int baOff, T[] dst, int dstOff, int count, int? outputSizeRatio = null)
            where T : struct
        {
            if (ba == null) {
                throw new ArgumentNullException("ba");
            } else if (ba.Length < 0) {
                throw new ArgumentException("ba.Length < 0", "ba");
            }

            if (dst == null) {
                throw new ArgumentNullException("dst");
            } else if (dst.Length < 0) {
                throw new ArgumentException("dst.Length < 0", "dst");
            }

            if (baOff < 0) {
                throw new ArgumentOutOfRangeException("baOff", "baOff < 0");
            }
            if (dstOff < 0) {
                throw new ArgumentOutOfRangeException("dstOff", "dstOff < 0");
            }

            int srcLength = ba.Length;
            int dstLength = dst.Length;

            if (outputSizeRatio != null && outputSizeRatio < 1) {
                throw new ArgumentOutOfRangeException("outputSizeRatio", "outputSizeRatio < 1");
            }
            int dstToBaSizeRatio = Shared.SizeOf<T>();
            if (count < 0 || (count * dstToBaSizeRatio) > srcLength || count > dstLength) {
                throw new ArgumentOutOfRangeException("count", "count < 0, and/or (count * sizeof(T)) > src.Length and/or count > dst.Length");
            }
            if (baOff > 0 && baOff + (count * dstToBaSizeRatio) > srcLength) {
                if (baOff >= srcLength) {
                    throw new ArgumentOutOfRangeException("baOff", "baOff >= ba.Length");
                }
                // More common case
                throw new ArgumentException("baOff + (count * sizeof(T)) > ba.Length", "baOff");
            }

            if (dstOff > 0 && dstOff + count > dstLength) {
                if (dstOff >= dstLength) {
                    throw new ArgumentOutOfRangeException("dstOff", "dstOff >= dst.Length");
                }
                // More common case
                throw new ArgumentException("dstOff + count > dst.Length", "dstOff");
            }
        }

        #endregion
    }
}
