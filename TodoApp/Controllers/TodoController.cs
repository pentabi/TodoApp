using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Interfaces;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ApplicationDbContext context, ITodoRepository todoRepository)
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

        //detail page
        //for https://localhost/todo/detail/1
        public async Task<IActionResult> Detail(int id)
        {
            Todo todo = await _todoRepository.GetByIdAsync(id);
            return View(todo);
        }

        //create page
        //for https://localhost/todo/create
        public IActionResult Create()
        {
            return View();
        }

        //edit page
        //for https://localhost/todo/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            Todo todo = await _todoRepository.GetByIdAsync(id);
            return View(todo);
        }

        [HttpPost]
        public IActionResult Create(TodoViewModel todoVM)
        {
            //check the url and the data type/restriction match
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var todo = new Todo
            {
                Title = todoVM.NewTodo.Title,
                Description = todoVM.NewTodo.Description,
                Image = todoVM.NewTodo.Image,
                TodoCategory = todoVM.NewTodo.TodoCategory,
                CreatedDate = todoVM.NewTodo.CreatedDate != default ? todoVM.NewTodo.CreatedDate : DateTime.Now
            };
            _todoRepository.Add(todo);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            _todoRepository.TransferData(todo);
            _todoRepository.Delete(todo);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Todo todo)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit todo");
                return View("Edit", todo);
            }

            Todo currentTodo = await _todoRepository.GetByIdAsync(todo.Id);
            if (currentTodo == null)
            {
                return NotFound();
            }

            //Update properties of the existing todo object
            currentTodo.Title = todo.Title;
            currentTodo.Description = todo.Description;
            currentTodo.TodoCategory = todo.TodoCategory;

            //Update
            _todoRepository.Update(currentTodo);

            return RedirectToAction("Index", "Home");

        }
    }
}
