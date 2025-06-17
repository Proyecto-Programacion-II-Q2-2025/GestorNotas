
using SQLite;

namespace GestorNotas.Models
{
    public class Nota
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string? Titulo { get; set; }

        [NotNull]
        public string? Contenido { get; set; }

    }
}
