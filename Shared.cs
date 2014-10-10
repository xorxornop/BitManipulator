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

#if INCLUDE_UNSAFE
using System;

namespace BitManipulationExtensions
{
    internal class Shared
    {
#if INCLUDE_UNSAFE
        internal const int UnmanagedThreshold = 128;
        internal static readonly int PlatformWordSize = IntPtr.Size;
        internal static readonly int PlatformWordSizeBits = PlatformWordSize * 8;
#endif

        internal static int SizeOf<T>() where T : struct
        {
            var typeOfT = typeof(T);
            if (typeOfT == typeof(byte)) {
                return 1;
            } else if (typeOfT == typeof(short) || typeOfT == typeof(ushort)) {
                return sizeof(short);
            } else if (typeOfT == typeof(int) || typeOfT == typeof(uint)) {
                return sizeof(int);
            } else if (typeOfT == typeof(long) || typeOfT == typeof(ulong)) {
                return sizeof(long);
            }
            // Other type
            throw new NotSupportedException("T : " + typeof(T).Name + " - Not a supported type."  );
        }

#if INCLUDE_UNSAFE
        /// <summary>
        ///     Copy data from <paramref name="src" /> into <paramref name="dst" />.
        /// </summary>
        /// <param name="src">Pointer to source of data.</param>
        /// <param name="dst">Pointer to destination for data.</param>
        /// <param name="length">Length of data to copy in bytes.</param>
        internal static unsafe void CopyMemory(byte* src, byte* dst, int length)
        {
            if (Shared.PlatformWordSize == sizeof(UInt32)) {
                while (length >= sizeof(UInt64)) {
                    *(UInt32*)dst = *(UInt32*)src;
                    dst += sizeof(UInt32);
                    src += sizeof(UInt32);
                    *(UInt32*)dst = *(UInt32*)src;
                    dst += sizeof(UInt32);
                    src += sizeof(UInt32);
                    length -= sizeof(UInt64);
                }
            } else if (Shared.PlatformWordSize == sizeof(UInt64)) {
                while (length >= sizeof(UInt64) * 2) {
                    *(UInt64*)dst = *(UInt64*)src;
                    dst += sizeof(UInt64);
                    src += sizeof(UInt64);
                    *(UInt64*)dst = *(UInt64*)src;
                    dst += sizeof(UInt64);
                    src += sizeof(UInt64);
                    length -= sizeof(UInt64) * 2;
                }

                if (length >= sizeof(UInt64)) {
                    *(UInt64*)dst = *(UInt64*)src;
                    dst += sizeof(UInt64);
                    src += sizeof(UInt64);
                    length -= sizeof(UInt64);
                }
            }

            if (length >= sizeof(UInt32)) {
                *(UInt32*)dst = *(UInt32*)src;
                dst += sizeof(UInt32);
                src += sizeof(UInt32);
                length -= sizeof(UInt32);
            }

            if (length >= sizeof(UInt16)) {
                *(UInt16*)dst = *(UInt16*)src;
                dst += sizeof(UInt16);
                src += sizeof(UInt16);
                length -= sizeof(UInt16);
            }

            if (length > 0) {
                *dst = *src;
            }
        }
#endif
    }
}
#endif
