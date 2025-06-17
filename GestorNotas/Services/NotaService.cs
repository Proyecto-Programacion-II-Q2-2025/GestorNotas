

using GestorNotas.Models;
using SQLite;

namespace GestorNotas.Services
{
    class NotaService
    {
        private readonly SQLiteConnection _connection;

        public NotaService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notas.db3");
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Nota>();
        }

        public List<Nota> GetAll()
        {
            return _connection.Table<Nota>().ToList();
        }

        public int Insert(Nota Nota)
        {
            return _connection.Insert(Nota);
        }

        public int Update(Nota Nota)
        {
            return _connection.Update(Nota);
        }

        public int Delete(Nota Nota)
        {
            return _connection.Delete(Nota);
        }
        //quede en el minuto 45:22 del 04062025
    }
}
