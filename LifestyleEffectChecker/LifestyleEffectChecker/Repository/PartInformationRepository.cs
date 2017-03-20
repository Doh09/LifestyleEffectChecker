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

        public PartInformation Create(PartInformation obj)
        {
            _connection.Insert(obj);
            return obj;
        }

        public PartInformation Read(int id)
        {
            return _connection.Table<PartInformation>().FirstOrDefault(action => action.ID == id);
        }

        public IEnumerable<PartInformation> ReadAll()
        {
            return (from t in _connection.Table<PartInformation>() select t).ToList();
        }

        public PartInformation Update(PartInformation obj)
        {
            _connection.Update(obj);
            return obj;
        }

        public bool Delete(int id)
        {
            _connection.Delete<PartInformation>(id);
            return Read(id) != null;
        }
    }
}
