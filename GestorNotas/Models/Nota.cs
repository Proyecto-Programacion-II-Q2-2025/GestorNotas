
using SQLite;

namespace GestorNotas.Models
{
    public class Nota
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Contenido { get; set; }

        public override string ToString() => $"{Titulo} - {Contenido}";
    }
}

