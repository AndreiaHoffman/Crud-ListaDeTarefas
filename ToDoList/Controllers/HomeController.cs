using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.Services;
using ToDoList.ViewModel;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ToDoServices _services;

        public HomeController(ILogger<HomeController> logger, ToDoServices todoservices)
        {
            _logger = logger;
            _services = todoservices;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lista = await _services.RecuperarTodos();
            return View(lista.Select(t => new ToDoViewModel(t)));
        }

        [HttpGet]
        public async Task<IActionResult> Novo()
        {
            return View(new ToDoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Novo(ToDoViewModel tarefa)
        {
            if (!ModelState.IsValid)
            {
                return View(tarefa);
            }

            try
            {
                DateTime data;

                DateTime.TryParse(tarefa.Prazo, out data);
                var todo = new ToDo()
                {
                    Id = tarefa.Id,
                    Completado = tarefa.Completado,
                    Nome = tarefa.Nome,
                    Prazo = string.IsNullOrEmpty(tarefa.Prazo) ? null : data
                };

                await _services.AdicionarTarefa(todo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(tarefa);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var tarefa = await _services.RecuperarPorId(id);
            return View(new ToDoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ToDoViewModel tarefa)
        {
            if (!ModelState.IsValid)
            {
                return View(tarefa);
            }

            try
            {
                DateTime data;

                DateTime.TryParse(tarefa.Prazo, out data);
                var todo = new ToDo()
                {
                    Id = tarefa.Id,
                    Completado = tarefa.Completado,
                    Nome = tarefa.Nome,
                    Prazo = string.IsNullOrEmpty(tarefa.Prazo) ? null : data
                };

                await _services.AtualizarTarefas(todo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(tarefa);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var tarefas = await _services.RecuperarPorId(id);
            return View(new ToDoViewModel(tarefas));
        }

        [HttpGet]
        public async Task<IActionResult> Deletar(int id)
        {
            var tarefas = await _services.RecuperarPorId(id);
            return View(new ToDoViewModel(tarefas));
        }

        [HttpPost, ActionName("Deletar")]
        public async Task<IActionResult> DeletarTarefa(int id)
        {
            var tarefa = await _services.RecuperarPorId(id);

            await _services.Deletar(id);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}