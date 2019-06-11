using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;

namespace TravelPlanner.Application
{
    public class ListHandler<T> where T : INameable
    {
        private readonly List<T> list;
        public ListHandler(List<T> list)
        {
            this.list = list;
        }

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Delete(T item)
        {
            list.Remove(item);
        }

        public void Replace(T oldItem, T newItem)
        {
            var index = list.IndexOf(oldItem);
            list.RemoveAt(index);
            list.Insert(index, newItem);
        }

        public List<T> GetItems()
        {
            return new List<T>(list);
        }
    }
}
