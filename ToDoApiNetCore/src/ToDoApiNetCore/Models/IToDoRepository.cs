using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApiNetCore.Models
{
    public interface IToDoRepository
    {
        void Add(ToDoItem toDoItem);
        IEnumerable<ToDoItem> GetAll();
        ToDoItem Find(string key);
        ToDoItem Remove(string key);
        void Update(ToDoItem toDoItem);
    }
}
