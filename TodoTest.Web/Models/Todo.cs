using System;
using TodoTest.Web.Data;

namespace TodoTest.Web.Models
{
    public class Todo : IPersistable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }

        public Todo(string name, bool completed = false)
        {
            Name = name;
            Completed = completed;
        }
    }
}