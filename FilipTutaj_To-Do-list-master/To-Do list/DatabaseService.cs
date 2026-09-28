using SQLite;

namespace To_Do_list
{
    // Uwaga: w tym projekcie istnieje klasa "Task" (Task.cs), dlatego
    // wszędzie, gdzie chodzi o System.Threading.Tasks.Task, używamy
    // pełnej nazwy System.Threading.Tasks.Task, żeby uniknąć konfliktu nazw.
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;
        private System.Threading.Tasks.Task? _initTask;

        public DatabaseService()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "tasks.db3");
            _database = new SQLiteAsyncConnection(dbPath);
        }

        private System.Threading.Tasks.Task InitAsync()
        {
            _initTask ??= _database.CreateTableAsync<Done>();
            return _initTask;
        }

        // Dodaje zarchiwizowane zadanie do pliku bazy danych.
        public async System.Threading.Tasks.Task<int> AddDoneTaskAsync(Done doneTask)
        {
            await InitAsync();
            return await _database.InsertAsync(doneTask);
        }

        // Wczytuje wszystkie zarchiwizowane zadania z bazy danych.
        public async System.Threading.Tasks.Task<List<Done>> GetDoneTasksAsync()
        {
            await InitAsync();
            return await _database.Table<Done>().ToListAsync();
        }
    }
}