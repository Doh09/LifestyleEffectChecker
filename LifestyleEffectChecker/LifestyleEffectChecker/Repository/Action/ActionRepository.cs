using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifestyleEffectChecker.Connection;
using LifestyleEffectChecker.Models;
using LifestyleEffectChecker.Repository.ServiceGateway;
using SQLite.Net;
using Xamarin.Forms;

namespace LifestyleEffectChecker.Repository.Action
{
    class ActionRepository : IRepository<Models.Action.Action>
    {
        private readonly SQLiteConnection _connection;

        private static ActionRepository instance;
        //Used to check for connection
        private readonly ICheckNetwork _netWork = DependencyService.Get<ICheckNetwork>();
        private readonly IRepository<Models.Action.Action> _serviceGateway = ServiceGatewayFacade.GetActionServiceGateway();

        public static ActionRepository GetInstance()
        {
            if (instance == null)
                instance = new ActionRepository();
            return instance;
        }

        private ActionRepository()
        {
            _connection = DependencyService.Get<IDBConnection>().GetConnection();
            _connection.CreateTable<Models.Action.Action>();
        }
        public async Task<Models.Action.Action> Create(Models.Action.Action obj)
        {
            //If there is online connection, send signal to the RestAPI
            //if (_netWork.IsOnline())
            //{
            //    await _serviceGateway.Create(obj);
            //}
            
            _connection.Insert(obj);
            return await Task.FromResult(obj);
        }

        public async Task<Models.Action.Action> Read(int id, bool goOnline = false)
        {
            if (goOnline)
            {
                if (_netWork.IsOnline())
                {
                    return await _serviceGateway.Read(id);
                }
            }
            return await Task.FromResult(_connection.Table<Models.Action.Action>().FirstOrDefault(action => action.ID == id));
        }

        public async Task<IEnumerable<Models.Action.Action>> ReadAll(bool goOnline = false)
        {
            //If there is online connection, send signal to the RestAPI
            if (goOnline)
            {
                if (_netWork.IsOnline())
                {
                    return await _serviceGateway.ReadAll();
                }
            }
            return await Task.FromResult((from t in _connection.Table<Models.Action.Action>() select t).ToList());
        }
        
        public async Task<Models.Action.Action> Update(Models.Action.Action obj)
        {  
            //If there is online connection, send signal to the RestAPI
            if (_netWork.IsOnline())
            {
                await _serviceGateway.Update(obj);
            }
            _connection.Update(obj);
            return await Task.FromResult(obj);
        }

        public async Task<bool> Delete(int id)
        {
            //If there is online connection, send signal to the RestAPI
            if (_netWork.IsOnline())
            {
                await _serviceGateway.Delete(id);
            }
            _connection.Delete<Models.Action.Action>(id);
            return await Task.FromResult(Read(id) != null);
        }
    }
}
