using FluentAssertions;
using NUnit.Framework;
using TodoTest.Web.Data;
using TodoTest.Web.Models;

namespace TodoTest.Tests
{
    [TestFixture]
    class StoreTests
    {
        [SetUp]
        public void Setup()
        {
            Store.DropDatabase();
        }

        [Test]
        public void CanAddTodo()
        {
            var store = new Store();
            var addedToDo = store.Add(new Todo("buy milk"));
            addedToDo.Should().NotBeNull();
            addedToDo.Id.Should().NotBeEmpty();
        }

        [Test]
        public void CanGetAllTodos()
        {
            const string expectedName = "buy milk";

            var store = new Store();
            store.Add(new Todo(expectedName));
            var todos = store.Get<Todo>(0, 10);
            todos.Should().ContainSingle(x => x.Name.Equals(expectedName));
        }

        [Test]
        public void CanGetTodo()
        {
            const string expectedName = "buy milk";

            var store = new Store();
            var persisted = store.Add(new Todo(expectedName));
            var todo = store.Get<Todo>(persisted);
            todo.Name.Should().Be(expectedName);
        }

        [Test]
        public void CanUpdateTodo()
        {
            const string expectedName = "buy chocolate";

            var store = new Store();
            var persisted = store.Add(new Todo("buy milk"));
            var todo = store.Get<Todo>(persisted);
            todo.Name = expectedName;
            var updatedToDo = store.Update(todo);

            store.Get<Todo>(0, 10).Should().HaveCount(1);
            store.Get<Todo>(updatedToDo).Name.Should().Be(expectedName);
        }
            
        [Test]
        public void CanDeleteTodo()
        {
            var store = new Store();
            var persisted = store.Add(new Todo("buy milk"));
            store.Add(new Todo("buy chocolate"));
            store.Get<Todo>(0, 10).Should().HaveCount(2);

            store.Delete<Todo>(persisted.Id);
            store.Get<Todo>(0, 10).Should().ContainSingle(x => x.Name.Equals("buy chocolate"));
            store.Get<Todo>(0, 10).Should().HaveCount(1);
        }
    }
}
