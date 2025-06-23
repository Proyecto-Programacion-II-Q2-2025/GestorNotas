using GestorNotas.Models;
using SQLite;

namespace GestorNotas.Services
{
    public class NotaService
    {
        private SQLiteAsyncConnection _db;

        public async Task Init()
        {
            if (_db != null)
                return;

            var ruta = Path.Combine(FileSystem.AppDataDirectory, "notas.db");
            _db = new SQLiteAsyncConnection(ruta);
            await _db.CreateTableAsync<Nota>();
        }

        public async Task<List<Nota>> ObtenerNotas()
        {
            await Init();
            return await _db.Table<Nota>().ToListAsync();
        }

        public async Task AgregarNota(Nota nota)
        {
            await Init();
            await _db.InsertAsync(nota);
        }

        public async Task EliminarNota(Nota nota)
        {
            await Init();
            await _db.DeleteAsync(nota);
        }

        public async Task ActualizarNota(Nota nota)
        {
            await Init();
            await _db.UpdateAsync(nota);
        }
    }
}
