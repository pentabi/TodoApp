using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Interfaces;
using TodoApp.Models;

namespace TodoApp.Repository
{
    public class FinishedTodoRepository : IFinishedTodoRepository
    {
        private readonly ApplicationDbContext _context;
        public FinishedTodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(FinishedTodo todo)
        {
            _context.Add(todo);
            return Save();
        }

        public bool Delete(FinishedTodo todo)
        {
            _context.Remove(todo);
            return Save();
        }



        //get list of all the FinishedTodos
        public async Task<IEnumerable<FinishedTodo>> GetAll()
        {
            return await _context.FinishedTodos.ToListAsync();
        }

        //get by Id
        public async Task<FinishedTodo> GetByIdAsync(int id)
        {
            return await _context.FinishedTodos.FirstOrDefaultAsync(i => i.Id == id);
        }
        //get by date
        public async Task<IEnumerable<FinishedTodo>> GetFinishedTodoByDate(DateTime date)
        {
            return await _context.FinishedTodos.Where(c => c.CreatedDate.Date == date.Date).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(FinishedTodo todo)
        {
            _context.Update(todo);
            return Save();
        }
    }
}