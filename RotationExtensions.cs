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
    ///     Extension methods for rotating integral numbers.
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
        public static UInt32 RotateLeft(this UInt32 i, int distance)
        {
            ThrowOnInvalidArgument(distance, 32);
            return (i << distance) | (i >> -distance);
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
            ThrowOnInvalidArgument(distance, 32);
            return (i << distance) ^ (Int32) ((UInt32) i >> -distance);
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
            ThrowOnInvalidArgument(distance, 64);
            return (i << distance) | (i >> -distance);
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
            ThrowOnInvalidArgument(distance, 64);
            return (i << distance) ^ (Int64) ((UInt64) i >> -distance);
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
        public static UInt32 RotateRight(this UInt32 i, int distance)
        {
            ThrowOnInvalidArgument(distance, 32);
            return (i >> distance) | (i << -distance);
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
            ThrowOnInvalidArgument(distance, 32);
            return (Int32) ((UInt32) i >> distance) ^ (i << -distance);
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
            ThrowOnInvalidArgument(distance, 64);
            return (i >> distance) | (i << -distance);
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
            ThrowOnInvalidArgument(distance, 64);
            return (Int64) ((UInt64) i >> distance) ^ (i << -distance);
        }

        #endregion

        /// <summary>
        ///     Validate arguments/parameters to prevent unexpected exceptions.
        /// </summary>
        /// <param name="distance">Distance to rotate.</param>
        /// <param name="sizeOfTBits">Size of value type to be rotated, in bits (e.g. uint = 32 bits).</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowOnInvalidArgument(int distance, int sizeOfTBits)
        {
            if (Math.Abs(distance) > sizeOfTBits) {
                throw new ArgumentOutOfRangeException("distance", "abs(distance) > sizeOfTBits");
            }
        }
    }
}
