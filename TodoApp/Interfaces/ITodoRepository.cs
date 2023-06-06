using TodoApp.Models;

namespace TodoApp.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAll();
        bool TransferData(Todo todo);
        Task<Todo> GetByIdAsync(int id);
        Task<IEnumerable<Todo>> GetTodoByDate(DateTime Date);
        bool Add(Todo todo);
        bool Update(Todo todo);
        bool Delete(Todo todo);
        bool Save();
    }
}
