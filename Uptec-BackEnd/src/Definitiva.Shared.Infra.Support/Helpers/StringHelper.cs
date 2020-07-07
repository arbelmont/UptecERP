using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Definitiva.Shared.Infra.Support.Helpers
{
    public static class StringHelper
    {
        #region Extension Methods that return a string

        /// <summary>
        /// Returns a specified number of characters from the left side of a string.
        /// </summary>
        /// <param name="length">Number of characters on the left.</param>
        /// <returns></returns>
        public static string Left(this string value, int length)
        {
            if (value.IsNullOrWhiteSpace())
                return value;

            return value.Length <= length ? value : value.Substring(0, Math.Abs(length));
        }

        /// <summary>
        /// Returns a specified number of characters from the right side of a string.
        /// </summary>
        /// <param name="length">Number of characters on the right.</param>
        /// <returns></returns>
        public static string Right(this string value, int length)
        {
            if (value.IsNullOrWhiteSpace())
                return value;

            return value.Length <= length ? value : value.Substring(value.Length - Math.Abs(length));
        }

        /// <summary>
        /// Replaces null with the informed character.
        /// </summary>
        /// <param name="replacement">Character that will replace null</param>
        /// <returns></returns>
        public static string ReplaceNull(this string value, string replacement = "")
        {
            return value.IsNullOrWhiteSpace() ? replacement : value;
        }

        /// <summary>
        /// Returns the numeric part of the string.
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyNumbers(this string value)
        {
            return Regex.Replace(value.ReplaceNull(""), "[^0-9]", "");
        }

        public static string ToCamelCase(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? value : $"{value.Substring(0, 1).ToLower()}{value.Substring(1)}";
        }

        public static string ToCamelCase(this string value, char separator = '.')
        {
            var result = new StringBuilder();
            var parts = value.Split(separator);

            result.Append(parts.First().ToCamelCase());
            for (int i = 1; i < parts.Count(); i++)
            {
                result.Append(parts[i].ToPascalCase());
            }
            return result.ToString();
        }

        public static string ToPascalCase(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? value : $"{value.Substring(0, 1).ToUpper()}{value.Substring(1).ToLower()}";
        }

        public static string ToPascalCase(this string value, char separator = '.', bool returnSeparator = false)
        {
            var result = new StringBuilder();

            foreach (var item in value.Split(separator))
            {
                result.Append($"{item.ToPascalCase()}{(returnSeparator ? separator.ToString() : string.Empty)}");
            }

            return result.ToString();
        }

        /// <summary>
        /// Remove last character from string according to paramero character.
        /// </summary>
        /// <param name="character">Character to be removed.</param>
        /// <returns></returns>
        public static string RemoveLastCharIf(this string value, char character = '.')
        {
            return value.EndsWith(character.ToString()) ? value.Remove(value.Length - 1) : value;
        }


        /// <summary>
        /// Adds the character to the end of the current string (if it does not end with a character)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="character">Character to be added in end of string. </param>
        /// <returns></returns>
        public static string AddEndChar(this string value, string character = "")
        {
            return (!value.IsNullOrWhiteSpace() && !value.Trim().EndsWith(character)) ? $"{value.Trim()}{character}" : value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveAccents(this string value, string codePage = "ISO-8859-8")
        {
            return Encoding.ASCII.GetString(Encoding.GetEncoding(codePage).GetBytes(value));
        }
        #endregion


        #region Extension Methods that return a list of string

        public static IEnumerable<string> GetSplited(this string value, char separator)
        {
            if (value.IsNullOrWhiteSpace())
                return new List<string>();

            return value.Split(separator).Select(p => p.Trim()).ToList();
        }
        #endregion


        #region Extension Methods that return a boolean

        public static bool FormedByTheSameCharacter(this string value)
        {
            if (value.IsNullOrWhiteSpace())
                return true;

            for (var i = 1; i < value.Length; i++)
                if (value[i] != value[0]) return false;

            return true;
        }

        public static bool IsNullOrWhiteSpace(this string value) => (string.IsNullOrWhiteSpace(value));

        public static bool ContainsNumber(this string value)
        {
            var result = false;

            value = value.Trim();

            var sequency = value.ToCharArray();
            if (sequency.Any(char.IsDigit))
                result = true;

            return result;
        }

        public static bool FormedOnlyByNumbers(this string value, bool ignoreDotOrComma = false)
        {
            if (value.IsNullOrWhiteSpace())
                return false;

            var result = true;

            value = value.Trim();
            if (ignoreDotOrComma)
                value = value.Replace(".", "").Replace(",", "");

            var sequence = value.ToCharArray();
            if (sequence.Any(t => !char.IsDigit(t)))
                result = false;

            return result;
        }
        #endregion


        #region Extension Methods that return a Decimal
        public static decimal ToDecimal(this string value)
        {
            Decimal result;

            if (CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator.ToString() == ",")
                decimal.TryParse(value.Replace(".", ","), out result);
            else
                decimal.TryParse(value, out result);

            return result;
        }
        #endregion
    }
}
