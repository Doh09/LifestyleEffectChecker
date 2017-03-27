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
    class EffectParameterRepository : IRepository<EffectParameter>
    {
        private readonly SQLiteConnection _connection;

        public EffectParameterRepository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<EffectParameter>();
        }

        public async Task<EffectParameter> Create(EffectParameter obj)
        {
            _connection.Insert(obj);
            return await Task.FromResult(obj);
        }

        public async Task<EffectParameter> Read(int id)
        {
            return await Task.FromResult(_connection.Table<EffectParameter>().FirstOrDefault(action => action.ID == id));
        }

        public async Task<IEnumerable<EffectParameter>> ReadAll()
        {
            return await Task.FromResult((from t in _connection.Table<EffectParameter>() select t).ToList());
        }

        public async Task<EffectParameter> Update(EffectParameter obj)
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
