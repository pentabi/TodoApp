using TodoApp.Models;

namespace TodoApp.ViewModels
{
    public class FinishedTodoViewModel
    {
        public IEnumerable<FinishedTodo>? FinishedTodoList { get; set; }
        public FinishedTodo? FinishedTodo { get; set; }
    }
}
