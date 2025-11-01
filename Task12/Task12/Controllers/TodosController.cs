using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Task12.DataAccess;
using Task12.Models;
using Task12.ViewModels;

namespace Task12.Controllers
{
    public class TodosController : Controller
    {
        public ApplicationDbContext db = new();
        public IActionResult NameEntry(string? Name)
        {
            if(Name is null)
            {
                return View();
            }
            Response.Cookies.Append("UserName", Name, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            });
            
            return RedirectToAction("ViewTodos");
        }

        public IActionResult ViewTodos()
        {
            var todos = db.todos.AsNoTracking().AsQueryable();

            return View(todos.AsEnumerable());
        }

        [HttpGet]
        public IActionResult CreateTodo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTodo(TodoVM todoVM, IFormFile file)
        {
            var fileName = "";
            if (file is not null && file.Length > 0)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\TodosFiles", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
            }
            db.todos.Add(new Todo
            {
                Title = todoVM.Title,
                Description = todoVM.Description,
                DeadLine = todoVM.DeadLine,
                File = fileName,
                UserId = 1
            });
            db.SaveChanges();
            return RedirectToAction(nameof(ViewTodos));
        }

        [HttpGet]
        public IActionResult EditTodo(int id)
        {
            var currentTodo = db.todos.AsQueryable().Where(t => t.Id == id).FirstOrDefault();
            ViewBag.TodoTitle = currentTodo.Title;
            ViewBag.TodoDescription = currentTodo.Description;
            ViewBag.TodoDeadLine = currentTodo.DeadLine.ToString();
            ViewBag.TodoFile = currentTodo.File;
            return View();
        }

        [HttpPost]
        public IActionResult EditTodo(TodoVM todoVM,IFormFile file,int id)
        {
            var currentTodo = db.todos.AsQueryable().Where(t => t.Id == id).FirstOrDefault();
            currentTodo.Title = todoVM.Title is null? currentTodo.Title : todoVM.Title;
            currentTodo.Description = todoVM.Description is null? currentTodo.Description : todoVM.Description;
            currentTodo.DeadLine = todoVM.DeadLine == default? currentTodo.DeadLine : todoVM.DeadLine;
            if(file is not null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\TodosFiles",fileName);
                if(!System.IO.File.Exists(filePath))
                {
                    System.IO.File.Create(filePath);
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\TodosFiles", currentTodo.File));
                }
                currentTodo.File = fileName;
            }
            db.SaveChanges();
            TempData["Success"] = true;
            return RedirectToAction(nameof(ViewTodos));
        }

        public IActionResult DeleteTodo(int id)
        {
            try
            {
                var currentTodo = db.todos.FirstOrDefault(t => t.Id == id);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\TodosFiles", currentTodo.File);
                if(System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                db.todos.Remove(currentTodo);
                db.SaveChanges();
                return RedirectToAction(nameof(ViewTodos));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
