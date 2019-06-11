using System.Collections.Generic;
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
    }
}
