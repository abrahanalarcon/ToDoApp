using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _db;

        public async Task Init()
        {
            if (_db is not null)
                return;

            var ruta = Path.Combine(FileSystem.AppDataDirectory, "Tareas.db");
            _db = new SQLiteAsyncConnection(ruta);
            await _db.CreateTableAsync<Tarea>();
        }

        public async Task<List<Tarea>> GetTareasAsync()
        {
            await Init();
            return await _db.Table<Tarea>().ToListAsync();
        }

        public async Task AddTareaAsync(Tarea tarea)
        {
            await Init();
            await _db.InsertAsync(tarea);
        }

        public async Task DeleteTareaAsync(Tarea tarea)
        {
            await Init();
            await _db.DeleteAsync(tarea);
        }

    }
}
