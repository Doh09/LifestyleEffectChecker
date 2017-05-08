using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Models.Effect;
using LifestyleEffectChecker.Repository.ServiceGateway;
using SQLite.Net;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Repository.Effect
{
    class EffectParameterRepository : IRepository<EffectParameter>
    {
        private readonly SQLiteConnection _connection;

        private static EffectParameterRepository instance;

        //Used to check for connection
        private readonly ICheckNetwork _netWork = DependencyService.Get<ICheckNetwork>();
        private readonly IRepository<EffectParameter> _serviceGateway = ServiceGatewayFacade.GetEffectParameterServiceGateway();
        public static EffectParameterRepository GetInstance()
        {
            if (instance == null)
                instance = new EffectParameterRepository();
            return instance;
        }

        private EffectParameterRepository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<EffectParameter>();
        }

        public async Task<EffectParameter> Create(EffectParameter obj)
        {    //If there is online connection, send signal to the RestAPI
            if (_netWork.IsOnline())
            {
                await _serviceGateway.Create(obj);
            }
            _connection.Insert(obj);
            return await Task.FromResult(obj);
        }

        public async Task<EffectParameter> Read(int id, bool goOnline = false)
        {   //If there is online connection, send signal to the RestAPI
            if (goOnline)
            {
                if (_netWork.IsOnline())
                {
                    return await _serviceGateway.Read(id);
                }
            }
            return await Task.FromResult(_connection.Table<EffectParameter>().FirstOrDefault(action => action.ID == id));
        }

        public async Task<IEnumerable<EffectParameter>> ReadAll(bool goOnline = false)
        { //If there is online connection, send signal to the RestAPI
            if (goOnline)
            {
                if (_netWork.IsOnline())
                {
                    return await _serviceGateway.ReadAll();
                }
            }
            return await Task.FromResult((from t in _connection.Table<EffectParameter>() select t).ToList());
        }

        public async Task<EffectParameter> Update(EffectParameter obj)
        {  //If there is online connection, send signal to the RestAPI
            if (_netWork.IsOnline())
            {
                await _serviceGateway.Update(obj);
            }
            _connection.Update(obj);
            return await Task.FromResult(obj);
        }

        public async Task<bool> Delete(int id)
        {   //If there is online connection, send signal to the RestAPI
            if (_netWork.IsOnline())
            {
                await _serviceGateway.Delete(id);
            }
            _connection.Delete<EffectParameter>(id);
            return await Task.FromResult(Read(id) != null);
        }
    }
}
