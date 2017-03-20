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
    class ActionPartRepository : IRepository<ActionPart>
    {
        private readonly SQLiteConnection _connection;

        public ActionPartRepository(SQLiteConnection connection)
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<ActionPart>();
        }

        public ActionPart Create(ActionPart obj)
        {
            _connection.Insert(obj);
            return obj;
        }

        public ActionPart Read(int id)
        {
            return _connection.Table<ActionPart>().FirstOrDefault(action => action.ID == id);
        }

        public IEnumerable<ActionPart> ReadAll()
        {
            return (from t in _connection.Table<ActionPart>() select t).ToList();
        }

        public ActionPart Update(ActionPart obj)
        {
            _connection.Update(obj);
            return obj;
        }

        public bool Delete(int id)
        {
            _connection.Delete<ActionPart>(id);
            return Read(id) != null;
        }
    }
}
