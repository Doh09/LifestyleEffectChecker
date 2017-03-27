using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Models.Effect;
using SQLite.Net;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Repository.Effect
{
    class EffectRepository : IRepository<Models.Effect.Effect>
    {
        private readonly SQLiteConnection _connection;

        public EffectRepository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<Models.Effect.Effect>();
        }

        public async Task<Models.Effect.Effect> Create(Models.Effect.Effect obj)
        {
            _connection.Insert(obj);
            return await Task.FromResult(obj);
        }

        public async Task<Models.Effect.Effect> Read(int id)
        {
            return await Task.FromResult(_connection.Table<Models.Effect.Effect>().FirstOrDefault(action => action.ID == id));
        }

        public async Task<IEnumerable<Models.Effect.Effect>> ReadAll()
        {
            return await Task.FromResult((from t in _connection.Table<Models.Effect.Effect>() select t).ToList());
        }

        public async Task<Models.Effect.Effect> Update(Models.Effect.Effect obj)
        {
            _connection.Update(obj);
            return await Task.FromResult(obj);
        }

        public async Task<bool> Delete(int id)
        {
            _connection.Delete<EffectParameter>(id);
            return await Task.FromResult(Read(id) != null);
        }

    }
}
