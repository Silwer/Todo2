namespace TodoTest.Web.Models.ViewModels
{
    public class TodoViewModel
    {
        public bool Completed { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }

        public TodoViewModel()
        {
            
        }

        public TodoViewModel(Todo todo)
        {
            Id = todo.Id.ToString();
            Name = todo.Name;
            Completed = todo.Completed;
        }   
    }
}