// |-----------------------------------------------------------------------------------------------------|
// <copyright file="SessionExtensions.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Web.Extensions
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class holds all extension methods for the <see cref="ISession"/> interface.
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// This method is used to persist more complex object by serializing them.
        /// </summary>
        /// <typeparam name="T">The type of the object to be persisted.</typeparam>
        /// <param name="session">The session where the data should be stored.</param>
        /// <param name="key">The key of the new entry.</param>
        /// <param name="value">The value of the new entry.</param>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value, Formatting.None));
        }

        /// <summary>
        /// This method is used to read more complex serialized data
        /// persisted by the <see cref="Set{T}(ISession, string, T)"/> method.
        /// </summary>
        /// <typeparam name="T">The type of the object to be read.</typeparam>
        /// <param name="session">The session where the data is stored.</param>
        /// <param name="key">The key of of the entry.</param>
        /// <returns>
        /// Returns the desirialized entry or the <see langword="default"/> of <typeparamref name="T"/>
        /// if no entry with <paramref name="key"/> can be found.
        /// </returns>
        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
