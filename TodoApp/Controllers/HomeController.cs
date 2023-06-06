using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoApp.Data;
using TodoApp.Interfaces;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public HomeController(ApplicationDbContext context, ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        //for https://localhost
        public async Task<IActionResult> Index()
        {
            var todoViewModel = new TodoViewModel
            {
                TodoList = await _todoRepository.GetAll(),
                NewTodo = new Todo() // Initialize a new instance of Todo for NewTodo
            };

            return View(todoViewModel);
        }

    }
}