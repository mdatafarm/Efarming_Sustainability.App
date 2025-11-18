using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Database
{
    public class SQLiteConnection
    {
        protected static SQLiteAsyncConnection _db;

        protected async Task InitAsync()
        {
            if (_db != null)
                return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "EfarmingSustainability.db3");
            _db = new SQLiteAsyncConnection(databasePath);
            
        }
    }
}
