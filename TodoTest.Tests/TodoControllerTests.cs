using FluentAssertions;
using NUnit.Framework;
using TodoTest.Web.Controllers;
using TodoTest.Web.Data;
using TodoTest.Web.Models;
using TodoTest.Web.Models.ViewModels;

namespace TodoTest.Tests
{
    [TestFixture]
    public class TodoControllerTests
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

            const string expectedName = "go to the gym";
            var controller = new TodoController(store);
            var addResult = controller.Add(expectedName);
            (addResult.Data as IPersistable).Id.Should().NotBeEmpty();

            store.Get<Todo>(1, 10).Should().ContainSingle(x => x.Name.Equals(expectedName));
        }

        [Test]
        public void CanDeleteTodo()
        {
            const string expectedName = "go to school";

            var store = new Store();
            store.Add(new Todo("go to the gym"));
            var addedTodo = store.Add(new Todo("go to school")) as Todo;

            var controller = new TodoController(store);
            var removeResult = controller.Remove(addedTodo.Id.ToString(), string.Empty, addedTodo.Name);
            var deletedTodo = (removeResult.Data as TodoViewModel).Id;
            deletedTodo.Should().NotBeNull();

            store.Get<Todo>(1, 10).Should().HaveCount(1);
            store.Get<Todo>(1, 10).Should().NotContain(x => x.Name.Equals(expectedName));
        }

        [Test]
        public void CanUpdateTodo()
        {
            const string expectedName = "go to work";

            var store = new Store();
            store.Add(new Todo("go to the gym"));
            var addedTodo = store.Add(new Todo("go to school")) as Todo;

            var controller = new TodoController(store);
            addedTodo.Name = expectedName;
            var updateResult = controller.Update(new TodoViewModel(addedTodo));
            var updatedTodo = (updateResult.Data as TodoViewModel).Id;
            updatedTodo.Should().NotBeNull();

            store.Get<Todo>(1, 10).Should().HaveCount(2);
            store.Get<Todo>(1, 10).Should().ContainSingle(x => x.Name.Equals(expectedName));
        }
    }
}
