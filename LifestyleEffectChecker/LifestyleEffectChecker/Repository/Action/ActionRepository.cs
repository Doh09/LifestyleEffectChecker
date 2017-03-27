using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using SQLite.Net;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Repository.Action
{
    class ActionRepository : IRepository<Models.Action.Action>
    {
        private readonly SQLiteConnection _connection;

        public ActionRepository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<Models.Action.Action>();
        }
        public async Task<Models.Action.Action> Create(Models.Action.Action obj)
        {
            _connection.Insert(obj);
            return await Task.FromResult(obj);
        }

        public async Task<Models.Action.Action> Read(int id)
        {
            return await Task.FromResult(_connection.Table<Models.Action.Action>().FirstOrDefault(action => action.ID == id));
        }

        public async Task<IEnumerable<Models.Action.Action>> ReadAll()
        {
            return await Task.FromResult((from t in _connection.Table<Models.Action.Action>() select t).ToList());
        }

        public async Task<Models.Action.Action> Update(Models.Action.Action obj)
        {
            _connection.Update(obj);
            return await Task.FromResult(obj);
        }

        public async Task<bool> Delete(int id)
        {
            _connection.Delete<Models.Action.Action>(id);
            return await Task.FromResult(Read(id) != null);
        }
    }
}
