using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Services;
using ToDoList.ViewModel;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly ToDoServices _services;

        public MobileController(ToDoServices services)
        {
            _services = services;
        }


        [HttpGet]
        [Route("RecuperarTodos")]
        public async Task<IActionResult> RecuperarTodos()
        {
            var tarefas = await _services.RecuperarTodos();
            return Ok(tarefas.Select(t => new ToDoViewModel(t)));
        }

        [HttpGet]
        [Route("RecuperaPorId/{id}")]
        public async Task<ActionResult<ToDoViewModel>> RecuperaPorId(int id)
        {
            var tarefas = await _services.RecuperarPorId(id);
            return Ok(new ToDoViewModel(tarefas));
        }


    }
}
