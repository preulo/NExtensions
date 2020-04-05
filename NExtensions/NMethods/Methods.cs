using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace NExtensions.NMethods
{
    public static class Methods
    {
        /// <summary>
        /// Add an item to an object that implements the System.Collections.Generic.IDictionary<TKey, TValue> interface if the key if the key is not already in the dictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of the key of the dictionary.</typeparam>
        /// <typeparam name="TValue">Type of the values of the dictionary.</typeparam>
        /// <param name="dictionary">The System.Collections.Generic.IDictionary to add the item to.</param>
        /// <param name="key">Key of the item to add.</param>
        /// <param name="value">Value of the item to add.</param>
        /// <returns>true if the item was added; otherwise, false.</returns>
        public static bool AddIfNotContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            bool contains = dictionary.ContainsKey(key);

            if (!contains)
            {
                dictionary.Add(key, value);
            }

            return !contains;
        }

        /// <summary>
        /// Adds multiple items to a list.
        /// </summary>
        /// <typeparam name="T">Type of the items in the list.</typeparam>
        /// <param name="list">The System.Collections.Generic.List to which the elements will be added.</param>
        /// <param name="items">Items to add to the list.</param>
        public static void AddRange<T>(this List<T> list, params T[] items)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            list.AddRange(items);
        }

        /// <summary>
        /// Adds multiple strings to a System.Collections.Specialized.StringCollection.
        /// </summary>
        /// <param name="collection">The System.Collections.Specialized.StringCollection to which the strings will be added.</param>
        /// <param name="strings">Strings to add to the collection.</param>
        public static void AddRange(this StringCollection collection, params string[] strings)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (strings == null)
            {
                throw new ArgumentNullException(nameof(strings));
            }

            if (strings.Length == 0)
            {
                return;
            }

            collection.AddRange(strings);
        }

        /// <summary>
        /// Adds the strings of a System.Collections.Specialized.StringCollection object to another System.Collections.Specialized.StringCollection object.
        /// </summary>
        /// <param name="collection">The System.Collections.Specialized.StringCollection object to which the elements will be added.</param>
        /// <param name="items">The System.Collections.Specialized.StringCollection object containing the items to be added.</param>
        public static void AddRange(this StringCollection collection, StringCollection items)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            string[] itemsArray = new string[items.Count];

            items.CopyTo(itemsArray, 0);

            collection.AddRange(itemsArray);
        }

        /// <summary>
        /// Checks whether a value can be considered to be inside a range defined by two values.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">Value to check.</param>
        /// <param name="from">First of the two limits of the range.</param>
        /// <param name="to">Second of the two limits of the range.</param>
        /// <param name="inclusive">true if value should be considered inside the range if it is equal to one of the limits; false is value should be considered not inside the range if it is equal to one of the limits.</param>
        /// <returns>true if value is inside the range defined by from and to; otherwise, false.</returns>
        public static bool Between<T>(this T value, T from, T to, bool inclusive = true) where T : IComparable
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (from == null)
            {
                throw new ArgumentNullException(nameof(from));
            }

            if (to == null)
            {
                throw new ArgumentNullException(nameof(to));
            }

            int compareArguments = from.CompareTo(to);

            bool expectedOrder = compareArguments < 0;

            T lower = expectedOrder ? from : to;
            T upper = expectedOrder ? to : from;

            int compareResultFrom = value.CompareTo(lower);
            int compareResultTo   = value.CompareTo(upper);

            bool passesFrom = inclusive ? compareResultFrom >= 0 : compareResultFrom > 0;
            bool passesTo   = inclusive ? compareResultTo   <= 0 : compareResultTo   < 0;

            return passesFrom && passesTo;
        }

        /// <summary>
        /// Enqueues an item on a queue if it is not already enqueued.
        /// </summary>
        /// <typeparam name="T">Type of the queue's items.</typeparam>
        /// <param name="queue">Queue to search and enqueue the item.</param>
        /// <param name="item">Item to be searched for and enqueued.</param>
        public static void EnqueueIfNotContains<T>(this Queue<T> queue, T item)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (!queue.Contains(item))
            {
                queue.Enqueue(item);
            }
        }

        /// <summary>
        /// Returns the first string in the System.Collections.Specialized.StringCollection.
        /// </summary>
        /// <param name="collection">A System.Collections.Specialized.StringCollection object to return a string from.</param>
        /// <param name="predicate">An optional function to test each element for a condition.</param>
        /// <returns>The first string in the StringCollection; in case a predicate was provided, the first string in the StringCollection that satisfies the condition.</returns>
        public static string First(this StringCollection collection, Func<string, bool> predicate = null)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count == 0)
            {
                throw new InvalidOperationException($"{nameof(collection)} contains no elements");
            }

            if (predicate == null)
            {
                return collection[0];
            }
            else
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    string stringItem = collection[i];

                    if (predicate(stringItem))
                    {
                        return stringItem;
                    }
                }

                throw new InvalidOperationException("Collection contains no matching element");
            }
        }

        /// <summary>
        /// Performs the specified action on each element of the System.Collections.Generic.List, providing the index of the current element to the action being performed.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the System.Collections.Generic.List.</typeparam>
        /// <param name="list">The list containing the items to which the action will be performed on.</param>
        /// <param name="action">The System.Action delegate to perform on each element of the System.Collections.Generic.List.</param>
        public static void For<T>(this List<T> list, Action<int, T> action)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            for (int i = 0; i < list.Count; i++)
            {
                action(i, list[i]);
            }
        }

        /// <summary>
        /// Copies all the possible values of an enumerator type to a new array.
        /// </summary>
        /// <typeparam name="T">The type of the System.Enum which values will be returned.</typeparam>
        /// <returns>An array with all the values of enumerator of type T.</returns>
        public static T[] GetEnumValues<T>() where T : Enum
        {
            Type typeT = typeof(T);

            if (!typeT.IsEnum)
            {
                throw new InvalidOperationException($"T ({typeT.FullName}) is not an enum type.");
            }

            return Enum.GetValues(typeT).Cast<T>().ToArray();
        }

        /// <summary>
        /// Checks whether an item is contained in an array of values.
        /// </summary>
        /// <typeparam name="T">Type of the item to be checked.</typeparam>
        /// <param name="item">Item to be checked.</param>
        /// <param name="values">Array of values to search the item.</param>
        /// <returns>true if the item is found in the array; otherwise, false.</returns>
        public static bool In<T>(this T item, params T[] values)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values.Length == 0)
            {
                return false;
            }

            bool found = false;

            foreach (T value in values)
            {
                if (item.Equals(value))
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        /// <summary>
        /// Checks whether an array is empty.
        /// </summary>
        /// <param name="array">Reference to an System.Array object.</param>
        /// <returns>true if the Length of the array object equals zero; otherwise, false.</returns>
        public static bool IsEmpty(this Array array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return array.Length == 0;
        }

        /// <summary>
        /// Checks whether a collection is empty.
        /// </summary>
        /// <param name="collection">Reference to an object that implements the System.Collections.ICollection interface.</param>
        /// <returns>true if the Count property of the collection equals zero; otherwise, false.</returns>
        public static bool IsEmpty(this ICollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Count == 0;
        }

        /// <summary>
        /// Checks whether a System.Text.StringBuilder is empty.
        /// </summary>
        /// <param name="stringBuilder">Reference to a System.Text.StringBuilder object.</param>
        /// <returns>true if the Length of the System.Text.StringBuilder equals zero; otherwise, false.</returns>
        public static bool IsEmpty(this StringBuilder stringBuilder)
        {
            if (stringBuilder == null)
            {
                throw new ArgumentNullException(nameof(stringBuilder));
            }

            return stringBuilder.Length == 0;
        }

        /// <summary>
        /// Returns the last string in the System.Collections.Specialized.StringCollection.
        /// </summary>
        /// <param name="collection">A System.Collections.Specialized.StringCollection object to return a string from.</param>
        /// <param name="predicate">An optional function to test each element for a condition.</param>
        /// <returns>The last string in the StringCollection; in case a predicate was provided, the last string in the StringCollection that satisfies the condition.</returns>
        public static string Last(this StringCollection collection, Func<string, bool> predicate = null)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count == 0)
            {
                throw new InvalidOperationException($"{nameof(collection)} contains no elements");
            }

            if (predicate == null)
            {
                return collection[collection.Count - 1];
            }
            else
            {
                for (int i = collection.Count - 1; i >= 0; i--)
                {
                    string stringItem = collection[i];

                    if (predicate(stringItem))
                    {
                        return stringItem;
                    }
                }

                throw new InvalidOperationException("Collection contains no matching element");
            }
        }

        /// <summary>
        /// Returns a string last but one character identical to the current instance. The original string remains unchanged.
        /// </summary>
        /// <param name="stringToRemove">The string to be copied.</param>
        /// <param name="n">The number of characters to remove, starting from the end.</param>
        /// <returns>A copy of the string with the last character removed.</returns>
        public static string RemoveLast(this string stringToRemove, int n = 1)
        {
            if (stringToRemove == null)
            {
                throw new ArgumentNullException(nameof(stringToRemove));
            }

            if (n < 1)
            {
                throw new ArgumentException($"{nameof(n)} must be a positive integer");
            }

            if (stringToRemove.Length < n)
            {
                throw new InvalidOperationException($"{nameof(stringToRemove)} contains insufficient characters");
            }

            return stringToRemove.Remove(stringToRemove.Length - n);
        }

        /// <summary>
        /// Removes the last n characters of a System.Text.StringBuilder object.
        /// </summary>
        /// <param name="stringBuilder">The string builder to modify.</param>
        /// <param name="n">The number of characters to remove, starting from the end.</param>
        public static void RemoveLast(this StringBuilder stringBuilder, int n = 1)
        {
            if (stringBuilder == null)
            {
                throw new ArgumentNullException(nameof(stringBuilder));
            }

            if (n < 1)
            {
                throw new ArgumentException($"{nameof(n)} must be a positive integer");
            }

            if (stringBuilder.Length < n)
            {
                throw new InvalidOperationException($"{nameof(stringBuilder)} contains insufficient characters");
            }

            stringBuilder.Length -= n;
        }

        /// <summary>
        /// Looks up for substrings inside a string and replaces them with an empty string. The original string remains unchanged.
        /// </summary>
        /// <param name="stringToSearch">String to look up into.</param>
        /// <param name="substrings">An array that contains the values to look up to substitute.</param>
        /// <returns>A new string with the values of the array oldValues replaced by String.Empty.</returns>
        public static string RemoveMany(this string stringToSearch, params string[] substrings)
        {
            if (stringToSearch == null)
            {
                throw new ArgumentNullException(nameof(stringToSearch));
            }

            if (substrings == null)
            {
                throw new ArgumentNullException(nameof(substrings));
            }

            if (substrings.Any(s => s == null))
            {
                throw new ArgumentException($"{nameof(substrings)} cannot contain nulls.");
            }

            if (string.Empty.In(substrings))
            {
                throw new ArgumentException($"{nameof(substrings)} cannot contain empty strings.");
            }

            var builderReplace = new StringBuilder(stringToSearch);

            foreach (string oldValue in substrings)
            {
                builderReplace.Replace(oldValue, string.Empty);
            }

            return builderReplace.ToString();
        }

        /// <summary>
        /// Copies the elements of the System.Collections.Specialized.StringCollection to a new array.
        /// </summary>
        /// <param name="stringCollection">A System.Collections.Specialized.StringCollection object containing the elements to copy.</param>
        /// <returns>An array containing copies of the elements of the System.Collections.Specialized.StringCollection.</returns>
        public static string[] ToArray(this StringCollection stringCollection)
        {
            if (stringCollection == null)
            {
                throw new ArgumentNullException(nameof(stringCollection));
            }

            string[] array = new string[stringCollection.Count];

            stringCollection.CopyTo(array, 0);

            return array;
        }

        /// <summary>
        /// Parses a string to the corresponding enumerator type.
        /// </summary>
        /// <typeparam name="T">Type of the enumerator.</typeparam>
        /// <param name="enumString">The string representation of an enum value of type T.</param>
        /// <param name="caseInsensitive">true to ignore case; false to consider case.</param>
        /// <returns>An enum of type T whose value is represented by enumString.</returns>
        public static T ToEnum<T>(this string enumString, bool caseInsensitive = false) where T : struct
        {
            if (enumString == null)
            {
                throw new ArgumentNullException(nameof(enumString));
            }

            bool validEnumValue = SyntaxFacts.IsValidIdentifier(enumString);

            if (!validEnumValue)
            {
                throw new ArgumentException($"{enumString} is not a valid enum value", nameof(enumString));
            }

            Type typeT = typeof(T);

            if (!typeT.IsEnum)
            {
                throw new InvalidOperationException($"Type {typeT.FullName} is not an enum type.");
            }

            bool parsed = Enum.TryParse(enumString, caseInsensitive, out T result);

            if (parsed)
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"{enumString} is not a value of {typeT.FullName}", nameof(enumString));
            }
        }

        /// <summary>
        /// Converts the value of an object to its equivalent string representation using the specified format and culture-specific format information, if applicable.
        /// </summary>
        /// <param name="obj">The current System.Object to get the string representation.</param>
        /// <param name="objFormat">A format string to use.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string representation of the System.Object.</returns>
        public static string ToString(this object obj, string objFormat = "", IFormatProvider formatProvider = null)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (objFormat == null)
            {
                throw new ArgumentNullException(nameof(objFormat));
            }

            if (obj is IFormattable)
            {
                return (obj as IFormattable).ToString(objFormat, formatProvider);
            }
            else
            {
                return obj.ToString();
            }
        }
    }
}