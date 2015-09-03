// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;

namespace YAWL.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Replaces all invalid characters in a file name with the specified replacement
        /// character. Default replacement character is '_'.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="replacementChar"></param>
        /// <returns></returns>
        public static string FixFileName(this string fileName, char replacementChar = '_')
        {
            var invalid = new HashSet<char>(Path.GetInvalidFileNameChars());

            // using manual builder instead of LINQ is intentional
            var chars = new char[fileName.Length];

            for (var index = 0; index < fileName.Length; index++)
            {
                var ch = fileName[index];
                chars[index] = invalid.Contains(ch) ? replacementChar : ch;
            }

            return new string(chars);
        }
    }
}
