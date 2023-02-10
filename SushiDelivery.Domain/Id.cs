namespace SushiDelivery.Domain
{
    /// <summary>
    /// Structure for strongly typing an identifier value
    /// </summary>
    /// <typeparam name="T">Type of Model</typeparam>
    public struct Id<T> 
    {
        private readonly Guid _value;

        public Id(Guid value)
        {
            _value = value;
        }

        public static explicit operator Guid(Id<T> id)
        {
            return id._value;
        }

        public static explicit operator Id<T>(Guid value)
        {
            return new Id<T>(value);
        }


        public override bool Equals(object obj)
        {
            return obj is Id<T> item && _value.Equals(item._value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(Id<T> x, Id<T> y)
        {
            return x==y;
        }

        public static bool operator !=(Id<T> x, Id<T> y)
        {
            return !(x == y);
        }
    }
}
