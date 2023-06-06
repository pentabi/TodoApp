using System.ComponentModel.DataAnnotations.Schema;
using TodoApp.Models;

namespace TodoApp.ViewModels
{
    public class TodoViewModel
    {
        public IEnumerable<Todo>? TodoList { get; set; }
        public Todo? NewTodo { get; set; }
    }
}
