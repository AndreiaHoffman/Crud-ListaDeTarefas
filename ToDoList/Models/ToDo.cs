namespace ToDoList.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public bool Completado { get; set; }

        public DateTime? Prazo { get; set; } = null;
    }
}
