using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TodoTest.Web.Data
{
    public class Store : IStore
    {
        protected static readonly ConcurrentBag<IPersistable> Database = new ConcurrentBag<IPersistable>();

        public static void DropDatabase()
        {
            while (!Database.IsEmpty)
            {
                IPersistable _;
                Database.TryTake(out _);
            }
        }

        public virtual IPersistable Add<T>(T @object) where T : IPersistable
        {
            if (@object.Id == Guid.Empty)
            {
                @object.Id = Guid.NewGuid();
            }

            Database.Add(@object);

            return @object;
        }

        public virtual IEnumerable<T> Get<T>(int skip, int take) where T : IPersistable
        {
            var skipIndex = skip == 0 ? 0 : skip - 1;
            return Database.Skip(skipIndex).Take(take).Cast<T>();
        }

        public virtual T Get<T>(IPersistable @object) where T : IPersistable
        {
            return (T)Database.SingleOrDefault(x => x.Id.Equals(@object.Id));
        }

        public virtual T Update<T>(T @object) where T : IPersistable
        {
            var _ = Delete<T>(@object.Id);
            Add(@object);
            return @object;
        }

        public virtual T Delete<T>(Guid guid) where T : IPersistable
        {
            var deletedItem = default (T);
            var tmp = new List<T>();
            while (!Database.IsEmpty)
            {
                IPersistable item;
                Database.TryTake(out item);
                if (item.Id.Equals(guid))
                {
                    deletedItem = (T)item;
                    break;
                }

                tmp.Add((T)item);
            }

            tmp.ForEach(x => Database.Add(x));

            return deletedItem;
        }
    }
}