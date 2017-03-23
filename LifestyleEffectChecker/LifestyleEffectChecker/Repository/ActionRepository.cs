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
        public async Task<Action> Create(Action obj)
        {
            _connection.Insert(obj);
            return await Task.FromResult(obj);
        }

        public async Task<Action> Read(int id)
        {
            return await Task.FromResult(_connection.Table<Action>().FirstOrDefault(action => action.ID == id));
        }

        public async Task<IEnumerable<Action>> ReadAll()
        {
            return await Task.FromResult((from t in _connection.Table<Action>() select t).ToList());
        }

        public async Task<Action> Update(Action obj)
        {
            _connection.Update(obj);
            return await Task.FromResult(obj);
        }

        public async Task<bool> Delete(int id)
        {
            _connection.Delete<Action>(id);
            return await Task.FromResult(Read(id) != null);
        }
    }
}
