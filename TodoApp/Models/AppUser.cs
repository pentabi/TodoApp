using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net;

namespace TodoApp.Models
{
    public class AppUser
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<Todo>? Todos { get; set; }
        public ICollection<FinishedTodo>? FinishedTodos { get; set; }
    }
}
