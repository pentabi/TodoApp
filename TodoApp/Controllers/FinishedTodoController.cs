using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Interfaces;
using TodoApp.Models;
using TodoApp.Repository;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    public class FinishedTodoController : Controller
    {
        private readonly IFinishedTodoRepository _finishedTodoRepository;

        public FinishedTodoController(ApplicationDbContext context, IFinishedTodoRepository finishedTodoRepository)
        {
            _finishedTodoRepository = finishedTodoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var finishedTodoViewModel = new FinishedTodoViewModel
            {
                FinishedTodoList = await _finishedTodoRepository.GetAll(),
                FinishedTodo = new FinishedTodo() // Initialize a new instance of Todo for NewTodo
            };

            return View(finishedTodoViewModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _finishedTodoRepository.GetByIdAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            _finishedTodoRepository.Delete(todo);

            return RedirectToAction("Index", "FinishedTodo");
        }
    }
}
