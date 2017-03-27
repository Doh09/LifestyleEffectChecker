using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Models;
using SQLite.Net;
using Xamarin.Forms;
using Effect = LifestyleEffectChecker.Models.Effect;

namespace LifestyleEffectChecker.Repository
{
    class EffectRepository : IRepository<Effect>
    {
        private readonly SQLiteConnection _connection;

        public EffectRepository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<Effect>();
        }

        public async Task<Effect> Create(Effect obj)
        {
            _connection.Insert(obj);
            return await Task.FromResult(obj);
        }

        public async Task<Effect> Read(int id)
        {
            return await Task.FromResult(_connection.Table<Effect>().FirstOrDefault(action => action.ID == id));
        }

        public async Task<IEnumerable<Effect>> ReadAll()
        {
            return await Task.FromResult((from t in _connection.Table<Effect>() select t).ToList());
        }

        public async Task<Effect> Update(Effect obj)
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
