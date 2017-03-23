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
    class PartInformationRepository : IRepository<PartInformation>
    {
        private readonly SQLiteConnection _connection;

        public PartInformationRepository(SQLiteConnection connection)
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<PartInformation>();
        }

        public async Task<PartInformation> Create(PartInformation obj)
        {
            _connection.Insert(obj);
            return await Task.FromResult(obj);
        }

        public async Task<PartInformation> Read(int id)
        {
            return await Task.FromResult(_connection.Table<PartInformation>().FirstOrDefault(action => action.ID == id));
        }

        public async Task<IEnumerable<PartInformation>> ReadAll()
        {
            return await Task.FromResult((from t in _connection.Table<PartInformation>() select t).ToList());
        }

        public async Task<PartInformation> Update(PartInformation obj)
        {
            _connection.Update(obj);
            return await Task.FromResult(obj);
        }

        public async Task<bool> Delete(int id)
        {
            _connection.Delete<PartInformation>(id);
            return await Task.FromResult(Read(id) != null);
        }
    }
}
