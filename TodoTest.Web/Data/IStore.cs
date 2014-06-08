using System;
using System.Collections.Generic;

namespace TodoTest.Web.Data
{
    public interface IStore
    {
        IPersistable Add<T>(T @object) where T : IPersistable;
        IEnumerable<T> Get<T>(int skip, int take) where T : IPersistable;
        T Get<T>(IPersistable @object) where T : IPersistable;
        T Update<T>(T @object) where T : IPersistable;
        T Delete<T>(Guid id) where T : IPersistable;
    }
}