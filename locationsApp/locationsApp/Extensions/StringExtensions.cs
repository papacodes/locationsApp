using System;
using System.Collections.Generic;
using System.Text;

namespace locationsApp.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check to determine if a string is null or empty
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>The answer</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Check to determine if a string is not null or empty
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>The answer</returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !IsNullOrEmpty(str);
        }

        /// <summary>
        /// Check to determine if a string is null or white space
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>The answer</returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Check to determine if a string is not null or white space
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>The answer</returns>
        public static bool IsNotNullOrWhiteSpace(this string str)
        {
            return !IsNullOrWhiteSpace(str);
        }
    }
}
