using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Models;
using SQLite.Net;
using Xamarin.Forms;
using Action = LifestyleEffectChecker.Models.Action;

namespace LifestyleEffectChecker.Repository
{
    class JournalRepository : IRepository<Journal>
    {
        private readonly SQLiteConnection _connection;

        private static JournalRepository instance;

        public static JournalRepository GetInstance()
        {
            if (instance == null)
                instance = new JournalRepository();
            return instance;
        }

        private JournalRepository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<Journal>();

            Create(new Journal() {Name = "Adventure Journal", ID = 24, ActionParts = new List<Action>()});
            Create(new Journal() { Name = "Food Journal", ID = 25, ActionParts = new List<Action>() });
            Create(new Journal() { Name = "Excercise Journal", ID = 26, ActionParts = new List<Action>() });
        }

        public async Task<Journal> Create(Journal obj)
        {
            _connection.Insert(obj);
            
            return await Task.FromResult(obj);
        }

        public async Task<Journal> Read(int id)
        {
            return await Task.FromResult(_connection.Table<Journal>().FirstOrDefault(journal => journal.ID == id));
        }

        public async Task<IEnumerable<Journal>> ReadAll()
        {
            var journals = (from t in _connection.Table<Journal>() select t).ToList();
            var toReturn = await Task.FromResult(journals);
            
            return toReturn;
        }

        public async Task<Journal> Update(Journal obj)
        {
            _connection.Update(obj);
            return await Task.FromResult(obj);
        }

        public async Task<bool> Delete(int id)
        {
            _connection.Delete<Journal>(id);
            return await Task.FromResult(Read(id) != null);
        }
    }
}
