using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TravelPlanner.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        public static AutoCompleteStringCollection ToAutoCompleteStringCollection<T>
            (this IEnumerable<T> enumerable)
        {
            var autoComplete = new AutoCompleteStringCollection();
            foreach (var item in enumerable) autoComplete.Add(item.ToString());
            return autoComplete;
        }

        public static List<TObject> InitializeProperty<TObject, TProperty>
            (this IEnumerable<TObject> enumerable, Action<TObject, TProperty> setProperty,
            IEnumerable<TProperty> properties)
        {
            var objectArray = enumerable.ToList();
            var propertyArray = properties.ToList();
            for (var i = 0; i < propertyArray.Count; i++)
            {
                setProperty(objectArray[i], propertyArray[i]);
            }

            return objectArray;
        }

        public static IEnumerable<Tuple<T1, T2>> Pairs<T1, T2>
            (this IEnumerable<T1> enumerable, IEnumerable<T2> other)
        {
            return from item in enumerable from otherItem in other select Tuple.Create(item, otherItem);
        }
    }
}
