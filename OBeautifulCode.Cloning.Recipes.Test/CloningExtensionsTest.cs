// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CloningExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Cloning.Recipes.Test
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.CodeAnalysis.Recipes;
    using OBeautifulCode.Equality.Recipes;
    using OBeautifulCode.Type;

    using Xunit;

    public static class CloningExtensionsTest
    {
        [Fact]
        public static void DeepClone_T___Should_throw_ArgumentNullException___When_parameter_value_is_null()
        {
            // Arrange
            string value1 = null;
            DateTime? value2 = null;

            // Act
            var actual1 = Record.Exception(() => value1.DeepClone<string>());
            var actual2 = Record.Exception(() => value2.DeepClone<DateTime?>());

            // Assert
            actual1.AsTest().Must().BeOfType<ArgumentNullException>();
            actual1.Message.AsTest().Must().ContainString("value");

            actual2.AsTest().Must().BeOfType<ArgumentNullException>();
            actual2.Message.AsTest().Must().ContainString("value");
        }

        [Fact]
        public static void DeepClone_T___Should_throw_NotSupportedException___When_parameter_value_is_not_cloneable()
        {
            // Arrange
            var value1 = new NotCloneableClass1();
            var value2 = new NotCloneableClass2();

            // Act
            var actual1 = Record.Exception(() => value1.DeepClone<NotCloneableClass1>());
            var actual2 = Record.Exception(() => value2.DeepClone<ITestInterface>());

            // Assert
            actual1.AsTest().Must().BeOfType<NotSupportedException>();
            actual1.Message.AsTest().Must().ContainString("I do not know how to deep clone an object of type");

            actual2.AsTest().Must().BeOfType<NotSupportedException>();
            actual2.Message.AsTest().Must().ContainString("I do not know how to deep clone an object of type");
        }

        [Fact]
        public static void DeepClone_T___Should_deep_clone_objects___When_called()
        {
            // Arrange
            var expected = new TestClass
            {
                ObjectProperty = new object(),
                StringProperty = A.Dummy<string>(),
                VersionProperty = A.Dummy<Version>(),
                UriProperty = A.Dummy<Uri>(),
                TestInterfaceClassProperty = A.Dummy<TestInterfaceClass>(),
                TestArrayProperty = new[] { new TestClassElement(A.Dummy<int>()), null, new TestClassElement(A.Dummy<int>()) },
                DictionaryProperty = new Dictionary<TestClassElement, TestClassElement> { { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, { A.Dummy<TestClassElement>(), null }, { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, },
                DictionaryInterfaceProperty = new Dictionary<TestClassElement, TestClassElement> { { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, { A.Dummy<TestClassElement>(), null }, { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, },
                ReadOnlyDictionaryProperty = new ReadOnlyDictionary<TestClassElement, TestClassElement>(new Dictionary<TestClassElement, TestClassElement> { { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, { A.Dummy<TestClassElement>(), null }, { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, }),
                ReadOnlyDictionaryInterfaceProperty = new Dictionary<TestClassElement, TestClassElement> { { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, { A.Dummy<TestClassElement>(), null }, { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, },
                ConcurrentDictionaryProperty = new ConcurrentDictionary<TestClassElement, TestClassElement>(new Dictionary<TestClassElement, TestClassElement> { { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, { A.Dummy<TestClassElement>(), null }, { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, }),
                CollectionProperty = new Collection<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
                CollectionInterfaceProperty = new Collection<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
                ReadOnlyCollectionProperty = new ReadOnlyCollection<TestClassElement>(new List<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() }),
                ReadOnlyCollectionInterfaceProperty = new List<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
                ListProperty = new List<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
                ListInterfaceProperty = new List<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
                ReadOnlyListInterfaceProperty = new TestClassElement[] { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
                ObjectStringProperty = A.Dummy<string>(),
                ObjectVersionProperty = A.Dummy<Version>(),
                ObjectUriProperty = A.Dummy<Uri>(),
                ObjectTestInterfaceClassProperty = A.Dummy<TestInterfaceClass>(),
                ObjectTestArrayProperty = new[] { new TestClassElement(A.Dummy<int>()), null, new TestClassElement(A.Dummy<int>()) },
                ObjectDictionaryProperty = new Dictionary<TestClassElement, TestClassElement> { { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, { A.Dummy<TestClassElement>(), null }, { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, },
                ObjectDictionaryInterfaceProperty = new Dictionary<TestClassElement, TestClassElement> { { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, { A.Dummy<TestClassElement>(), null }, { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, },
                ObjectReadOnlyDictionaryProperty = new ReadOnlyDictionary<TestClassElement, TestClassElement>(new Dictionary<TestClassElement, TestClassElement> { { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, { A.Dummy<TestClassElement>(), null }, { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, }),
                ObjectReadOnlyDictionaryInterfaceProperty = new Dictionary<TestClassElement, TestClassElement> { { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, { A.Dummy<TestClassElement>(), null }, { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, },
                ObjectConcurrentDictionaryProperty = new ConcurrentDictionary<TestClassElement, TestClassElement>(new Dictionary<TestClassElement, TestClassElement> { { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, { A.Dummy<TestClassElement>(), null }, { A.Dummy<TestClassElement>(), A.Dummy<TestClassElement>() }, }),
                ObjectCollectionProperty = new Collection<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
                ObjectCollectionInterfaceProperty = new Collection<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
                ObjectReadOnlyCollectionProperty = new ReadOnlyCollection<TestClassElement>(new List<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() }),
                ObjectReadOnlyCollectionInterfaceProperty = new List<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
                ObjectListProperty = new List<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
                ObjectListInterfaceProperty = new List<TestClassElement> { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
                ObjectReadOnlyListInterfaceProperty = new TestClassElement[] { A.Dummy<TestClassElement>(), null, A.Dummy<TestClassElement>() },
            };

            // Act
            var actual = expected.DeepClone<TestClass>();

            // Assert
            actual.AsTest().Must().NotBeSameReferenceAs(expected);
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void DeepClone_T___Should_deep_clone_objects___When_all_collection_and_dictionary_properties_are_empty()
        {
            // Arrange
            var expected = new TestClass
            {
                ObjectProperty = new object(),
                StringProperty = A.Dummy<string>(),
                VersionProperty = A.Dummy<Version>(),
                UriProperty = A.Dummy<Uri>(),
                TestInterfaceClassProperty = A.Dummy<TestInterfaceClass>(),
                TestArrayProperty = new TestClassElement[0],
                DictionaryProperty = new Dictionary<TestClassElement, TestClassElement>(),
                DictionaryInterfaceProperty = new Dictionary<TestClassElement, TestClassElement>(),
                ReadOnlyDictionaryProperty = new ReadOnlyDictionary<TestClassElement, TestClassElement>(new Dictionary<TestClassElement, TestClassElement>()),
                ReadOnlyDictionaryInterfaceProperty = new Dictionary<TestClassElement, TestClassElement>(),
                ConcurrentDictionaryProperty = new ConcurrentDictionary<TestClassElement, TestClassElement>(new Dictionary<TestClassElement, TestClassElement>()),
                CollectionProperty = new Collection<TestClassElement>(),
                CollectionInterfaceProperty = new Collection<TestClassElement>(),
                ReadOnlyCollectionProperty = new ReadOnlyCollection<TestClassElement>(new List<TestClassElement>()),
                ReadOnlyCollectionInterfaceProperty = new List<TestClassElement>(),
                ListProperty = new List<TestClassElement>(),
                ListInterfaceProperty = new List<TestClassElement>(),
                ReadOnlyListInterfaceProperty = new List<TestClassElement>(),
                ObjectStringProperty = A.Dummy<string>(),
                ObjectVersionProperty = A.Dummy<Version>(),
                ObjectUriProperty = A.Dummy<Uri>(),
                ObjectTestInterfaceClassProperty = A.Dummy<TestInterfaceClass>(),
                ObjectTestArrayProperty = new TestClassElement[0],
                ObjectDictionaryProperty = new Dictionary<TestClassElement, TestClassElement>(),
                ObjectDictionaryInterfaceProperty = new Dictionary<TestClassElement, TestClassElement>(),
                ObjectReadOnlyDictionaryProperty = new ReadOnlyDictionary<TestClassElement, TestClassElement>(new Dictionary<TestClassElement, TestClassElement>()),
                ObjectReadOnlyDictionaryInterfaceProperty = new Dictionary<TestClassElement, TestClassElement>(),
                ObjectConcurrentDictionaryProperty = new ConcurrentDictionary<TestClassElement, TestClassElement>(new Dictionary<TestClassElement, TestClassElement>()),
                ObjectCollectionProperty = new Collection<TestClassElement>(),
                ObjectCollectionInterfaceProperty = new Collection<TestClassElement>(),
                ObjectReadOnlyCollectionProperty = new ReadOnlyCollection<TestClassElement>(new List<TestClassElement>()),
                ObjectReadOnlyCollectionInterfaceProperty = new List<TestClassElement>(),
                ObjectListProperty = new List<TestClassElement>(),
                ObjectListInterfaceProperty = new List<TestClassElement>(),
                ObjectReadOnlyListInterfaceProperty = new List<TestClassElement>(),
            };

            // Act
            var actual = expected.DeepClone<TestClass>();

            // Assert
            actual.AsTest().Must().NotBeSameReferenceAs(expected);
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void DeepClone_String___Should_throw_ArgumentNullException___When_parameter_value_is_null()
        {
            // Arrange
            string value = null;

            // Act
            var actual = Record.Exception(() => value.DeepClone());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("value");
        }

        [Fact]
        public static void DeepClone_String___Should_deep_clone_String___When_called()
        {
            // Arrange
            var expected = A.Dummy<string>();

            // Act
            var actual = expected.DeepClone();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);

            // the same referenced IS returned
            // actual.AsTest().Must().NotBeSameReferenceAs(expected);
        }

        [Fact]
        public static void DeepClone_Version___Should_throw_ArgumentNullException___When_parameter_value_is_null()
        {
            // Arrange
            Version value = null;

            // Act
            var actual = Record.Exception(() => value.DeepClone());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("value");
        }

        [Fact]
        public static void DeepClone_Version___Should_deep_clone_Version___When_called()
        {
            // Arrange
            var expected = A.Dummy<Version>();

            // Act
            var actual = expected.DeepClone();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
            actual.AsTest().Must().NotBeSameReferenceAs(expected);
        }

        [Fact]
        public static void DeepClone_Uri___Should_throw_ArgumentNullException___When_parameter_value_is_null()
        {
            // Arrange
            Uri value = null;

            // Act
            var actual = Record.Exception(() => value.DeepClone());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("value");
        }

        [Fact]
        public static void DeepClone_Uri___Should_deep_clone_Uri___When_called()
        {
            // Arrange
            var expected = new[]
            {
                new Uri("http://duckduckgo.com/search"),
                new Uri("http://duckduckgo.com/search/"),
                new Uri("http://duckduckgo.com/search/whatever.html"),
                new Uri("http://duckduckgo.com/search/whatever.html?id=monkey&name=something"),
                new Uri("http://duckduckgo.com/search.html#anchor"),
                new Uri("c:\\my-folder\\whatever.txt"),
                new Uri("c:\\my-folder\\whatever\\"),
                new Uri("/Skins/MainSkin.xaml", UriKind.Relative),
                new Uri("/Skins/MainSkin.xaml#anchor", UriKind.Relative),
            };

            var expectedToStrings = expected.Select(_ => _.ToString()).ToList();

            // Act
            var actuals = expected.Select(_ => _.DeepClone()).ToArray();
            var actualToStrings = expected.Select(_ => _.ToString()).ToList();

            // Assert
            for (var x = 0; x < expected.Length; x++)
            {
                actuals[x].AsTest().Must().BeEqualTo(expected[x]);
                actuals[x].AsTest().Must().NotBeSameReferenceAs(expected[x]);
                actualToStrings[x].AsTest().Must().BeEqualTo(expectedToStrings[x]);
            }
        }

        #pragma warning disable SA1201 // Elements should appear in the correct order
        private interface ITestInterface
        {
        }

        private class NotCloneableClass1
        {
        }

        private class NotCloneableClass2 : ITestInterface
        {
        }

        private class TestClass : IDeepCloneable<TestClass>, IEquatable<TestClass>
        {
            public object ObjectProperty { get; set; }

            public string StringProperty { get; set; }

            public Version VersionProperty { get; set; }

            public Uri UriProperty { get; set; }

            public ITestInterface TestInterfaceClassProperty { get; set; }

            public TestClassElement[] TestArrayProperty { get; set; }

            public IDictionary<TestClassElement, TestClassElement> DictionaryProperty { get; set; }

            public IDictionary<TestClassElement, TestClassElement> DictionaryInterfaceProperty { get; set; }

            public ReadOnlyDictionary<TestClassElement, TestClassElement> ReadOnlyDictionaryProperty { get; set; }

            public IReadOnlyDictionary<TestClassElement, TestClassElement> ReadOnlyDictionaryInterfaceProperty { get; set; }

            public ConcurrentDictionary<TestClassElement, TestClassElement> ConcurrentDictionaryProperty { get; set; }

            public Collection<TestClassElement> CollectionProperty { get; set; }

            public ICollection<TestClassElement> CollectionInterfaceProperty { get; set; }

            public ReadOnlyCollection<TestClassElement> ReadOnlyCollectionProperty { get; set; }

            public IReadOnlyCollection<TestClassElement> ReadOnlyCollectionInterfaceProperty { get; set; }

            public List<TestClassElement> ListProperty { get; set; }

            public IList<TestClassElement> ListInterfaceProperty { get; set; }

            public IReadOnlyList<TestClassElement> ReadOnlyListInterfaceProperty { get; set; }

            public object ObjectStringProperty { get; set; }

            public object ObjectVersionProperty { get; set; }

            public object ObjectUriProperty { get; set; }

            public object ObjectTestInterfaceClassProperty { get; set; }

            public object ObjectTestArrayProperty { get; set; }

            public object ObjectDictionaryProperty { get; set; }

            public object ObjectDictionaryInterfaceProperty { get; set; }

            public object ObjectReadOnlyDictionaryProperty { get; set; }

            public object ObjectReadOnlyDictionaryInterfaceProperty { get; set; }

            public object ObjectConcurrentDictionaryProperty { get; set; }

            public object ObjectCollectionProperty { get; set; }

            public object ObjectCollectionInterfaceProperty { get; set; }

            public object ObjectReadOnlyCollectionProperty { get; set; }

            public object ObjectReadOnlyCollectionInterfaceProperty { get; set; }

            public object ObjectListProperty { get; set; }

            public object ObjectListInterfaceProperty { get; set; }

            public object ObjectReadOnlyListInterfaceProperty { get; set; }

            public TestClass DeepClone()
            {
                var result = new TestClass
                {
                    ObjectProperty = this.ObjectProperty?.DeepClone<object>(),
                    StringProperty = this.StringProperty?.DeepClone<string>(),
                    VersionProperty = this.VersionProperty?.DeepClone<Version>(),
                    UriProperty = this.UriProperty?.DeepClone<Uri>(),
                    TestInterfaceClassProperty = this.TestInterfaceClassProperty?.DeepClone<ITestInterface>(),
                    TestArrayProperty = this.TestArrayProperty?.DeepClone<TestClassElement[]>(),
                    DictionaryProperty = this.DictionaryProperty?.DeepClone<IDictionary<TestClassElement, TestClassElement>>(),
                    DictionaryInterfaceProperty = this.DictionaryInterfaceProperty?.DeepClone<IDictionary<TestClassElement, TestClassElement>>(),
                    ReadOnlyDictionaryProperty = this.ReadOnlyDictionaryProperty?.DeepClone<ReadOnlyDictionary<TestClassElement, TestClassElement>>(),
                    ReadOnlyDictionaryInterfaceProperty = this.ReadOnlyDictionaryInterfaceProperty?.DeepClone<IReadOnlyDictionary<TestClassElement, TestClassElement>>(),
                    ConcurrentDictionaryProperty = this.ConcurrentDictionaryProperty?.DeepClone<ConcurrentDictionary<TestClassElement, TestClassElement>>(),
                    CollectionProperty = this.CollectionProperty?.DeepClone<Collection<TestClassElement>>(),
                    CollectionInterfaceProperty = this.CollectionInterfaceProperty?.DeepClone<ICollection<TestClassElement>>(),
                    ReadOnlyCollectionProperty = this.ReadOnlyCollectionProperty?.DeepClone<ReadOnlyCollection<TestClassElement>>(),
                    ReadOnlyCollectionInterfaceProperty = this.ReadOnlyCollectionInterfaceProperty?.DeepClone<IReadOnlyCollection<TestClassElement>>(),
                    ListProperty = this.ListProperty?.DeepClone<List<TestClassElement>>(),
                    ListInterfaceProperty = this.ListInterfaceProperty?.DeepClone<IList<TestClassElement>>(),
                    ReadOnlyListInterfaceProperty = this.ReadOnlyListInterfaceProperty?.DeepClone<IReadOnlyList<TestClassElement>>(),
                    ObjectStringProperty = this.ObjectStringProperty?.DeepClone<object>(),
                    ObjectVersionProperty = this.ObjectVersionProperty?.DeepClone<object>(),
                    ObjectUriProperty = this.ObjectUriProperty?.DeepClone<object>(),
                    ObjectTestInterfaceClassProperty = this.ObjectTestInterfaceClassProperty?.DeepClone<object>(),
                    ObjectTestArrayProperty = this.ObjectTestArrayProperty?.DeepClone<object>(),
                    ObjectDictionaryProperty = this.ObjectDictionaryProperty?.DeepClone<object>(),
                    ObjectDictionaryInterfaceProperty = this.ObjectDictionaryInterfaceProperty?.DeepClone<object>(),
                    ObjectReadOnlyDictionaryProperty = this.ObjectReadOnlyDictionaryProperty?.DeepClone<object>(),
                    ObjectReadOnlyDictionaryInterfaceProperty = this.ObjectReadOnlyDictionaryInterfaceProperty?.DeepClone<object>(),
                    ObjectConcurrentDictionaryProperty = this.ObjectConcurrentDictionaryProperty?.DeepClone<object>(),
                    ObjectCollectionProperty = this.ObjectCollectionProperty?.DeepClone<object>(),
                    ObjectCollectionInterfaceProperty = this.ObjectCollectionInterfaceProperty?.DeepClone<object>(),
                    ObjectReadOnlyCollectionProperty = this.ObjectReadOnlyCollectionProperty?.DeepClone<object>(),
                    ObjectReadOnlyCollectionInterfaceProperty = this.ObjectReadOnlyCollectionInterfaceProperty?.DeepClone<object>(),
                    ObjectListProperty = this.ObjectListProperty?.DeepClone<object>(),
                    ObjectListInterfaceProperty = this.ObjectListInterfaceProperty?.DeepClone<object>(),
                    ObjectReadOnlyListInterfaceProperty = this.ObjectReadOnlyListInterfaceProperty?.DeepClone<object>(),
                };

                return result;
            }

            public object Clone() => this.DeepClone();

            public static bool operator ==(TestClass left, TestClass right)
            {
                if (ReferenceEquals(left, right))
                {
                    return true;
                }

                if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                {
                    return false;
                }

                var result = left.Equals(right);

                return result;
            }

            public static bool operator !=(TestClass left, TestClass right) => !(left == right);

            public bool Equals(TestClass other)
            {
                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                if (ReferenceEquals(other, null))
                {
                    return false;
                }

                var result =
                    this.ObjectProperty.IsEqualTo(other.ObjectProperty) &&
                    this.StringProperty.IsEqualTo(other.StringProperty) &&
                    this.VersionProperty.IsEqualTo(other.VersionProperty) &&
                    this.UriProperty.IsEqualTo(other.UriProperty) &&
                    this.TestInterfaceClassProperty.IsEqualTo(other.TestInterfaceClassProperty) &&
                    this.TestArrayProperty.IsEqualTo(other.TestArrayProperty) &&
                    this.DictionaryProperty.IsEqualTo(other.DictionaryProperty) &&
                    this.DictionaryInterfaceProperty.IsEqualTo(other.DictionaryInterfaceProperty) &&
                    this.ReadOnlyDictionaryProperty.IsEqualTo(other.ReadOnlyDictionaryProperty) &&
                    this.ReadOnlyDictionaryInterfaceProperty.IsEqualTo(other.ReadOnlyDictionaryInterfaceProperty) &&
                    this.ConcurrentDictionaryProperty.IsEqualTo(other.ConcurrentDictionaryProperty) &&
                    this.CollectionProperty.IsEqualTo(other.CollectionProperty) &&
                    this.CollectionInterfaceProperty.IsEqualTo(other.CollectionInterfaceProperty) &&
                    this.ReadOnlyCollectionProperty.IsEqualTo(other.ReadOnlyCollectionProperty) &&
                    this.ReadOnlyCollectionInterfaceProperty.IsEqualTo(other.ReadOnlyCollectionInterfaceProperty) &&
                    this.ListProperty.IsEqualTo(other.ListProperty) &&
                    this.ListInterfaceProperty.IsEqualTo(other.ListInterfaceProperty) &&
                    this.ReadOnlyListInterfaceProperty.IsEqualTo(other.ReadOnlyListInterfaceProperty) &&
                    this.ObjectStringProperty.IsEqualTo(other.ObjectStringProperty) &&
                    this.ObjectVersionProperty.IsEqualTo(other.ObjectVersionProperty) &&
                    this.ObjectUriProperty.IsEqualTo(other.ObjectUriProperty) &&
                    this.ObjectTestInterfaceClassProperty.IsEqualTo(other.ObjectTestInterfaceClassProperty) &&
                    this.ObjectTestArrayProperty.IsEqualTo(other.ObjectTestArrayProperty) &&
                    this.ObjectDictionaryProperty.IsEqualTo(other.ObjectDictionaryProperty) &&
                    this.ObjectDictionaryInterfaceProperty.IsEqualTo(other.ObjectDictionaryInterfaceProperty) &&
                    this.ObjectReadOnlyDictionaryProperty.IsEqualTo(other.ObjectReadOnlyDictionaryProperty) &&
                    this.ObjectReadOnlyDictionaryInterfaceProperty.IsEqualTo(other.ObjectReadOnlyDictionaryInterfaceProperty) &&
                    this.ObjectConcurrentDictionaryProperty.IsEqualTo(other.ObjectConcurrentDictionaryProperty) &&
                    this.ObjectCollectionProperty.IsEqualTo(other.ObjectCollectionProperty) &&
                    this.ObjectCollectionInterfaceProperty.IsEqualTo(other.ObjectCollectionInterfaceProperty) &&
                    this.ObjectReadOnlyCollectionProperty.IsEqualTo(other.ObjectReadOnlyCollectionProperty) &&
                    this.ObjectReadOnlyCollectionInterfaceProperty.IsEqualTo(other.ObjectReadOnlyCollectionInterfaceProperty) &&
                    this.ObjectListProperty.IsEqualTo(other.ObjectListProperty) &&
                    this.ObjectListInterfaceProperty.IsEqualTo(other.ObjectListInterfaceProperty) &&
                    this.ObjectReadOnlyListInterfaceProperty.IsEqualTo(other.ObjectReadOnlyListInterfaceProperty);

                return result;
            }

            public override bool Equals(object obj) => this == (obj as TestClass);

            public override int GetHashCode() => HashCodeHelper.Initialize()
                .Hash(this.ObjectProperty)
                .Hash(this.StringProperty)
                .Hash(this.VersionProperty)
                .Hash(this.UriProperty)
                .Hash(this.TestInterfaceClassProperty)
                .Hash(this.TestArrayProperty)
                .Hash(this.DictionaryProperty)
                .Hash(this.DictionaryInterfaceProperty)
                .Hash(this.ReadOnlyDictionaryProperty)
                .Hash(this.ReadOnlyDictionaryInterfaceProperty)
                .Hash(this.ConcurrentDictionaryProperty)
                .Hash(this.CollectionProperty)
                .Hash(this.CollectionInterfaceProperty)
                .Hash(this.ReadOnlyCollectionProperty)
                .Hash(this.ReadOnlyCollectionInterfaceProperty)
                .Hash(this.ListProperty)
                .Hash(this.ListInterfaceProperty)
                .Hash(this.ReadOnlyListInterfaceProperty)
                .Hash(this.ObjectStringProperty)
                .Hash(this.ObjectVersionProperty)
                .Hash(this.ObjectUriProperty)
                .Hash(this.ObjectTestInterfaceClassProperty)
                .Hash(this.ObjectTestArrayProperty)
                .Hash(this.ObjectDictionaryProperty)
                .Hash(this.ObjectDictionaryInterfaceProperty)
                .Hash(this.ObjectReadOnlyDictionaryProperty)
                .Hash(this.ObjectReadOnlyDictionaryInterfaceProperty)
                .Hash(this.ObjectConcurrentDictionaryProperty)
                .Hash(this.ObjectCollectionProperty)
                .Hash(this.ObjectCollectionInterfaceProperty)
                .Hash(this.ObjectReadOnlyCollectionProperty)
                .Hash(this.ObjectReadOnlyCollectionInterfaceProperty)
                .Hash(this.ObjectListProperty)
                .Hash(this.ObjectListInterfaceProperty)
                .Hash(this.ObjectReadOnlyListInterfaceProperty)
                .Value;
        }

        [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = ObcSuppressBecause.CA1812_AvoidUninstantiatedInternalClasses_ClassExistsToUseItsTypeInUnitTests)]
        private class TestInterfaceClass : ITestInterface, IDeepCloneable<TestInterfaceClass>, IEquatable<TestInterfaceClass>
        {
            public TestInterfaceClass(
                Guid? nullableGuidProperty)
            {
                this.NullableGuidProperty = nullableGuidProperty;
            }

            public Guid? NullableGuidProperty { get; private set; }

            public TestInterfaceClass DeepClone()
            {
                var result = new TestInterfaceClass(this.NullableGuidProperty.DeepClone<Guid?>());

                return result;
            }

            public object Clone() => this.DeepClone();

            public static bool operator ==(TestInterfaceClass left, TestInterfaceClass right)
            {
                if (ReferenceEquals(left, right))
                {
                    return true;
                }

                if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                {
                    return false;
                }

                var result = left.Equals(right);

                return result;
            }

            public static bool operator !=(TestInterfaceClass left, TestInterfaceClass right) => !(left == right);

            public bool Equals(TestInterfaceClass other)
            {
                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                if (ReferenceEquals(other, null))
                {
                    return false;
                }

                var result = this.NullableGuidProperty.IsEqualTo(other.NullableGuidProperty);

                return result;
            }

            public override bool Equals(object obj) => this == (obj as TestInterfaceClass);

            public override int GetHashCode() => HashCodeHelper.Initialize()
                .Hash(this.NullableGuidProperty)
                .Value;
        }

        private class TestClassElement : IDeepCloneable<TestClassElement>, IEquatable<TestClassElement>
        {
            public TestClassElement(
                int intProperty)
            {
                this.IntProperty = intProperty;
            }

            public int IntProperty { get; private set; }

            public TestClassElement DeepClone()
            {
                var result = new TestClassElement(this.IntProperty.DeepClone<int>());

                return result;
            }

            public object Clone() => this.DeepClone();

            public static bool operator ==(TestClassElement left, TestClassElement right)
            {
                if (ReferenceEquals(left, right))
                {
                    return true;
                }

                if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                {
                    return false;
                }

                var result = left.Equals(right);

                return result;
            }

            public static bool operator !=(TestClassElement left, TestClassElement right) => !(left == right);

            public bool Equals(TestClassElement other)
            {
                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                if (ReferenceEquals(other, null))
                {
                    return false;
                }

                var result = this.IntProperty.IsEqualTo(other.IntProperty);

                return result;
            }

            public override bool Equals(object obj) => this == (obj as TestClassElement);

            public override int GetHashCode() => HashCodeHelper.Initialize()
                .Hash(this.IntProperty)
                .Value;
        }
        #pragma warning restore SA1201 // Elements should appear in the correct order
    }
}
