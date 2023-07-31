namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Structure for strongly typing an identifier value
    /// </summary>
    /// <typeparam name="T">Type of Model</typeparam>
    public readonly struct Id<T> 
    {
        private readonly Guid _value;

        public Id(Guid value) => _value = value;

        public static explicit operator Guid(Id<T> id) => id._value;

        public static explicit operator Id<T>(Guid value) => new Id<T>(value);

        public override bool Equals(object? obj) => obj is Id<T> item && _value.Equals(item._value);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Id<T> x, Id<T> y) => x._value == y._value;

        public static bool operator !=(Id<T> x, Id<T> y) => !(x == y);

        public override string ToString() => $"{base.ToString()} {_value}";

    }
}
