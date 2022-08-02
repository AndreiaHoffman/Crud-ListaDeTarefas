using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class ToDoServices
    {
        private readonly AppDbContext _services;

        public ToDoServices(AppDbContext services)
        {
            _services = services;
        }

        public async Task<ToDo> RecuperarPorId(int id)
        {
            return await _services.Todo.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<ToDo>> RecuperarTodos()
        {
            return await _services.Todo.ToListAsync();
        } 

        public async Task<bool> AdicionarTarefa(ToDo Tarefa)
        {
            try
            {
                await _services.Todo.AddAsync(Tarefa);
                await _services.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarTarefas(ToDo Tarefa)
        {
            try
            {
                _services.Entry(Tarefa).State = EntityState.Modified;
                await _services.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Deletar(int id)
        {
            try
            {
                var tarefa = await RecuperarPorId(id);
                _services.Todo.Remove(tarefa);
                await _services.SaveChangesAsync();
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
