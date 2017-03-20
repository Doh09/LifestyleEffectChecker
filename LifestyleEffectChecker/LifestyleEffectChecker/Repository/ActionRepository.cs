using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Connection;
using SQLite.Net;
using Xamarin.Forms;
using Action = LifestyleEffectChecker.Models.Action;

namespace LifestyleEffectChecker.Repository
{
    class ActionRepository : IRepository<Action>
    {
        private readonly SQLiteConnection _connection;

        public ActionRepository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<Action>();
        }
        public Action Create(Action obj)
        {
            _connection.Insert(obj);
            return obj;
        }

        public Action Read(int id)
        {
            return _connection.Table<Action>().FirstOrDefault(action => action.ID == id);
        }

        public IEnumerable<Action> ReadAll()
        {
            return (from t in _connection.Table<Action>() select t).ToList();
        }

        public Action Update(Action obj)
        {
            _connection.Update(obj);
            return obj;
        }

        public bool Delete(int id)
        {
            _connection.Delete<Action>(id);
            return Read(id) != null;
        }
    }
}
