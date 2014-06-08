using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoTest.Web.Data;
using TodoTest.Web.Models;
using TodoTest.Web.Models.ViewModels;

namespace TodoTest.Web.Controllers
{
    public class TodoController : Controller
    {
        private readonly IStore store;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
   

        public TodoController(IStore store)
        {
            this.store = store;
        }
       
        public JsonResult List(int page, int pageSize, string name)
        {
            try
            {
                var todos = MvcApplication.TodoList.Skip((page) * pageSize).Take(pageSize);
                MvcApplication.PaginatorList = new List<Todo>();
                foreach (var item in todos)
                {
                    if (item.Name.ToLower().Contains(name.ToLower()))
                    {
                        MvcApplication.PaginatorList.Add(item);
                    }
                }
                
                System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
      new System.Web.Script.Serialization.JavaScriptSerializer();
                string s = jSearializer.Serialize(MvcApplication.PaginatorList);
                return new JsonResult
                {
                    Data = s,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex) 
            {
                log.Debug("Paginator fungerar ej", ex);
            }

            return null;

        }

        [HttpPost]
        public JsonResult Add(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new HttpException((int)HttpStatusCode.BadRequest, "name cant be empty");
                }
                
                var createdTodo = store.Add(new Todo(name));
                return new JsonResult
                {
                    Data = createdTodo
                };
            }
            catch (Exception ex) 
            {
                log.Debug("Att lägga nya todos fungerar ej", ex);
            }

            return null;
        }

        [HttpPost]
        public void AddToDoList(TodoViewModel todo) 
        {
            MvcApplication.TodoList.Add(new Todo(todo.Name));
        }

        [HttpPost]
        public JsonResult Remove(string guid, string removename, string name)
        {
            try
            {
                if (MvcApplication.TodoList.Count > 0 && MvcApplication.TodoList != null && removename == "paginator")
                {
                    foreach (var item in MvcApplication.PaginatorList) 
                    {
                        if (item.Id == Guid.Parse(guid)) 
                        {
                            MvcApplication.PaginatorList.Remove(item);
                            MvcApplication.TodoList.Remove(item);
                        }
                    }

                    foreach (var item in MvcApplication.TodoList)
                    {
                        if (item.Id == Guid.Parse(guid))
                            MvcApplication.TodoList.Remove(item);
                    }
                    
                   return new JsonResult
                    {
                        Data = new TodoViewModel(new Todo(name))
                    };
                }
                else
                {
                    var deletedToDo = store.Delete<Todo>(Guid.Parse(guid));
                    if (deletedToDo == null)
                    {
                        throw new HttpException((int)HttpStatusCode.NotFound, "todo not found");
                    }

                    return new JsonResult
                    {
                        Data = new TodoViewModel(deletedToDo)
                    };
                }
            }
            catch (Exception ex)
            {
                log.Debug("Att ta bort todos fungerar ej", ex);
            }

            return null;
        }
        
        [HttpPost]
        public JsonResult Update(TodoViewModel todo)
        {
            var updatedTodo = store.Update(new Todo(todo.Name, todo.Completed) { Id = Guid.Parse(todo.Id) });
            if (updatedTodo == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "todo not found");
            }

            return new JsonResult
            {
                Data = new TodoViewModel(updatedTodo)
            };
        }

        [HttpPost]
        public JsonResult InputUpdate(string name)
        {
            try
            {
                var list = store.Get<Todo>(0, 50);
                MvcApplication.TodoList = new List<Todo>();

                if (list != null && name != null)
                {
                    foreach (var item in list)
                    {
                        if (item.Name.ToLower().Contains(name.ToLower()))
                        {
                            MvcApplication.TodoList.Add(item);
                        }

                    }
                }
                
                System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
                  new System.Web.Script.Serialization.JavaScriptSerializer();
                string s = jSearializer.Serialize(MvcApplication.TodoList);

                return new JsonResult
                {
                    Data = s
                };
            }
            catch (Exception ex) 
            {
                log.Debug("Att söka todos fungerar ej", ex);
            }

            return null;

        }
    }

}
