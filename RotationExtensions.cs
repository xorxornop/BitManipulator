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

namespace BitManipulator
{
    /// <summary>
    ///     Extension methods for circularly-rotating integral numbers ("rotate through carry" derivative not provided).
    /// </summary>
    public static class RotationExtensions
    {
        #region Left rotation

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte RotateLeft(this byte i, int distance)
        {
            ThrowOnInvalidArgument(distance, 8);
            return i.RotateLeft_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte RotateLeft_NoChecks(this byte i, int distance)
        {
            return (byte) ((i << distance) ^ (i >> -distance));
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 RotateLeft(this Int16 i, int distance)
        {
            const int size = sizeof(Int16) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateLeft_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 RotateLeft_NoChecks(this Int16 i, int distance)
        {
            const int size = sizeof(Int16) * 8;
            return (Int16) (((UInt16) i << distance) | ((UInt16) i >> (size - distance)));
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 RotateLeft(this UInt16 i, int distance)
        {
            const int size = sizeof(UInt16) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateLeft_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 RotateLeft_NoChecks(this UInt16 i, int distance)
        {
            return (UInt16) ((i << distance) ^ (i >> -distance));
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 RotateLeft(this Int32 i, int distance)
        {
            const int size = sizeof(Int32) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateLeft_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 RotateLeft_NoChecks(this Int32 i, int distance)
        {
            const int size = sizeof(Int32) * 8;
            return (Int32) (((UInt32) i << distance) | ((UInt32) i >> (size - distance)));
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 RotateLeft(this UInt32 i, int distance)
        {
            const int size = sizeof(UInt32) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateLeft_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 RotateLeft_NoChecks(this UInt32 i, int distance)
        {
            return (i << distance) ^ (i >> -distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 RotateLeft(this Int64 i, int distance)
        {
            const int size = sizeof(Int64) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateLeft_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 RotateLeft_NoChecks(this Int64 i, int distance)
        {
            const int size = sizeof(Int64) * 8;
            return (Int64) (((UInt64) i << distance) | ((UInt64) i >> (size - distance)));
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 RotateLeft(this UInt64 i, int distance)
        {
            const int size = sizeof(UInt64) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateLeft_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> left by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &lt;&lt;&lt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 RotateLeft_NoChecks(this UInt64 i, int distance)
        {
            return (i << distance) ^ (i >> -distance);
        }

        #endregion

        #region Right rotation

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte RotateRight(this byte i, int distance)
        {
            ThrowOnInvalidArgument(distance, 8);
            return i.RotateRight_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte RotateRight_NoChecks(this byte i, int distance)
        {
            return (byte) ((i >> distance) ^ (i << -distance));
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 RotateRight(this Int16 i, int distance)
        {
            const int size = sizeof(Int16) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateRight_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 RotateRight_NoChecks(this Int16 i, int distance)
        {
            const int size = sizeof(Int16) * 8;
            return (Int16) (((UInt16) i >> distance) | ((UInt16) i << (size - distance)));
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 RotateRight(this UInt16 i, int distance)
        {
            const int size = sizeof(UInt16) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateRight_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 RotateRight_NoChecks(this UInt16 i, int distance)
        {
            return (UInt16) ((i >> distance) ^ (i << -distance));
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 RotateRight(this Int32 i, int distance)
        {
            const int size = sizeof(Int32) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateRight_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 RotateRight_NoChecks(this Int32 i, int distance)
        {
            const int size = sizeof(Int32) * 8;
            return (Int32) (((UInt32) i >> distance) | ((UInt32) i << (size - distance)));
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 RotateRight(this UInt32 i, int distance)
        {
            const int size = sizeof(UInt32) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateRight_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 RotateRight_NoChecks(this UInt32 i, int distance)
        {
            return (i >> distance) ^ (i << -distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 RotateRight(this Int64 i, int distance)
        {
            const int size = sizeof(Int64) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateRight_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 RotateRight_NoChecks(this Int64 i, int distance)
        {
            const int size = sizeof(Int64) * 8;
            return (Int64) (((UInt64) i >> distance) | ((UInt64) i << (size - distance)));
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 RotateRight(this UInt64 i, int distance)
        {
            const int size = sizeof(UInt64) * 8;
            ThrowOnInvalidArgument(distance, size);
            return i.RotateRight_NoChecks(distance);
        }

        /// <summary>
        ///     Rotate an integer <paramref name="i" /> right by <paramref name="distance" /> bits
        ///     (<paramref name="i" /> &gt;&gt;&gt; <paramref name="distance" />).
        /// </summary>
        /// <param name="i">Integer to rotate.</param>
        /// <param name="distance">Distance to rotate.</param>
        /// <returns>Rotated integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 RotateRight_NoChecks(this UInt64 i, int distance)
        {
            return (i >> distance) ^ (i << -distance);
        }

        #endregion

        /// <summary>
        ///     Validate arguments/parameters to prevent unexpected exceptions.
        /// </summary>
        /// <param name="distance">Distance in bits to rotate (positive integer).</param>
        /// <param name="typeSizeBits">Size in bits of value type to be rotated (e.g. ulong = 64 bits).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Shift <paramref name="distance" /> is negative (less than 0), or exceeds
        ///     <paramref name="typeSizeBits" /> (bit-size of relevant value type - e.g. UInt64 = 64).
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowOnInvalidArgument(int distance, int typeSizeBits)
        {
            Debug.Assert(typeSizeBits >= 1 && typeSizeBits <= sizeof(UInt64),
                "typeSizeBits is less than a byte, or more than a UInt64 (bit-size).");
            if (distance < 0) {
                throw new ArgumentOutOfRangeException(
                    "distance",
                    String.Format("distance < 0 : {0} < 0",
                        distance));
            }
            if (distance > typeSizeBits) {
                throw new ArgumentOutOfRangeException(
                    "distance",
                    String.Format("distance > sizeOfTBits : {0} > {1}",
                        distance,
                        typeSizeBits));
            }
        }
    }
}
