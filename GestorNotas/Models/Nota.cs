

using SQLite;

namespace GestorNotas.Models
{
    public class Notas
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string? Titulo { get; set; }
        public string? Contenido { get; set; }
    }
}

