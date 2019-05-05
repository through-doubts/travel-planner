using System.Linq;
using System.Reflection;

namespace TravelPlanner.Infrastructure
{
    /// <summary>
    /// Базовый класс для всех Value типов.
    /// </summary>
    public class ValueType<T>
    {
        private static readonly PropertyInfo[] properties;
        static ValueType()
        {
            properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return !properties.Select(p => p.GetValue(this))
                .Zip(properties.Select(p => p.GetValue(other)),
                    (p1, p2) =>
                    {
                        if (p1 == null && p2 == null)
                            return true;
                        return p1.Equals(p2);
                    })
                .Contains(false);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((T)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return properties.Select(p => p.GetValue(this).GetHashCode())
                    .Aggregate((previous, current) => (previous * 397) ^ current);
            }
        }

        public override string ToString()
        {
            var typeName = typeof(T).Name;
            var namesAndValues = properties.OrderBy(p => p.Name).Select(p => $"{p.Name}: {p.GetValue(this)}");
            return $"{typeName}({string.Join("; ", namesAndValues)})";
        }
    }
}