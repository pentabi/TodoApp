using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Interfaces;
using TodoApp.Models;

namespace TodoApp.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;
        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Todo todo)
        {
            _context.Add(todo);
            return Save();
        }

        public bool Delete(Todo todo)
        {
            _context.Remove(todo);
            return Save();
        }



        //get list of all the Todos
        public async Task<IEnumerable<Todo>> GetAll()
        {
            return await _context.Todos.ToListAsync();
        }

        public bool TransferData(Todo todo)
        {
            var sourceItem = _context.Todos.FirstOrDefault(t => t.Title == todo.Title);

            if (sourceItem != null)
            {
                var transformedItem = new FinishedTodo
                {
                    Title = sourceItem.Title,
                    Description = sourceItem.Description,
                    Image = sourceItem.Image,
                    TodoCategory = sourceItem.TodoCategory,
                    CreatedDate = sourceItem.CreatedDate,
                    AppUserId = sourceItem.AppUserId
                };

                _context.FinishedTodos.Add(transformedItem); // Assuming FinishedTodos is the destination table

                return Save();//returns true
            }

            return false;
        }



        //get by Id
        public async Task<Todo> GetByIdAsync(int id)
        {
            return await _context.Todos.FirstOrDefaultAsync(i => i.Id == id);
        }
        //get by date
        public async Task<IEnumerable<Todo>> GetTodoByDate(DateTime date)
        {
            return await _context.Todos.Where(c => c.CreatedDate.Date == date.Date).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Todo todo)
        {
            _context.Update(todo);
            return Save();
        }
    }
}
