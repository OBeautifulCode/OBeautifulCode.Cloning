﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CloningExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Cloning.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Cloning.Recipes
{
    using global::System;
    using global::System.Collections.Concurrent;
    using global::System.Collections.Generic;
    using global::System.Collections.ObjectModel;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Linq;
    using global::System.Reflection;

    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Type;
    using OBeautifulCode.Type.Recipes;

    using static global::System.FormattableString;

    /// <summary>
    /// Extension methods to clone objects of various types.
    /// </summary>
#if !OBeautifulCodeCloningSolution
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Cloning.Recipes", "See package version number")]
    internal
#else
    public
#endif
    static class CloningExtensions
    {
        private static readonly MethodInfo DeepCloneDictionaryMethod = typeof(CloningExtensions)
            .GetMethods()
            .Where(_ => _.Name == nameof(DeepClone))
            .Single(_ => _.ReturnType.IsGenericType && (_.ReturnType.GetGenericTypeDefinition() == typeof(Dictionary<,>)));

        private static readonly MethodInfo DeepCloneReadOnlyDictionaryMethod = typeof(CloningExtensions)
            .GetMethods()
            .Where(_ => _.Name == nameof(DeepClone))
            .Single(_ => _.ReturnType.IsGenericType && (_.ReturnType.GetGenericTypeDefinition() == typeof(ReadOnlyDictionary<,>)));

        private static readonly MethodInfo DeepCloneConcurrentDictionaryMethod = typeof(CloningExtensions)
            .GetMethods()
            .Where(_ => _.Name == nameof(DeepClone))
            .Single(_ => _.ReturnType.IsGenericType && (_.ReturnType.GetGenericTypeDefinition() == typeof(ConcurrentDictionary<,>)));

        private static readonly MethodInfo DeepCloneListMethod = typeof(CloningExtensions)
            .GetMethods()
            .Where(_ => _.Name == nameof(DeepClone))
            .Single(_ => _.ReturnType.IsGenericType && (_.ReturnType.GetGenericTypeDefinition() == typeof(List<>)));

        private static readonly MethodInfo DeepCloneCollectionMethod = typeof(CloningExtensions)
            .GetMethods()
            .Where(_ => _.Name == nameof(DeepClone))
            .Single(_ => _.ReturnType.IsGenericType && (_.ReturnType.GetGenericTypeDefinition() == typeof(Collection<>)));

        private static readonly MethodInfo DeepCloneReadOnlyCollectionMethod = typeof(CloningExtensions)
            .GetMethods()
            .Where(_ => _.Name == nameof(DeepClone))
            .Single(_ => _.ReturnType.IsGenericType && (_.ReturnType.GetGenericTypeDefinition() == typeof(ReadOnlyCollection<>)));

        private static readonly MethodInfo DeepCloneArrayMethod = typeof(CloningExtensions)
            .GetMethods()
            .Where(_ => _.Name == nameof(DeepClone))
            .Single(_ => _.ReturnType.IsArray);

        /// <summary>
        /// Deep clones an arbitrary value.
        /// </summary>
        /// <typeparam name="T">The type of the value to deep clone.</typeparam>
        /// <param name="value">The value to deep clone.</param>
        /// <returns>
        /// A deep clone of the specified value.  If value is null, returns null.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static T DeepClone<T>(
            this T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var result = DeepCloneInternal(value);

            return result;
        }

        /// <summary>
        /// Deep clones a <see cref="string"/> value.
        /// </summary>
        /// <param name="value">The value to deep clone.</param>
        /// <returns>
        /// A deep clone of the specified value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads", Justification = ObcSuppressBecause.CA_ALL_NotApplicable)]
        public static string DeepClone(
            this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            // string should be cloned using it's existing interface.
            // note that this just returns the same reference, it doesn't result in a new reference
            // the ToString() is needed because Clone() returns an Object.
            // https://stackoverflow.com/questions/3465377/whats-the-use-of-string-clone
            var result = value.Clone().ToString();

            return result;
        }

        /// <summary>
        /// Deep clones a <see cref="Version"/> value.
        /// </summary>
        /// <param name="value">The value to deep clone.</param>
        /// <returns>
        /// A deep clone of the specified value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static Version DeepClone(
            this Version value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var result = (Version)value.Clone();

            return result;
        }

        /// <summary>
        /// Deep clones a <see cref="Uri"/> value.
        /// </summary>
        /// <param name="value">The value to deep clone.</param>
        /// <returns>
        /// A deep clone of the specified value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static Uri DeepClone(
            this Uri value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            // Not entirely sure how to deep clone a relative URI,
            // which is why we are not simply calling new Uri(value.ToString(), UriKind.RelativeOrAbsolute)
            var result = value.IsAbsoluteUri
                ? new Uri(value.AbsoluteUri, UriKind.Absolute)
                : new Uri(value.ToString(), UriKind.Relative);

            return result;
        }

        /// <summary>
        /// Deep clones a <see cref="Dictionary{TKey, TValue}"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="value">The value to deep clone.</param>
        /// <returns>
        /// A deep clone of the specified value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static Dictionary<TKey, TValue> DeepClone<TKey, TValue>(
            Dictionary<TKey, TValue> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var result = new Dictionary<TKey, TValue>(value.Count);

            foreach (var kvp in value)
            {
                result.Add(kvp.Key.DeepCloneInternal(), kvp.Value.DeepCloneInternal());
            }

            return result;
        }

        /// <summary>
        /// Deep clones a <see cref="ReadOnlyDictionary{TKey, TValue}"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="value">The value to deep clone.</param>
        /// <returns>
        /// A deep clone of the specified value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static ReadOnlyDictionary<TKey, TValue> DeepClone<TKey, TValue>(
            ReadOnlyDictionary<TKey, TValue> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var dictionary = new Dictionary<TKey, TValue>(value.Count);

            foreach (var kvp in value)
            {
                dictionary.Add(kvp.Key.DeepCloneInternal(), kvp.Value.DeepCloneInternal());
            }

            var result = new ReadOnlyDictionary<TKey, TValue>(dictionary);

            return result;
        }

        /// <summary>
        /// Deep clones a <see cref="ConcurrentDictionary{TKey, TValue}"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="value">The value to deep clone.</param>
        /// <returns>
        /// A deep clone of the specified value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static ConcurrentDictionary<TKey, TValue> DeepClone<TKey, TValue>(
            ConcurrentDictionary<TKey, TValue> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var dictionary = new Dictionary<TKey, TValue>(value.Count);

            foreach (var kvp in value)
            {
                dictionary.Add(kvp.Key.DeepCloneInternal(), kvp.Value.DeepCloneInternal());
            }

            var result = new ConcurrentDictionary<TKey, TValue>(dictionary);

            return result;
        }

        /// <summary>
        /// Deep clones a <see cref="List{T}"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="value">The value to deep clone.</param>
        /// <returns>
        /// A deep clone of the specified value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required by DeepCloneInternal<T>.")]
        public static List<T> DeepClone<T>(
            List<T> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var result = new List<T>(value.Count);

            foreach (var element in value)
            {
                result.Add(element.DeepCloneInternal());
            }

            return result;
        }

        /// <summary>
        /// Deep clones a <see cref="Collection{T}"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="value">The value to deep clone.</param>
        /// <returns>
        /// A deep clone of the specified value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static Collection<T> DeepClone<T>(
            Collection<T> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var result = new Collection<T>();

            foreach (var element in value)
            {
                result.Add(element.DeepCloneInternal());
            }

            return result;
        }

        /// <summary>
        /// Deep clones a <see cref="ReadOnlyCollection{T}"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the read-only collection.</typeparam>
        /// <param name="value">The value to deep clone.</param>
        /// <returns>
        /// A deep clone of the specified value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static ReadOnlyCollection<T> DeepClone<T>(
            ReadOnlyCollection<T> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var list = new List<T>(value.Count);

            foreach (var element in value)
            {
                list.Add(element.DeepCloneInternal());
            }

            var result = new ReadOnlyCollection<T>(list);

            return result;
        }

        /// <summary>
        /// Deep clones an array value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <param name="value">The value to deep clone.</param>
        /// <returns>
        /// A deep clone of the specified value.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static T[] DeepClone<T>(
            T[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var result = new T[value.Length];

            for (var x = 0; x < value.Length; x++)
            {
                result[x] = value[x].DeepCloneInternal();
            }

            return result;
        }

        private static T DeepCloneInternal<T>(
            this T value)
        {
            object result;

            var declaredType = typeof(T);

            if (ReferenceEquals(value, null))
            {
                result = null;
            }
            else
            {
                var runtimeType = value.GetType();

                if (declaredType.IsValueType || runtimeType.IsValueType)
                {
                    // This covers Nullable types as well.
                    result = value;
                }
                else if (value is IDeepCloneable<T> deepCloneableValue)
                {
                    // Is the declared type assignable to IDeepCloneable<T>?
                    result = deepCloneableValue.DeepClone();
                }
                else if (declaredType.IsSystemDictionaryType() || runtimeType.IsSystemDictionaryType())
                {
                    var keyType = runtimeType.GetClosedSystemDictionaryKeyType();

                    var valueType = runtimeType.GetClosedSystemDictionaryValueType();

                    // Here we are intentionally creating the same runtime type when the declared type is an interface (e.g. IReadOnlyDictionary<,>).
                    // We want the original object to be equal to the cloned object, using OBeautifulCode.Equality.Recipes.EqualityExtensions.IsEqualTo().
                    // For a declared type of type(object), that method will return false if two objects are not of the same runtime type.
                    // See notes in that method and the related ObjectEqualityComparer for why.
                    if (runtimeType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                    {
                        result = DeepCloneDictionaryMethod.MakeGenericMethod(keyType, valueType).Invoke(null, new object[] { value });
                    }
                    else if (runtimeType.GetGenericTypeDefinition() == typeof(ReadOnlyDictionary<,>))
                    {
                        result = DeepCloneReadOnlyDictionaryMethod.MakeGenericMethod(keyType, valueType).Invoke(null, new object[] { value });
                    }
                    else if (runtimeType.GetGenericTypeDefinition() == typeof(ConcurrentDictionary<,>))
                    {
                        result = DeepCloneConcurrentDictionaryMethod.MakeGenericMethod(keyType, valueType).Invoke(null, new object[] { value });
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"I do not know how to deep clone this type of dictionary '{runtimeType.ToStringReadable()}'."));
                    }
                }
                else if (declaredType.IsSystemCollectionType() || runtimeType.IsSystemCollectionType())
                {
                    var elementType = runtimeType.GetClosedSystemCollectionElementType();

                    // Here we are intentionally creating the same runtime type when the declared type is an interface (e.g. IReadOnlyList<,>)
                    // See note above in Dictionary logic.
                    if (runtimeType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        result = DeepCloneListMethod.MakeGenericMethod(elementType).Invoke(null, new object[] { value });
                    }
                    else if (runtimeType.GetGenericTypeDefinition() == typeof(Collection<>))
                    {
                        result = DeepCloneCollectionMethod.MakeGenericMethod(elementType).Invoke(null, new object[] { value });
                    }
                    else if (runtimeType.GetGenericTypeDefinition() == typeof(ReadOnlyCollection<>))
                    {
                        result = DeepCloneReadOnlyCollectionMethod.MakeGenericMethod(elementType).Invoke(null, new object[] { value });
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"I do not know how to deep clone this type of collection '{runtimeType.ToStringReadable()}'."));
                    }
                }
                else if (declaredType.IsArray || runtimeType.IsArray)
                {
                    var elementType = runtimeType.GetElementType();

                    result = DeepCloneArrayMethod.MakeGenericMethod(elementType).Invoke(null, new object[] { value });
                }
                else if (value is string valueAsString)
                {
                    result = valueAsString.DeepClone();
                }
                else if (value is Version valueAsVersion)
                {
                    result = valueAsVersion.DeepClone();
                }
                else if (value is Uri valueAsUri)
                {
                    result = valueAsUri.DeepClone();
                }
                else if (declaredType != runtimeType)
                {
                    // Is the runtime type assignable to IDeepCloneable<T>?
                    var deepCloneableInterface = typeof(IDeepCloneable<>).MakeGenericType(runtimeType);

                    // ReSharper disable once UseMethodIsInstanceOfType
                    if (deepCloneableInterface.IsAssignableFrom(runtimeType))
                    {
                        var deepCloneMethod = deepCloneableInterface.GetMethod(nameof(IDeepCloneable<object>.DeepClone));

                        // ReSharper disable once PossibleNullReferenceException
                        result = deepCloneMethod.Invoke(value, null);
                    }
                    else
                    {
                        throw new NotSupportedException(Invariant($"I do not know how to deep clone an object of type '{runtimeType.ToStringReadable()}'."));
                    }
                }
                else
                {
                    throw new NotSupportedException(Invariant($"I do not know how to deep clone an object of type '{runtimeType.ToStringReadable()}'."));
                }
            }

            return (T)result;
        }
    }
}
