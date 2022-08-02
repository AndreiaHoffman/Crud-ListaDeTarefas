using ToDoList.Models;

namespace ToDoList.ViewModel
{
    public class ToDoViewModel
    {
        public ToDoViewModel(){}

        public ToDoViewModel(ToDo tarefa)
        {
            Id = tarefa.Id;
            Nome = tarefa.Nome;
            Completado = tarefa.Completado;
            Prazo = tarefa.Prazo.HasValue ? tarefa.Prazo.Value.ToString("dd/MM/yyyy") : string.Empty;

        }

        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public bool Completado { get; set; }

        public string Prazo { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
    }
}
