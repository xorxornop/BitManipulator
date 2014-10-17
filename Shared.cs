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

namespace BitManipulator
{
    internal class Shared
    {
#if INCLUDE_UNSAFE
        internal const int UnmanagedThreshold = 128;
        internal static readonly int PlatformWordSize = IntPtr.Size;
        internal static readonly int PlatformWordSizeBits = PlatformWordSize * 8;
#endif

        /// <summary>
        ///     Determine the size in memory used to store a struct
        ///     of type <typeparamref name="T" /> at runtime.
        ///     Automatically determines if <typeparamref name="T" /> is
        ///     an array, and if so, checks the array element type.
        /// </summary>
        /// <remarks>
        ///     The sizeof operator cannot be used to get size information at run time, and so
        ///     this quite inelegant method is an unfortunate but necessary workaround.
        /// </remarks>
        /// <typeparam name="T">Type of the struct.</typeparam>
        /// <returns>Size of a <typeparamref name="T" /> instance in bytes.</returns>
        internal static int SizeOf<T>() where T : struct
        {
            Type typeOfT = typeof (T);
            if (typeOfT.IsArray) {
                typeOfT = typeOfT.GetElementType();
            }

            if (typeOfT == typeof (byte)) {
                return 1;
            }
            if (typeOfT == typeof (short) || typeOfT == typeof (ushort)) {
                return sizeof(short);
            }
            if (typeOfT == typeof (int) || typeOfT == typeof (uint)) {
                return sizeof(int);
            }
            if (typeOfT == typeof (long) || typeOfT == typeof (ulong)) {
                return sizeof(long);
            }
            // Other type
            throw new NotSupportedException("T : " + typeof (T).Name + " - Not a supported type.");
        }
    }
}
