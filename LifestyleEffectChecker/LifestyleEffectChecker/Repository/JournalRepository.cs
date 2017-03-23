using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Models;
using SQLite.Net;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Repository
{
    class JournalRepository : IRepository<Journal>
    {
        private readonly SQLiteConnection _connection;

        public JournalRepository(SQLiteConnection connection)
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<Journal>();
        }

        public Journal Create(Journal obj)
        {
            _connection.Insert(obj);
            return obj;
        }

        public Journal Read(int id)
        {
            return _connection.Table<Journal>().FirstOrDefault(action => action.ID == id);
        }

        public IEnumerable<Journal> ReadAll()
        {
            return (from t in _connection.Table<Journal>() select t).ToList();
        }

        public Journal Update(Journal obj)
        {
            _connection.Update(obj);
            return obj;
        }

        public bool Delete(int id)
        {
            _connection.Delete<Journal>(id);
            return Read(id) != null;
        }
    }
}
