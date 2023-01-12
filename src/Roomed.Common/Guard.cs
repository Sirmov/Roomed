// |-----------------------------------------------------------------------------------------------------|
// <copyright file="Guard.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Common
{
    using System.Text.RegularExpressions;

    using Roomed.Common.Constants;

    /// <summary>
    /// This static class contains methods for guarding
    /// against different type of invalid states of data.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// This method throws an exception when <paramref name="variable"/> is set to a null reference.
        /// </summary>
        /// <param name="variable">The variable to be checked.</param>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="isArgument">Flag indicating whether the <paramref name="variable"/> is a argument.</param>
        /// <exception cref="ArgumentNullException">
        /// Throws when the <paramref name="variable"/> is null
        /// and the <paramref name="isArgument"/> is set to <see langword="true"/>.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// Throws when the <paramref name="variable"/> is null
        /// and the <paramref name="isArgument"/> is set to <see langword="false"/>.
        /// </exception>
        public static void AgainstNull(object? variable, string variableName, bool isArgument = false)
        {
            if (variable == null)
            {
                if (isArgument)
                {
                    throw new ArgumentNullException(string.Format(ErrorMessagesConstants.ArgumentIsNull, variableName));
                }

                throw new NullReferenceException(string.Format(ErrorMessagesConstants.VariableIsNull, variableName));
            }
        }

        /// <summary>
        /// This method throws an exception when the <paramref name="boolean"/> is true.
        /// </summary>
        /// <param name="boolean">The boolean to be checked.</param>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="isArgument">Flag indicating whether the <paramref name="boolean"/> is a argument.</param>
        /// <exception cref="ArgumentException">
        /// Throws when the <paramref name="boolean"/> is true
        /// and the <paramref name="isArgument"/> is set to <see langword="true"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// Throws when the <paramref name="boolean"/> is true
        /// and the <paramref name="isArgument"/> is set to <see langword="false"/>.
        /// </exception>
        public static void AgainstTrue(bool boolean, string variableName, bool isArgument = false)
        {
            Guard.AgainstNull(boolean, nameof(boolean), true);
            Guard.AgainstNull(variableName, nameof(variableName), true);

            if (boolean == true)
            {
                if (isArgument)
                {
                    throw new ArgumentException(string.Format(ErrorMessagesConstants.ArgumentIsTrue, variableName));
                }

                throw new Exception(string.Format(ErrorMessagesConstants.VariableIsTrue, variableName));
            }
        }

        /// <summary>
        /// This method throws an exception when the <paramref name="boolean"/> is false.
        /// </summary>
        /// <param name="boolean">The boolean to be checked.</param>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="isArgument">Flag indicating whether the <paramref name="boolean"/> is a argument.</param>
        /// <exception cref="ArgumentException">
        /// Throws when the <paramref name="boolean"/> is false
        /// and the <paramref name="isArgument"/> is set to <see langword="true"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// Throws when the <paramref name="boolean"/> is false
        /// and the <paramref name="isArgument"/> is set to <see langword="false"/>.
        /// </exception>
        public static void AgainstFalse(bool boolean, string variableName, bool isArgument = false)
        {
            Guard.AgainstNull(boolean, nameof(boolean), true);
            Guard.AgainstNull(variableName, nameof(variableName), true);

            if (boolean == false)
            {
                if (isArgument)
                {
                    throw new ArgumentException(string.Format(ErrorMessagesConstants.ArgumentIsFalse, variableName));
                }

                throw new Exception(string.Format(ErrorMessagesConstants.VariableIsFalse, variableName));
            }
        }

        /// <summary>
        /// This method throws an exception when the <paramref name="text"/> does not math the <paramref name="regex"/> pattern.
        /// </summary>
        /// <param name="text">The text that should match the regex.</param>
        /// <param name="regex">The regex.</param>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="isArgument">Flag indicating whether the <paramref name="text"/> is a argument.</param>
        /// <exception cref="ArgumentException">
        /// Throws when the <paramref name="text"/> does not math the <paramref name="regex"/> pattern
        /// and the <paramref name="isArgument"/> is set to <see langword="true"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// Throws when the <paramref name="text"/> does not math the <paramref name="regex"/> pattern
        /// and the <paramref name="isArgument"/> is set to <see langword="false"/>.
        /// </exception>
        public static void AgainstRegex(string text, Regex regex, string variableName, bool isArgument = false)
        {
            Guard.AgainstNull(text, nameof(text), true);
            Guard.AgainstNull(regex, nameof(regex), true);
            Guard.AgainstNull(variableName, nameof(variableName), true);

            if (regex.IsMatch(text) == false)
            {
                if (isArgument)
                {
                    throw new ArgumentException(string.Format(ErrorMessagesConstants.ArgumentRegexDoesNotMatch, variableName, text, regex.ToString()));
                }

                throw new Exception(string.Format(ErrorMessagesConstants.VariableRegexDoesNotMatch, variableName, text, regex.ToString()));
            }
        }

        /// <summary>
        /// This method throws an exception when the <paramref name="text"/> does not math the regex <paramref name="pattern"/>.
        /// </summary>
        /// <param name="text">The text that should match the regex.</param>
        /// <param name="pattern">The regex pattern.</param>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="isArgument">Flag indicating whether the <paramref name="text"/> is a argument.</param>
        /// <exception cref="ArgumentException">
        /// Throws when the <paramref name="text"/> does not math the
        /// regex <paramref name="pattern"/> and the <paramref name="isArgument"/> is set to <see langword="true"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// Throws when the <paramref name="text"/> does not math the
        /// regex <paramref name="pattern"/> and the <paramref name="isArgument"/> is set to <see langword="false"/>.
        /// </exception>
        public static void AgainstRegex(string text, string pattern, string variableName, bool isArgument = false)
        {
            Guard.AgainstNull(text, nameof(text), true);
            Guard.AgainstNull(pattern, nameof(pattern), true);
            Guard.AgainstNull(variableName, nameof(variableName), true);

            if (Regex.IsMatch(text, pattern) == false)
            {
                if (isArgument)
                {
                    throw new ArgumentException(string.Format(ErrorMessagesConstants.ArgumentRegexDoesNotMatch, variableName, text, pattern));
                }

                throw new Exception(string.Format(ErrorMessagesConstants.VariableRegexDoesNotMatch, variableName, text, pattern));
            }
        }

        /// <summary>
        /// This method throws an exception when the <paramref name="text"/> is null or empty.
        /// </summary>
        /// <param name="text">The string variable.</param>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="isArgument">Flag indicating whether the <paramref name="text"/> is a argument.</param>
        /// <exception cref="ArgumentException">
        /// Throws when the <paramref name="text"/> is null or empty
        /// and the <paramref name="isArgument"/> is set to <see langword="true"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// Throws when the <paramref name="text"/> is null or empty
        /// and the <paramref name="isArgument"/> is set to <see langword="false"/>.
        /// </exception>
        public static void AgainstNullOrEmpty(string text, string variableName, bool isArgument = false)
        {
            Guard.AgainstNull(text, nameof(text), true);
            Guard.AgainstNull(variableName, nameof(variableName), true);

            if (string.IsNullOrEmpty(text))
            {
                if (isArgument)
                {
                    throw new ArgumentException(string.Format(ErrorMessagesConstants.ArgumentIsNullOrEmpty, variableName));
                }

                throw new Exception(string.Format(ErrorMessagesConstants.VariableIsNullOrEmpty, variableName));
            }
        }

        /// <summary>
        /// This method throws an exception when the <paramref name="text"/> is null or whitespace.
        /// </summary>
        /// <param name="text">The string variable.</param>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="isArgument">Flag indicating whether the <paramref name="text"/> is a argument.</param>
        /// <exception cref="ArgumentException">
        /// Throws when the <paramref name="text"/> is null or whitespace
        /// and the <paramref name="isArgument"/> is set to <see langword="true"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// Throws when the <paramref name="text"/> is null or whitespace
        /// and the <paramref name="isArgument"/> is set to <see langword="false"/>.
        /// </exception>
        public static void AgainstNullOrWhiteSpace(string text, string variableName, bool isArgument = false)
        {
            Guard.AgainstNull(text, nameof(text), true);
            Guard.AgainstNull(variableName, nameof(variableName), true);

            if (string.IsNullOrWhiteSpace(text))
            {
                if (isArgument)
                {
                    throw new ArgumentException(string.Format(ErrorMessagesConstants.ArgumentIsNullOrWhiteSpace, variableName));
                }

                throw new Exception(string.Format(ErrorMessagesConstants.VariableIsNullOrWhiteSpace, variableName));
            }
        }
    }
}
