using System;

namespace TodoTest.Web.Data
{
    public interface IPersistable
    {
        Guid Id { get; set; }
    }
}