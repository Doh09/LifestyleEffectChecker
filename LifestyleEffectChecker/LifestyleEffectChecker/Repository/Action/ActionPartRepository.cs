using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Models.Action;
using SQLite.Net;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Repository.Action
{
    class ActionPartRepository : IRepository<ActionPart>
    {
        private readonly SQLiteConnection _connection;

        public ActionPartRepository(SQLiteConnection connection)
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<ActionPart>();
        }

        public async Task<ActionPart> Create(ActionPart obj)
        {
            _connection.Insert(obj);
            return await Task.FromResult(obj);
        }

        public async Task<ActionPart> Read(int id)
        {
            return await Task.FromResult(_connection.Table<ActionPart>().FirstOrDefault(actionpart => actionpart.ID == id));
        }

        public async Task<IEnumerable<ActionPart>> ReadAll()
        {
            return await Task.FromResult((from t in _connection.Table<ActionPart>() select t).ToList());
        }

        public async Task<ActionPart> Update(ActionPart obj)
        {
            _connection.Update(obj);
            return await Task.FromResult(obj);
        }

        public async Task<bool> Delete(int id)
        {
            _connection.Delete<ActionPart>(id);
            return await Task.FromResult(Read(id) != null);
        }
    }
}
