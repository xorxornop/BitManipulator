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

namespace BitManipulator
{
    /// <summary>
    ///     Extension methods for packing/unpacking integral numbers into/out of byte arrays, in big-or-little endian formats.
    /// </summary>
    public static class EndiannessExtensions
    {
        #region Big-endian Int16

        /// <summary>
        ///     Packs an signed integer <paramref name="n" /> into a byte array
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBigEndian(this Int16 n)
        {
            var bs = new byte[sizeof(Int16)];
            n.ToBigEndian_NoChecks(bs);
            return bs;
        }

        /// <summary>
        ///     Packs an signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int16 n, byte[] ba)
        {
            ValidateArguments16(ba);
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
        public static void ToBigEndian_NoChecks(this Int16 n, byte[] ba)
        {
            ba[0] = (byte) (n >> 8);
            ba[1] = (byte) (n);
        }

        /// <summary>
        ///     Packs an signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int16 n, byte[] ba, int baOff)
        {
            ValidateArguments16(ba);
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
        public static void ToBigEndian_NoChecks(this Int16 n, byte[] ba, int baOff)
        {
            ba[baOff + 0] = (byte) (n >> 8);
            ba[baOff + 1] = (byte) (n);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int16[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(Int16));
            ia.ToBigEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToBigEndian_NoChecks(this Int16[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int16);
            if (count >= umLimit) {
                ToBigEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToBigEndian_NoChecks(ba, baOff + (i * sizeof(Int16)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToBigEndian_Unsafe(Int16[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int16* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder16((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(Int16));
                    }
                }
            }
        }
#endif

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>16-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 BigEndianToInt16(this byte[] ba)
        {
            ValidateArguments16(ba);
            return ba.BigEndianToInt16_NoChecks();
        }

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>16-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 BigEndianToInt16_NoChecks(this byte[] ba)
        {
            UInt32 n = (UInt32) ba[0] << 8
                       | ba[1];
            return (Int16) n;
        }

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>16-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 BigEndianToInt16(this byte[] ba, int baOff)
        {
            ValidateArguments16(ba, baOff);
            return ba.BigEndianToInt16_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks an signed integer in big-endian format from <paramref name="ba" />.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>16-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 BigEndianToInt16_NoChecks(this byte[] ba, int baOff)
        {
            UInt32 n = (UInt32) ba[baOff + 0] << 8
                       | ba[baOff + 1];
            return (Int16) n;
        }

        /// <summary>
        ///     Unpacks signed integers in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void BigEndianToInt16(this byte[] ba, int baOff, Int16[] ia, int iaOff, int count)
        {
            ValidateArguments16(ba, baOff, ia, iaOff, count);
            BigEndianToInt16_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks signed integers in big-endian format from <paramref name="ba" />.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void BigEndianToInt16_NoChecks(this byte[] ba, int baOff, Int16[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int16);
            if (count >= umLimit) {
                BigEndianToInt16_Unsafe(ba, baOff, ia, iaOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.BigEndianToInt16_NoChecks(baOff + (i * sizeof(Int16)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void BigEndianToInt16_Unsafe(byte[] ba, int baOff, Int16[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int16* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder16(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(Int16));
                    }
                }
            }
        }
#endif

        #endregion

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
            n.ToBigEndian_NoChecks(bs);
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
            ba[0] = (byte) (n >> 24);
            ba[1] = (byte) (n >> 16);
            ba[2] = (byte) (n >> 8);
            ba[3] = (byte) (n);
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
            ba[baOff + 0] = (byte) (n >> 24);
            ba[baOff + 1] = (byte) (n >> 16);
            ba[baOff + 2] = (byte) (n >> 8);
            ba[baOff + 3] = (byte) (n);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int32[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(Int32));
            ia.ToBigEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToBigEndian_NoChecks(this Int32[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int32);
            if (count >= umLimit) {
                ToBigEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToBigEndian_NoChecks(ba, baOff + (i * sizeof(Int32)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToBigEndian_Unsafe(Int32[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int32* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder32((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(Int32));
                    }
                }
            }
        }
#endif

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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void BigEndianToInt32(this byte[] ba, int baOff, Int32[] ia, int iaOff, int count)
        {
            ValidateArguments32(ba, baOff, ia, iaOff, count);
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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
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

#if INCLUDE_UNSAFE
        private static unsafe void BigEndianToInt32_Unsafe(byte[] ba, int baOff, Int32[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int32* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder32(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(Int32));
                    }
                }
            }
        }
#endif

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
            n.ToBigEndian_NoChecks(bs);
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
            ((Int32) (n >> 32)).ToBigEndian_NoChecks(ba, 0);
            ((Int32)(n)).ToBigEndian_NoChecks(ba, 4);
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
            ((Int32)(n >> 32)).ToBigEndian_NoChecks(ba, baOff);
            ((Int32)(n)).ToBigEndian_NoChecks(ba, baOff + 4);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this Int64[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(Int64));
            ia.ToBigEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToBigEndian_NoChecks(this Int64[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int64);
            if (count >= umLimit) {
                ToBigEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToBigEndian_NoChecks(ba, baOff + (i * sizeof(Int64)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToBigEndian_Unsafe(Int64[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int64* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder64((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(Int64));
                    }
                }
            }
        }
#endif

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
            Int32 hi = ba.BigEndianToInt32_NoChecks();
            Int32 lo = ba.BigEndianToInt32_NoChecks(4);
            return ((Int64) hi << 32) | lo;
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
            Int32 hi = ba.BigEndianToInt32_NoChecks(baOff);
            Int32 lo = ba.BigEndianToInt32_NoChecks(baOff + 4);
            return ((Int64) hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks signed integers in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void BigEndianToInt64(this byte[] ba, int baOff, Int64[] ia, int iaOff, int count)
        {
            ValidateArguments64(ba, baOff, ia, iaOff, count);
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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
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

#if INCLUDE_UNSAFE
        private static unsafe void BigEndianToInt64_Unsafe(byte[] ba, int baOff, Int64[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int64* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder64(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(Int64));
                    }
                }
            }
        }
#endif

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
            n.ToBigEndian_NoChecks(bs);
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
            ba[0] = (byte) (n >> 8);
            ba[1] = (byte) (n);
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
            ba[baOff] = (byte) (n >> 8);
            ba[baOff + 1] = (byte) (n);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this UInt16[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(UInt16));
            ia.ToBigEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToBigEndian_NoChecks(this UInt16[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt16);
            if (count >= umLimit) {
                ToBigEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToBigEndian_NoChecks(ba, baOff + (i * sizeof(UInt16)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToBigEndian_Unsafe(UInt16[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt16* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder16((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(UInt16));
                    }
                }
            }
        }
#endif

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
            uint n = (uint) ba[0] << 8
                     | ba[1];
            return (ushort) n;
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
            uint n = (uint) ba[baOff] << 8
                     | ba[baOff + 1];
            return (ushort) n;
        }

        /// <summary>
        ///     Unpacks unsigned integers in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void BigEndianToUInt16(this byte[] ba, int baOff, UInt16[] ia, int iaOff, int count)
        {
            ValidateArguments16(ba, baOff, ia, iaOff, count);
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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
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

#if INCLUDE_UNSAFE
        private static unsafe void BigEndianToUInt16_Unsafe(byte[] ba, int baOff, UInt16[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt16* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder16(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(UInt16));
                    }
                }
            }
        }
#endif

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
            n.ToBigEndian_NoChecks(bs);
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
            ba[0] = (byte) (n >> 24);
            ba[1] = (byte) (n >> 16);
            ba[2] = (byte) (n >> 8);
            ba[3] = (byte) (n);
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
            ba[baOff + 0] = (byte) (n >> 24);
            ba[baOff + 1] = (byte) (n >> 16);
            ba[baOff + 2] = (byte) (n >> 8);
            ba[baOff + 3] = (byte) (n);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this uint[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(UInt32));
            ia.ToBigEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToBigEndian_NoChecks(this uint[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt32);
            if (count >= umLimit) {
                ToBigEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToBigEndian_NoChecks(ba, baOff + (i * sizeof(UInt32)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToBigEndian_Unsafe(UInt32[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt32* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder32((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(UInt32));
                    }
                }
            }
        }
#endif

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
            return (UInt32) ba[0] << 24
                   | (UInt32) ba[1] << 16
                   | (UInt32) ba[2] << 8
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
            return (UInt32) ba[baOff] << 24
                   | (UInt32) ba[baOff + 1] << 16
                   | (UInt32) ba[baOff + 2] << 8
                   | ba[baOff + 3];
        }

        /// <summary>
        ///     Unpacks unsigned integers in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void BigEndianToUInt32(this byte[] ba, int baOff, UInt32[] ia, int iaOff, int count)
        {
            ValidateArguments32(ba, baOff, ia, iaOff, count);
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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
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

#if INCLUDE_UNSAFE
        private static unsafe void BigEndianToUInt32_Unsafe(byte[] ba, int baOff, UInt32[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt32* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder32(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(UInt32));
                    }
                }
            }
        }
#endif

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
            n.ToBigEndian_NoChecks(bs);
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
            ((UInt32) (n >> 32)).ToBigEndian_NoChecks(ba, 0);
            ((UInt32) (n)).ToBigEndian_NoChecks(ba, 4);
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
            ((UInt32) (n >> 32)).ToBigEndian_NoChecks(ba, baOff);
            ((UInt32) (n)).ToBigEndian_NoChecks(ba, baOff + 4);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBigEndian(this UInt64[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(UInt64));
            ia.ToBigEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in big-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToBigEndian_NoChecks(this UInt64[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt64);
            if (count >= umLimit) {
                ToBigEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToBigEndian_NoChecks(ba, baOff + (i * sizeof(UInt64)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToBigEndian_Unsafe(UInt64[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt64* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder64((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(UInt64));
                    }
                }
            }
        }
#endif

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
            UInt32 hi = ba.BigEndianToUInt32_NoChecks();
            UInt32 lo = ba.BigEndianToUInt32_NoChecks(4);
            return ((UInt64) hi << 32) | lo;
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
            UInt32 hi = ba.BigEndianToUInt32_NoChecks(baOff);
            UInt32 lo = ba.BigEndianToUInt32_NoChecks(baOff + 4);
            return ((UInt64) hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks unsigned integers in big-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void BigEndianToUInt64(this byte[] ba, int baOff, UInt64[] ia, int iaOff, int count)
        {
            ValidateArguments64(ba, baOff, ia, iaOff, count);
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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
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

#if INCLUDE_UNSAFE
        private static unsafe void BigEndianToUInt64_Unsafe(byte[] ba, int baOff, UInt64[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt64* iaPtr = &ia[iaOff]) {
                    if (BitConverter.IsLittleEndian) {
                        SwapOrder64(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(UInt64));
                    }
                }
            }
        }
#endif

        #endregion

        #region Little-endian Int16

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into a byte array
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <returns>Byte array containing packed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToLittleEndian(this Int16 n)
        {
            var bs = new byte[sizeof(Int16)];
            n.ToLittleEndian_NoChecks(bs);
            return bs;
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this Int16 n, byte[] ba)
        {
            ValidateArguments16(ba);
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
        public static void ToLittleEndian_NoChecks(this Int16 n, byte[] ba)
        {
            ba[0] = (byte) (n);
            ba[1] = (byte) (n >> 8);
        }

        /// <summary>
        ///     Packs a signed integer <paramref name="n" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="n">Integer to pack.</param>
        /// <param name="ba">Byte array to pack integer into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this Int16 n, byte[] ba, int baOff)
        {
            ValidateArguments16(ba);
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
        public static void ToLittleEndian_NoChecks(this Int16 n, byte[] ba, int baOff)
        {
            ba[baOff + 0] = (byte) (n);
            ba[baOff + 1] = (byte) (n >> 8);
        }

        /// <summary>
        ///     Packs signed integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this Int16[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(Int16));
            ia.ToLittleEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs signed integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToLittleEndian_NoChecks(this Int16[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int16);
            if (count >= umLimit) {
                ToLittleEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToLittleEndian_NoChecks(ba, baOff + (i * sizeof(Int16)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToLittleEndian_Unsafe(Int16[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int16* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder32((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(Int16));
                    }
                }
            }
        }
#endif

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>16-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 LittleEndianToInt16(this byte[] ba)
        {
            ValidateArguments32(ba);
            return ba.LittleEndianToInt16_NoChecks();
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <returns>16-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 LittleEndianToInt16_NoChecks(this byte[] ba)
        {
            return (Int16) (ba[0]
                            | (ba[1] << 8));
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>16-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 LittleEndianToInt16(this byte[] ba, int baOff)
        {
            ValidateArguments16(ba, baOff);
            return ba.LittleEndianToInt16_NoChecks(baOff);
        }

        /// <summary>
        ///     Unpacks a signed integer in little-endian format from <paramref name="ba" />.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <returns>16-bit signed integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 LittleEndianToInt16_NoChecks(this byte[] ba, int baOff)
        {
            return (Int16) (ba[baOff]
                            | (ba[baOff + 1] << 8));
        }

        /// <summary>
        ///     Unpacks signed integers in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void LittleEndianToInt16(this byte[] ba, int baOff, Int16[] ia, int iaOff, int count)
        {
            ValidateArguments16(ba, baOff, ia, iaOff, count);
            LittleEndianToInt16_NoChecks(ba, baOff, ia, iaOff, count);
        }

        /// <summary>
        ///     Unpacks signed integers in little-endian format from <paramref name="ba" />.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void LittleEndianToInt16_NoChecks(this byte[] ba, int baOff, Int16[] ia, int iaOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int16);
            if (count >= umLimit) {
                LittleEndianToInt16_Unsafe(ba, baOff, ia, iaOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i] = ba.LittleEndianToInt16_NoChecks(baOff + (i * sizeof(Int16)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void LittleEndianToInt16_Unsafe(byte[] ba, int baOff, Int16[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int16* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder16(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(Int16));
                    }
                }
            }
        }
#endif

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
            n.ToLittleEndian_NoChecks(bs);
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
            ba[0] = (byte) (n);
            ba[1] = (byte) (n >> 8);
            ba[2] = (byte) (n >> 16);
            ba[3] = (byte) (n >> 24);
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
            ba[baOff + 0] = (byte) (n);
            ba[baOff + 1] = (byte) (n >> 8);
            ba[baOff + 2] = (byte) (n >> 16);
            ba[baOff + 3] = (byte) (n >> 24);
        }

        /// <summary>
        ///     Packs signed integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this Int32[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(Int32));
            ia.ToLittleEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs signed integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToLittleEndian_NoChecks(this Int32[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int32);
            if (count >= umLimit) {
                ToLittleEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToLittleEndian_NoChecks(ba, baOff + (i * sizeof(Int32)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToLittleEndian_Unsafe(Int32[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int32* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder32((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(Int32));
                    }
                }
            }
        }
#endif

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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void LittleEndianToInt32(this byte[] ba, int baOff, Int32[] ia, int iaOff, int count)
        {
            ValidateArguments32(ba, baOff, ia, iaOff, count);
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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
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

#if INCLUDE_UNSAFE
        private static unsafe void LittleEndianToInt32_Unsafe(byte[] ba, int baOff, Int32[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int32* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder32(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(Int32));
                    }
                }
            }
        }
#endif

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
            n.ToLittleEndian_NoChecks(bs);
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
            ((Int32) n).ToLittleEndian_NoChecks(ba, 0);
            ((Int32) (n >> 32)).ToLittleEndian_NoChecks(ba, 4);
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
            ((Int32) n).ToLittleEndian_NoChecks(ba, baOff);
            ((Int32) (n >> 32)).ToLittleEndian_NoChecks(ba, baOff + 4);
        }

        /// <summary>
        ///     Packs signed integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this Int64[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(Int64));
            ia.ToLittleEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs signed integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToLittleEndian_NoChecks(this Int64[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(Int64);
            if (count >= umLimit) {
                ToLittleEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToLittleEndian_NoChecks(ba, baOff + (i * sizeof(Int64)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToLittleEndian_Unsafe(Int64[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int64* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder64((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(Int64));
                    }
                }
            }
        }
#endif

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
            return ((Int64) hi << 32) | lo;
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
            return ((Int64) hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks signed integers in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void LittleEndianToInt64(this byte[] ba, int baOff, long[] ia, int iaOff, int count)
        {
            ValidateArguments64(ba, baOff, ia, iaOff, count);
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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
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

#if INCLUDE_UNSAFE
        private static unsafe void LittleEndianToInt64_Unsafe(byte[] ba, int baOff, Int64[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (Int64* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder64(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(Int64));
                    }
                }
            }
        }
#endif

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
            ba[0] = (byte) (n);
            ba[1] = (byte) (n >> 8);
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
            ba[baOff] = (byte) (n);
            ba[baOff + 1] = (byte) (n >> 8);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this UInt16[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(UInt16));
            ia.ToLittleEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToLittleEndian_NoChecks(this UInt16[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt16);
            if (count >= umLimit) {
                ToLittleEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToLittleEndian_NoChecks(ba, baOff + (i * sizeof(UInt16)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToLittleEndian_Unsafe(UInt16[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt16* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder16((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(UInt16));
                    }
                }
            }
        }
#endif

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
            UInt32 n = ba[0]
                       | (UInt32) ba[1] << 8;
            return (UInt16) n;
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
            UInt32 n = ba[baOff]
                       | (UInt32) ba[baOff + 1] << 8;
            return (UInt16) n;
        }

        /// <summary>
        ///     Unpacks unsigned integers in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void LittleEndianToUInt16(this byte[] ba, int baOff, ushort[] ia, int iaOff, int count)
        {
            ValidateArguments16(ba, baOff, ia, iaOff, count);
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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
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

#if INCLUDE_UNSAFE
        private static unsafe void LittleEndianToUInt16_Unsafe(byte[] ba, int baOff, UInt16[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt16* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder16(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(UInt16));
                    }
                }
            }
        }
#endif

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
            ba[0] = (byte) (n);
            ba[1] = (byte) (n >> 8);
            ba[2] = (byte) (n >> 16);
            ba[3] = (byte) (n >> 24);
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
            ba[baOff + 0] = (byte) (n);
            ba[baOff + 1] = (byte) (n >> 8);
            ba[baOff + 2] = (byte) (n >> 16);
            ba[baOff + 3] = (byte) (n >> 24);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this uint[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(UInt32));
            ia.ToLittleEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToLittleEndian_NoChecks(this uint[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt32);
            if (count >= umLimit) {
                ToLittleEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToLittleEndian_NoChecks(ba, baOff + (i * sizeof(UInt32)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToLittleEndian_Unsafe(UInt32[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt32* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder32((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(UInt32));
                    }
                }
            }
        }
#endif

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
                   | (UInt32) ba[1] << 8
                   | (UInt32) ba[2] << 16
                   | (UInt32) ba[3] << 24;
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
                   | (UInt32) ba[baOff + 1] << 8
                   | (UInt32) ba[baOff + 2] << 16
                   | (UInt32) ba[baOff + 3] << 24;
        }

        /// <summary>
        ///     Unpacks unsigned integers in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void LittleEndianToUInt32(this byte[] ba, int baOff, uint[] ia, int iaOff, int count)
        {
            ValidateArguments32(ba, baOff, ia, iaOff, count);
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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
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

#if INCLUDE_UNSAFE
        private static unsafe void LittleEndianToUInt32_Unsafe(byte[] ba, int baOff, UInt32[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt32* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder32(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(UInt32));
                    }
                }
            }
        }
#endif

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
            ((UInt32) n).ToLittleEndian_NoChecks(ba, 0);
            ((UInt32) (n >> 32)).ToLittleEndian_NoChecks(ba, 4);
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
            ((UInt32) n).ToLittleEndian_NoChecks(ba, baOff);
            ((UInt32) (n >> 32)).ToLittleEndian_NoChecks(ba, baOff + 4);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLittleEndian(this UInt64[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
            ValidateArgumentsIntegersToBytesImpl(ia, iaOff, ba, baOff, count, sizeof(UInt64));
            ia.ToLittleEndian_NoChecks(iaOff, ba, baOff, count);
        }

        /// <summary>
        ///     Packs unsigned integers <paramref name="ia" /> into <paramref name="ba" />
        ///     in little-endian format.
        ///     Does not perform any argument validation.
        /// </summary>
        /// <param name="ba">Byte array to pack integers into.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to write to.</param>
        /// <param name="ia">Integer array to pack into <paramref name="ba" />.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to read from.</param>
        /// <param name="count">Number of integers to output into <paramref name="ba" />.</param>
        public static void ToLittleEndian_NoChecks(this UInt64[] ia, int iaOff, byte[] ba, int baOff, int count)
        {
#if INCLUDE_UNSAFE
            const int umLimit = Shared.UnmanagedThreshold / sizeof(UInt64);
            if (count >= umLimit) {
                ToLittleEndian_Unsafe(ia, iaOff, ba, baOff, count);
            } else {
#endif
                for (int i = 0; i < count; i++) {
                    ia[iaOff + i].ToLittleEndian_NoChecks(ba, baOff + (i * sizeof(UInt64)));
                }
#if INCLUDE_UNSAFE
            }
#endif
        }

#if INCLUDE_UNSAFE
        private static unsafe void ToLittleEndian_Unsafe(UInt64[] ia, int iaOff, byte[] ba, int baOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt64* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder64((byte*) iaPtr, baPtr, integerCount);
                    } else {
                        CopyMemory((byte*) iaPtr, baPtr, integerCount * sizeof(UInt64));
                    }
                }
            }
        }
#endif

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
            return ((UInt64) hi << 32) | lo;
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
            return ((UInt64) hi << 32) | lo;
        }

        /// <summary>
        ///     Unpacks unsigned integers in little-endian format from <paramref name="ba" />.
        /// </summary>
        /// <param name="ba">Byte array to unpack integer from.</param>
        /// <param name="baOff">Offset in <paramref name="ba" /> to read from.</param>
        /// <param name="ia">Array to place unpacked integers in.</param>
        /// <param name="iaOff">Offset in <paramref name="ia" /> to write to.</param>
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
        public static void LittleEndianToUInt64(this byte[] ba, int baOff, ulong[] ia, int iaOff, int count)
        {
            ValidateArguments64(ba, baOff, ia, iaOff, count);
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
        /// <param name="count">Number of integers to output into <paramref name="ia" />.</param>
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

#if INCLUDE_UNSAFE
        private static unsafe void LittleEndianToUInt64_Unsafe(byte[] ba, int baOff, UInt64[] ia, int iaOff, int integerCount)
        {
            fixed (byte* baPtr = &ba[baOff]) {
                fixed (UInt64* iaPtr = &ia[iaOff]) {
                    if (!BitConverter.IsLittleEndian) {
                        SwapOrder64(baPtr, (byte*) iaPtr, integerCount);
                    } else {
                        CopyMemory(baPtr, (byte*) iaPtr, integerCount * sizeof(UInt64));
                    }
                }
            }
        }
#endif

        #endregion

        #region Argument validation utility methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments16(byte[] ba, int baOff = 0)
        {
            ValidateArgumentsBytesToIntegersImpl<ushort>(ba, baOff, 16 / 8);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments16(byte[] ba, int baOff, short[] dst, int dstOff, int count)
        {
            ValidateArgumentsBytesToIntegersImpl(ba, baOff, dst, dstOff, count, sizeof(short));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments16(byte[] ba, int baOff, ushort[] dst, int dstOff, int count)
        {
            ValidateArgumentsBytesToIntegersImpl(ba, baOff, dst, dstOff, count, sizeof(ushort));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments32(byte[] ba, int baOff = 0)
        {
            ValidateArgumentsBytesToIntegersImpl<uint>(ba, baOff, 32 / 8);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments32(byte[] ba, int baOff, int[] dst, int dstOff, int count)
        {
            ValidateArgumentsBytesToIntegersImpl(ba, baOff, dst, dstOff, count, sizeof(int));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments32(byte[] ba, int baOff, uint[] dst, int dstOff, int count)
        {
            ValidateArgumentsBytesToIntegersImpl(ba, baOff, dst, dstOff, count, sizeof(uint));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments64(byte[] ba, int baOff = 0)
        {
            ValidateArgumentsBytesToIntegersImpl<ulong>(ba, baOff, 64 / 8);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments64(byte[] ba, int baOff, long[] dst, int dstOff, int count)
        {
            ValidateArgumentsBytesToIntegersImpl(ba, baOff, dst, dstOff, count, sizeof(long));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArguments64(byte[] ba, int baOff, ulong[] dst, int dstOff, int count)
        {
            ValidateArgumentsBytesToIntegersImpl(ba, baOff, dst, dstOff, count, sizeof(ulong));
        }

        /// <summary>
        ///     Validate arguments/parameters to prevent unexpected exceptions.
        ///     Throws well-defined exceptions for different types of invalid arguments(s).
        /// </summary>
        /// <param name="src">Byte array that output will be produced from.</param>
        /// <param name="srcOff">Offset in <paramref name="src" /> to read data from.</param>
        /// <param name="outputSizeRatio">
        ///     How many bytes are required to produce one unit of the output type <typeparamref name="T" />
        ///     (<paramref name="src" /> : <typeparamref name="T" />). Default is null - size will be determined algorithmically.
        ///     It is recommended to supply the size ratio for better performance, instead of relying on this.
        /// </param>
        /// <typeparam name="T">Type of item that data from <paramref name="src" /> will be transformed to.</typeparam>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="src" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Length of <paramref name="src" /> less than 0,
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <list type="bullet">
        ///         <item>
        ///             <description>
        ///                 <paramref name="srcOff" /> less than 0.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <paramref name="srcOff" /> more than length of <paramref name="src" /> - sizeof(
        ///                 <typeparamref name="T" />).
        ///             </description>
        ///         </item>
        ///     </list>
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateArgumentsBytesToIntegersImpl<T>(byte[] src, int srcOff, int? outputSizeRatio = null) where T : struct
        {
            if (src == null) {
                throw new ArgumentNullException("src");
            }
            if (src.Length < 0) {
                throw new ArgumentException("dst.Length < 0", "src");
            }
            if (srcOff < 0) {
                throw new ArgumentOutOfRangeException("srcOff", "dstOff < 0");
            }
            if (outputSizeRatio != null && outputSizeRatio < 1) {
                throw new ArgumentOutOfRangeException("outputSizeRatio", "outputSizeRatio < 1");
            }
            if (srcOff > src.Length - (outputSizeRatio ?? Shared.SizeOf<T>())) {
                throw new ArgumentOutOfRangeException("srcOff", "dstOff > dst.Length - sizeof(T)");
            }
        }

        /// <summary>
        ///     Validate arguments/parameters to prevent unexpected exceptions.
        ///     Throws well-defined exceptions for different types of invalid arguments(s).
        /// </summary>
        /// <param name="src">Byte array that output will be produced from.</param>
        /// <param name="srcOff">Offset in <paramref name="src" /> to read data from.</param>
        /// <param name="dst">Destination array of type <typeparamref name="T" /> for output.</param>
        /// <param name="dstOff">Offset in <paramref name="dst" /> to write data to.</param>
        /// <param name="count">Number of items (of the size of <typeparamref name="T" />) to process.</param>
        /// <param name="outputSizeRatio">
        ///     How many input units (bytes) are required to write one unit of the output type
        ///     <typeparamref name="T" /> (<paramref name="src" /> : <paramref name="dst" />). Default is null - size will be
        ///     determined algorithmically. It is recommended to supply the size ratio for better performance,
        ///     instead of relying on this.
        /// </param>
        /// <typeparam name="T">
        ///     Destination/target type of data in <paramref name="dst" /> that data
        ///     from <paramref name="src" /> will be transformed to and written to.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="src" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Length of <paramref name="src" /> less than 0.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <list type="bullet">
        ///         <item>
        ///             <description>
        ///                 <paramref name="srcOff" /> less than 0.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <paramref name="srcOff" /> more than length of <paramref name="src" /> - (sizeof(
        ///                 <typeparamref name="T" />) * <paramref name="count" />).
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <paramref name="count" /> less than 1.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <paramref name="count" /> less than length of <paramref name="src" />.
        ///             </description>
        ///         </item>
        ///     </list>
        /// </exception>
        internal static void ValidateArgumentsBytesToIntegersImpl<T>(byte[] src, int srcOff, T[] dst, int dstOff, int count,
                                                                     int? outputSizeRatio = null)
            where T : struct
        {
            if (src == null) {
                throw new ArgumentNullException("src");
            }
            if (src.Length < 0) {
                throw new ArgumentException("dst.Length < 0", "src");
            }

            if (dst == null) {
                throw new ArgumentNullException("dst");
            }
            if (dst.Length < 0) {
                throw new ArgumentException("src.Length < 0", "dst");
            }

            if (srcOff < 0) {
                throw new ArgumentOutOfRangeException("srcOff", "dstOff < 0");
            }
            if (dstOff < 0) {
                throw new ArgumentOutOfRangeException("dstOff", "srcOff < 0");
            }

            int srcLength = src.Length;
            int dstLength = dst.Length;

            if (outputSizeRatio != null && outputSizeRatio < 1) {
                throw new ArgumentOutOfRangeException("outputSizeRatio", "outputSizeRatio < 1");
            }
            int dstToBaSizeRatio = Shared.SizeOf<T>();
            if (count < 0 || (count * dstToBaSizeRatio) > srcLength || count > dstLength) {
                throw new ArgumentOutOfRangeException("count",
                    "count < 0, and/or (count * sizeof(T)) > dst.Length and/or count > src.Length");
            }
            if (srcOff > 0 && srcOff + (count * dstToBaSizeRatio) > srcLength) {
                if (srcOff >= srcLength) {
                    throw new ArgumentOutOfRangeException("srcOff", "dstOff >= dst.Length");
                }
                // More common case
                throw new ArgumentException("dstOff + (count * sizeof(T)) > dst.Length", "srcOff");
            }

            if (dstOff > 0 && dstOff + count > dstLength) {
                if (dstOff >= dstLength) {
                    throw new ArgumentOutOfRangeException("dstOff", "srcOff >= src.Length");
                }
                // More common case
                throw new ArgumentException("srcOff + count > src.Length", "dstOff");
            }
        }

        /// <summary>
        ///     Validate arguments/parameters to prevent unexpected exceptions.
        ///     Throws well-defined exceptions for different types of invalid arguments(s).
        /// </summary>
        /// <param name="dst">Destination byte array to write output to.</param>
        /// <param name="dstOff">Offset in <paramref name="dst" /> to write data to.</param>
        /// <param name="src">Source <typeparamref name="T" /> array to read input from.</param>
        /// <param name="srcOff">Offset in <paramref name="src" /> to read data from.</param>
        /// <param name="count">Number of items (of the size of <typeparamref name="T" />) to process.</param>
        /// <param name="outputSizeRatio">
        ///     How many output units (bytes) are required to write one transformed unit of the output type
        ///     <typeparamref name="T" /> (<paramref name="dst" /> : <paramref name="src" />). Default is null - size will be
        ///     determined algorithmically. It is recommended to supply the size ratio for better performance,
        ///     instead of relying on this.
        /// </param>
        /// <typeparam name="T">
        ///     Source type of data in <paramref name="src" /> that data
        ///     from will be transformed from and written to <paramref name="dst" />.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="src" />or <paramref name="dst" /> are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Length of <paramref name="dst" /> less than 0.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <list type="bullet">
        ///         <item>
        ///             <description>
        ///                 <paramref name="dstOff" /> less than 0.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <paramref name="dstOff" /> more than length of <paramref name="dst" /> - (sizeof(
        ///                 <typeparamref name="T" />) * <paramref name="count" />).
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <paramref name="count" /> less than 1.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <paramref name="count" /> less than length of <paramref name="dst" />.
        ///             </description>
        ///         </item>
        ///     </list>
        /// </exception>
        internal static void ValidateArgumentsIntegersToBytesImpl<T>(T[] src, int srcOff, byte[] dst, int dstOff, int count,
                                                                     int? outputSizeRatio = null)
            where T : struct
        {
            if (src == null) {
                throw new ArgumentNullException("src");
            }
            if (src.Length < 0) {
                throw new ArgumentException("src.Length < 0", "src");
            }

            if (dst == null) {
                throw new ArgumentNullException("dst");
            }
            if (dst.Length < 0) {
                throw new ArgumentException("dst.Length < 0", "dst");
            }

            if (srcOff < 0) {
                throw new ArgumentOutOfRangeException("srcOff", "srcOff < 0");
            }
            if (dstOff < 0) {
                throw new ArgumentOutOfRangeException("dstOff", "dstOff < 0");
            }

            int srcLength = dst.Length;
            int dstLength = src.Length;

            if (outputSizeRatio != null && outputSizeRatio < 1) {
                throw new ArgumentOutOfRangeException("outputSizeRatio", "outputSizeRatio < 1");
            }
            int dstToBaSizeRatio = Shared.SizeOf<T>();
            if (count < 0 || (count * dstToBaSizeRatio) > srcLength || count > dstLength) {
                throw new ArgumentOutOfRangeException("count",
                    "count < 0, and/or (count * sizeof(T)) > dst.Length and/or count > src.Length");
            }
            if (dstOff > 0 && dstOff + (count * dstToBaSizeRatio) > srcLength) {
                if (dstOff >= srcLength) {
                    throw new ArgumentOutOfRangeException("dstOff", "dstOff >= dst.Length");
                }
                // More common case
                throw new ArgumentException("dstOff + (count * sizeof(T)) > dst.Length", "dstOff");
            }

            if (srcOff > 0 && srcOff + count > dstLength) {
                if (srcOff >= dstLength) {
                    throw new ArgumentOutOfRangeException("srcOff", "srcOff >= src.Length");
                }
                // More common case
                throw new ArgumentException("srcOff + count > src.Length", "srcOff");
            }
        }

        /// <summary>
        ///     Used to verify arguments for a method of the form "copy <paramref name="length" /> items,
        ///     possibly with modification, from <paramref name="src" />[<paramref name="srcOff" />] to
        ///     <paramref name="dst" />[<paramref name="dstOff" />].".
        /// </summary>
        /// <typeparam name="T">Type of the source and destination arrays.</typeparam>
        /// <param name="src">Source data array.</param>
        /// <param name="dst">Destination array for data.</param>
        /// <param name="length">Number of items to copy from <paramref name="src" /> into <paramref name="dst" />.</param>
        /// <param name="srcOff">Offset in <paramref name="src" /> to read from.</param>
        /// <param name="dstOff">Offset in <paramref name="dst" /> to write to.</param>
        /// <param name="srcName">
        ///     Name of the argument for <paramref name="src" />.
        ///     Set to null (default) if existing name matches.
        /// </param>
        /// <param name="dstName">
        ///     Name of the argument for <paramref name="dst" />.
        ///     Set to null (default) if existing name matches.
        /// </param>
        /// <param name="lengthName">
        ///     Name of the argument for <paramref name="length" />.
        ///     Set to null (default) if existing name matches.
        /// </param>
        /// <param name="srcOffName">
        ///     Name of the argument for <paramref name="srcOff" />.
        ///     Set to null (default) if existing name matches.
        /// </param>
        /// <param name="dstOffName">
        ///     Name of the argument for <paramref name="dstOff" />.
        ///     Set to null (default) if existing name matches.
        /// </param>
        internal static void ThrowOnInvalidArgument<T>(T[] src, T[] dst, int length, int srcOff = 0, int dstOff = 0,
                                                       string srcName = null, string dstName = null, string lengthName = null,
                                                       string srcOffName = null, string dstOffName = null) where T : struct
        {
            if (src == null) {
                throw new ArgumentNullException(srcName ?? "src");
            }
            int srcLength = src.Length;
            if (src.Length < 0) {
                throw new ArgumentException(String.Format("{0}.Length < 0 : {1} < 0", srcName ?? "src", srcLength), srcName ?? "src");
            }

            if (dst == null) {
                throw new ArgumentNullException(dstName ?? "dst");
            }
            int dstLength = dst.Length;
            if (dst.Length < 0) {
                throw new ArgumentException(String.Format("{0}.Length < 0 : {1} < 0", dstName ?? "dst", dstLength), dstName ?? "dst");
            }

            if (srcOff != 0 || dstOff != 0 || length != srcLength) {
                if (length < 0) {
                    throw new ArgumentOutOfRangeException(lengthName ?? "length",
                        String.Format("{0} < 0 : {1} < 0", lengthName ?? "length", length));
                }
                // Check source values
                if (srcOff + length > srcLength) {
                    if (srcOff >= srcLength) {
                        throw new ArgumentException(
                            String.Format("{0} >= {1}.Length : {2} >= {3}",
                                srcOffName ?? "srcOff", srcName ?? "src", srcOff, srcLength));
                    }
                    if (length > srcLength) {
                        throw new ArgumentOutOfRangeException(lengthName ?? "length",
                            String.Format("{0} > {1}.Length : {2} > {3}",
                                lengthName ?? "length", srcName ?? "src", length, srcLength));
                    }
                    // Either the array is smaller than expected/desired, 
                    // or the chosen offset and/or length are for a different size array...
                    throw new ArgumentException(
                        String.Format("{0} + {1} > {2}.Length : {3} + {4} > {5}",
                            srcOffName ?? "srcOff", lengthName ?? "length", srcName ?? "src",
                            srcOff, length, srcLength));
                }
                if (srcOff < 0) {
                    throw new ArgumentOutOfRangeException(srcOffName ?? "srcOff",
                        String.Format("{0} < 0 : {1} < 0",
                            srcOffName ?? "srcOff", srcOff));
                }
                // Check destination values
                if (dstOff + length > dstLength) {
                    if (dstOff >= dstLength) {
                        throw new ArgumentException(
                            String.Format("{0} >= {1} : {2} >= {3}",
                                dstOffName ?? "dstOff", dstName ?? "dst", dstOff, dstLength));
                    }
                    if (length > dstLength) {
                        throw new ArgumentOutOfRangeException(lengthName ?? "length",
                            String.Format("{0} > {1}.Length : {2} > {3}",
                                lengthName ?? "length", dstName ?? "dst", length, dstLength));
                    }
                    // Either the array is smaller than expected/desired, 
                    // or the chosen offset and/or length are for a different size array...
                    throw new ArgumentException(
                        String.Format("{0} + {1} > {2}.Length : {3} + {4} > {5}",
                            dstOffName ?? "dstOff", lengthName ?? "length", dstName ?? "dst",
                            dstOff, length, dstLength));
                }
                if (dstOff < 0) {
                    throw new ArgumentOutOfRangeException(dstOffName ?? "dstOff",
                        String.Format("{0} < 0 : {1} < 0",
                            dstOffName ?? "dstOff", dstOff));
                }
            }
        }

        #endregion

        #region Performance copy/swap methods

#if INCLUDE_UNSAFE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void SwapOrder(byte* srcPtr, byte* dstPtr, int count)
        {
            byte* dstEnd = &dstPtr[count];
            for (int i = 0; i < count; i++) {
                *(dstEnd - i) = *(srcPtr + i);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void SwapOrder16(byte* srcPtr, byte* dstPtr, int count)
        {
            int byteCount = count * 2;
            byte* dstEnd = &dstPtr[byteCount];
            for (int i = 0; i < byteCount; i += 2) {
                *(dstEnd - i) = *(srcPtr + i);
                *(dstEnd - i - 1) = *(srcPtr + i + 1);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void SwapOrder32(byte* srcPtr, byte* dstPtr, int count)
        {
            int byteCount = count * 4;
            byte* dstEnd = &dstPtr[byteCount];
            for (int i = 0; i < byteCount; i += 4) {
                *(dstEnd - i) = *(srcPtr + i);
                *(dstEnd - i - 1) = *(srcPtr + i + 1);
                *(dstEnd - i - 2) = *(srcPtr + i + 2);
                *(dstEnd - i - 3) = *(srcPtr + i + 3);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void SwapOrder64(byte* srcPtr, byte* dstPtr, int count)
        {
            int byteCount = count * 8;
            byte* dstEnd = &dstPtr[byteCount];
            for (int i = 0; i < byteCount; i += 8) {
                *(dstEnd - i) = *(srcPtr + i);
                *(dstEnd - i - 1) = *(srcPtr + i + 1);
                *(dstEnd - i - 2) = *(srcPtr + i + 2);
                *(dstEnd - i - 3) = *(srcPtr + i + 3);
                *(dstEnd - i - 4) = *(srcPtr + i + 4);
                *(dstEnd - i - 5) = *(srcPtr + i + 5);
                *(dstEnd - i - 6) = *(srcPtr + i + 6);
                *(dstEnd - i - 7) = *(srcPtr + i + 7);
            }
        }

        internal static unsafe void CopyMemory(byte* srcPtr, byte* dstPtr, int length)
        {
            if (Shared.PlatformWordSize == sizeof(UInt32)) {
                int remainingBytes;
                int words32 = DivRem(length, sizeof(UInt32), out remainingBytes);
                var src32Ptr = (UInt32*) srcPtr;
                var dst32Ptr = (UInt32*) dstPtr;
                for (int i = 0; i < words32; i += 2) {
                    *(dst32Ptr + i) = *(src32Ptr + i);
                    *(dst32Ptr + i + 1) = *(src32Ptr + i + 1);
                }
                if (remainingBytes >= sizeof(UInt32)) {
                    *(dst32Ptr + words32) = *(src32Ptr + words32);
                    words32++;
                    remainingBytes -= sizeof(UInt32);
                }
                srcPtr += words32 * sizeof(UInt32);
                dstPtr += words32 * sizeof(UInt32);
                length = remainingBytes;
            } else if (Shared.PlatformWordSize == sizeof(UInt64)) {
                int remainingBytes;
                int words64 = DivRem(length, sizeof(UInt64), out remainingBytes);
                var src64Ptr = (UInt64*) srcPtr;
                var dst64Ptr = (UInt64*) dstPtr;
                for (int i = 0; i < words64; i += 2) {
                    *(dst64Ptr + i) = *(src64Ptr + i);
                    *(dst64Ptr + i + 1) = *(src64Ptr + i + 1);
                }
                if (remainingBytes >= sizeof(UInt64)) {
                    *(dst64Ptr + words64) = *(src64Ptr + words64);
                    words64++;
                    remainingBytes -= sizeof(UInt64);
                }
                if (remainingBytes >= sizeof(UInt32)) {
                    *(UInt32*) (dst64Ptr + words64) = *(UInt32*) (src64Ptr + words64);
                    dstPtr += sizeof(UInt32);
                    srcPtr += sizeof(UInt32);
                    remainingBytes -= sizeof(UInt32);
                }
                srcPtr += words64 * sizeof(UInt64);
                dstPtr += words64 * sizeof(UInt64);
                length = remainingBytes;
            }

            if (length >= sizeof(UInt16)) {
                *(UInt16*) dstPtr = *(UInt16*) srcPtr;
                dstPtr += sizeof(UInt16);
                srcPtr += sizeof(UInt16);
                length -= sizeof(UInt16);
            }

            if (length > 0) {
                *dstPtr = *srcPtr;
            }
        }
#endif

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int DivRem(int dividend, int divisor, out int remainder)
        {
            int quotient = dividend / divisor;
            remainder = dividend - (divisor * quotient);
            return quotient;
        }
    }
}
