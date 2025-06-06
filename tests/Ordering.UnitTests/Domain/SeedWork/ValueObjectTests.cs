﻿namespace Inked.Ordering.UnitTests.Domain.SeedWork;

public class ValueObjectTests
{
    private static readonly ValueObject APrettyValueObject = new ValueObjectA(1, "2",
        Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"), new ComplexObject(2, "3"));

    public static IEnumerable<object[]> EqualValueObjects =>
        new[]
        {
            new object[] { null, null, "they should be equal because they are both null" },
            new object[]
            {
                APrettyValueObject, APrettyValueObject, "they should be equal because they are the same object"
            },
            new object[]
            {
                new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"),
                    new ComplexObject(2, "3")),
                new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"),
                    new ComplexObject(2, "3")),
                "they should be equal because they have equal members"
            },
            new object[]
            {
                new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"),
                    new ComplexObject(2, "3"), "xpto"),
                new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"),
                    new ComplexObject(2, "3"), "xpto2"),
                "they should be equal because all equality components are equal, even though an additional member was set"
            },
            new object[]
            {
                new ValueObjectB(1, "2", 1, 2, 3), new ValueObjectB(1, "2", 1, 2, 3),
                "they should be equal because all equality components are equal, including the 'C' list"
            }
        };

    public static IEnumerable<object[]> NonEqualValueObjects =>
        new[]
        {
            new object[]
            {
                new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"),
                    new ComplexObject(2, "3")),
                new ValueObjectA(2, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"),
                    new ComplexObject(2, "3")),
                "they should not be equal because the 'A' member on ValueObjectA is different among them"
            },
            new object[]
            {
                new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"),
                    new ComplexObject(2, "3")),
                new ValueObjectA(1, null, Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"),
                    new ComplexObject(2, "3")),
                "they should not be equal because the 'B' member on ValueObjectA is different among them"
            },
            new object[]
            {
                new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"),
                    new ComplexObject(2, "3")),
                new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"),
                    new ComplexObject(3, "3")),
                "they should not be equal because the 'A' member on ValueObjectA's 'D' member is different among them"
            },
            new object[]
            {
                new ValueObjectA(1, "2", Guid.Parse("97ea43f0-6fef-4fb7-8c67-9114a7ff6ec0"),
                    new ComplexObject(2, "3")),
                new ValueObjectB(1, "2"),
                "they should not be equal because they are not of the same type"
            },
            new object[]
            {
                new ValueObjectB(1, "2", 1, 2, 3), new ValueObjectB(1, "2", 1, 2, 3, 4),
                "they should be not be equal because the 'C' list contains one additional value"
            },
            new object[]
            {
                new ValueObjectB(1, "2", 1, 2, 3, 5), new ValueObjectB(1, "2", 1, 2, 3),
                "they should be not be equal because the 'C' list contains one additional value"
            },
            new object[]
            {
                new ValueObjectB(1, "2", 1, 2, 3, 5), new ValueObjectB(1, "2", 1, 2, 3, 4),
                "they should be not be equal because the 'C' lists are not equal"
            }
        };

    [TestMethod]
    [DynamicData(nameof(EqualValueObjects))]
    public void Equals_EqualValueObjects_ReturnsTrue(ValueObject instanceA, ValueObject instanceB, string reason)
    {
        // Act
        var result = EqualityComparer<ValueObject>.Default.Equals(instanceA, instanceB);

        // Assert
        Assert.IsTrue(result, reason);
    }

    [TestMethod]
    [DynamicData(nameof(NonEqualValueObjects))]
    public void Equals_NonEqualValueObjects_ReturnsFalse(ValueObject instanceA, ValueObject instanceB, string reason)
    {
        // Act
        var result = EqualityComparer<ValueObject>.Default.Equals(instanceA, instanceB);

        // Assert
        Assert.IsFalse(result, reason);
    }

    private class ValueObjectA : ValueObject
    {
        public ValueObjectA(int a, string b, Guid c, ComplexObject d, string notAnEqualityComponent = null)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            NotAnEqualityComponent = notAnEqualityComponent;
        }

        public int A { get; }
        public string B { get; }
        public Guid C { get; }
        public ComplexObject D { get; }
        public string NotAnEqualityComponent { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return A;
            yield return B;
            yield return C;
            yield return D;
        }
    }

    private class ValueObjectB : ValueObject
    {
        public ValueObjectB(int a, string b, params int[] c)
        {
            A = a;
            B = b;
            C = c.ToList();
        }

        public int A { get; }
        public string B { get; }

        public List<int> C { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return A;
            yield return B;

            foreach (var c in C)
            {
                yield return c;
            }
        }
    }

    private class ComplexObject : IEquatable<ComplexObject>
    {
        public ComplexObject(int a, string b)
        {
            A = a;
            B = b;
        }

        public int A { get; }

        public string B { get; }

        public bool Equals(ComplexObject other)
        {
            return other != null &&
                   A == other.A &&
                   B == other.B;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ComplexObject);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B);
        }
    }
}
