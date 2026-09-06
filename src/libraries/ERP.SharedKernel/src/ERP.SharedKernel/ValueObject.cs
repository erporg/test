// Copyright (c) 2026 ERP Organization
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software
// and associated documentation files (the “Software”), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

namespace ERP.SharedKernel;

/// <summary>
///     Represents a base class for value objects in a domain-driven design context.
///     Value objects are immutable and compared based on their properties values rather than object identity.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>
    ///     Determines whether the specified <see cref="ValueObject" /> is equal to the current <see cref="ValueObject" />.
    /// </summary>
    /// <param name="other">The <see cref="ValueObject" /> to compare with the current <see cref="ValueObject" />.</param>
    /// <returns>
    ///     <c>true</c> if the specified <see cref="ValueObject" /> is equal to the current <see cref="ValueObject" />;
    ///     otherwise, <c>false</c>.
    /// </returns>
    public virtual bool Equals(ValueObject? other) => other is not null && ValuesAreEqual(other);

    /// <summary>
    ///     Determines whether two <see cref="ValueObject" /> instances are equal.
    /// </summary>
    /// <param name="a">The first <see cref="ValueObject" /> instance to compare.</param>
    /// <param name="b">The second <see cref="ValueObject" /> instance to compare.</param>
    /// <returns><c>true</c> if the two <see cref="ValueObject" /> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(ValueObject? a, ValueObject? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    /// <summary>
    ///     Determines whether two <see cref="ValueObject" /> instances are not equal.
    /// </summary>
    /// <param name="a">The first <see cref="ValueObject" /> instance to compare.</param>
    /// <param name="b">The second <see cref="ValueObject" /> instance to compare.</param>
    /// <returns><c>true</c> if the two <see cref="ValueObject" /> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(ValueObject? a, ValueObject? b) => !(a == b);

    /// <summary>
    ///     Determines whether the specified <see cref="ValueObject" /> is equal to the current <see cref="ValueObject" />.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="ValueObject" />.</param>
    /// <returns>
    ///     <c>true</c> if the specified object is equal to the current <see cref="ValueObject" />; otherwise,
    ///     <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj) => obj is ValueObject valueObject && ValuesAreEqual(valueObject);

    /// <summary>
    ///     Serves as the default hash function for the <see cref="ValueObject" /> type.
    /// </summary>
    /// <returns>An integer representing the hash code for the current <see cref="ValueObject" />.</returns>
    public override int GetHashCode() =>
        GetAtomicValues().Aggregate(
            0,
            (hashcode, value) =>
                HashCode.Combine(hashcode, value.GetHashCode()));

    /// <summary>
    ///     Returns the collection of atomic values that collectively define the equality of the current
    ///     <see cref="ValueObject" /> instance.
    /// </summary>
    /// <returns>
    ///     An <see cref="IEnumerable{T}" /> of objects representing the atomic values of the current
    ///     <see cref="ValueObject" />.
    /// </returns>
    protected abstract IEnumerable<object> GetAtomicValues();

    private bool ValuesAreEqual(ValueObject valueObject) =>
        GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
}
