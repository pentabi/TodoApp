using TodoApp.Models;

namespace TodoApp.Interfaces
{
    public interface IFinishedTodoRepository
    {
        Task<IEnumerable<FinishedTodo>> GetAll();
        Task<FinishedTodo> GetByIdAsync(int id);
        Task<IEnumerable<FinishedTodo>> GetFinishedTodoByDate(DateTime Date);
        bool Add(FinishedTodo todo);
        bool Update(FinishedTodo todo);
        bool Delete(FinishedTodo todo);
        bool Save();
    }
}
